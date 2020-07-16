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
    public class UpdateSubTaskCommandHandler : ICommandHandler<UpdateSubTaskCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SubTask> _repository;
        private readonly IStorage _storage;

        public UpdateSubTaskCommandHandler(IUnitOfWork unitOfWork, IRepository<SubTask> repository, IStorage storage)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async System.Threading.Tasks.Task Handle(UpdateSubTaskCommand command)
        {
            var subTask = await GetSubTask(command);

            await SaveChanges(subTask);

            await _storage.SaveData(subTask);
        }

        private async Task<SubTask> GetSubTask(UpdateSubTaskCommand updateSubTaskCommand)
        {
            try
            {
                var subTask = await _repository.GetById(updateSubTaskCommand.SubTaskId);

                subTask.Name = updateSubTaskCommand.Name;
                subTask.Description = updateSubTaskCommand.Description;
                subTask.Status = updateSubTaskCommand.Status;

                return subTask;
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
                _repository.Update(subTask);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new TaskManagerException($"Unable to save {nameof(SubTask)} to database.", ex);
            }
        }
    }
}
