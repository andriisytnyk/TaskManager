using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TaskManager.Core.Commands.GlobalTaskCommands;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.CommandHandlers.GlobalTaskCommandHandlers
{
    public class DeleteGlobalTaskCommandHandler : ICommandHandler<DeleteGlobalTaskCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<GlobalTask> _repository;
        private readonly IStorage _storage;

        public DeleteGlobalTaskCommandHandler(IUnitOfWork unitOfWork, IRepository<GlobalTask> repository, IStorage storage)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async System.Threading.Tasks.Task Handle(DeleteGlobalTaskCommand command)
        {
            var globalTask = await GetGlobalTask(command);

            await SaveChanges(globalTask);

            await _storage.SaveData(globalTask);
        }

        private async Task<GlobalTask> GetGlobalTask(DeleteGlobalTaskCommand deleteGlobalTaskCommand)
        {
            try
            {
                return await _repository.GetById(deleteGlobalTaskCommand.GlobalTaskId);
            }
            catch (Exception ex)
            {
                throw new TaskManagerException($"Error has occured during {nameof(GlobalTask)} getting.", ex);
            }
        }

        private async System.Threading.Tasks.Task SaveChanges(GlobalTask globalTask)
        {
            try
            {
                _repository.Delete(globalTask);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new TaskManagerException($"Unable to save {nameof(GlobalTask)} to database.", ex);
            }
        }
    }
}
