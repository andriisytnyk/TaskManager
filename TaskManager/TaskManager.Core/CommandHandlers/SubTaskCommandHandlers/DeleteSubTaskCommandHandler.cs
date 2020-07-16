using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TaskManager.Core.Commands.SubTaskCommands;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.CommandHandlers.SubTaskCommandHandlers
{
    public class DeleteSubTaskCommandHandler : ICommandHandler<DeleteSubTaskCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SubTask> _repository;
        private readonly IStorage _storage;

        public DeleteSubTaskCommandHandler(IUnitOfWork unitOfWork, IRepository<SubTask> repository, IStorage storage)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async System.Threading.Tasks.Task Handle(DeleteSubTaskCommand command)
        {
            var subTask = await GetSubTask(command);

            await SaveChanges(subTask);

            await _storage.SaveData(subTask);
        }

        private async Task<SubTask> GetSubTask(DeleteSubTaskCommand updateSubTaskCommand)
        {
            try
            {
                return await _repository.GetById(updateSubTaskCommand.SubTaskId);
            }
            catch (Exception ex)
            {
                throw new TaskManagerException($"Error has occured during {nameof(SubTask)} creation.", ex);
            }
        }

        private async System.Threading.Tasks.Task SaveChanges(SubTask subTask)
        {
            try
            {
                _repository.Delete(subTask);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new TaskManagerException($"Unable to save {nameof(SubTask)} to database.", ex);
            }
        }
    }
}
