using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TaskManager.Core.Commands.PlannedTaskCommands;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.CommandHandlers.PlannedTaskCommandHandlers
{
    public class DeletePlannedTaskCommandHandler : ICommandHandler<DeletePlannedTaskCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PlannedTask> _repository;
        private readonly IStorage _storage;

        public DeletePlannedTaskCommandHandler(IUnitOfWork unitOfWork, IRepository<PlannedTask> repository, IStorage storage)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async System.Threading.Tasks.Task Handle(DeletePlannedTaskCommand command)
        {
            var plannedTask = await GetPlannedTask(command);

            await SaveChanges(plannedTask);

            await _storage.SaveData(plannedTask);
        }

        private async Task<PlannedTask> GetPlannedTask(DeletePlannedTaskCommand deletePlannedTaskCommand)
        {
            try
            {
                return await _repository.GetById(deletePlannedTaskCommand.PlannedTaskId);
            }
            catch (Exception ex)
            {
                throw new TaskManagerException($"Error has occured during {nameof(PlannedTask)} getting.", ex);
            }
        }

        private async System.Threading.Tasks.Task SaveChanges(PlannedTask plannedTask)
        {
            try
            {
                _repository.Delete(plannedTask);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new TaskManagerException($"Unable to save {nameof(PlannedTask)} to database.", ex);
            }
        }
    }
}
