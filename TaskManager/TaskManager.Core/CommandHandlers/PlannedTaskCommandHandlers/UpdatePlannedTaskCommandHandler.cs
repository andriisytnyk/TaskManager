using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Commands.PlannedTaskCommands;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.CommandHandlers.PlannedTaskCommandHandlers
{
    public class UpdatePlannedTaskCommandHandler : ICommandHandler<UpdatePlannedTaskCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PlannedTask> _repository;
        private readonly IStorage _storage;

        public UpdatePlannedTaskCommandHandler(IUnitOfWork unitOfWork, IRepository<PlannedTask> repository, IStorage storage)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async System.Threading.Tasks.Task Handle(UpdatePlannedTaskCommand command)
        {
            var plannedTask = await UpdatePlannedTask(command);

            await SaveChanges(plannedTask);

            await _storage.SaveData(plannedTask);
        }

        private async Task<PlannedTask> UpdatePlannedTask(UpdatePlannedTaskCommand updatePlannedTaskCommand)
        {
            try
            {
                var plannedTask = await _repository.GetById(updatePlannedTaskCommand.PlannedTaskId);

                plannedTask.Name = updatePlannedTaskCommand.Name;
                plannedTask.Description = updatePlannedTaskCommand.Description;
                plannedTask.Status = updatePlannedTaskCommand.Status;
                plannedTask.Frequency = updatePlannedTaskCommand.Frequency;
                plannedTask.Requirement = updatePlannedTaskCommand.Requirement;
                plannedTask.StartDateTime = updatePlannedTaskCommand.StartDateTime;
                plannedTask.FinishDateTime = updatePlannedTaskCommand.FinishDateTime;
                plannedTask.Estimation = updatePlannedTaskCommand.Estimation;
                plannedTask.SubTasks = updatePlannedTaskCommand.SubTasks?.Select(st => new SubTask
                {
                    Id = st.Id,
                    Name = st.Name,
                    Description = st.Description,
                    Status = st.Status
                }).ToList();

                return plannedTask;
            }
            catch (Exception ex)
            {
                throw new TaskManagerException($"Error has occured during {nameof(PlannedTask)} creation.", ex);
            }
        }

        private async System.Threading.Tasks.Task SaveChanges(PlannedTask plannedTask)
        {
            try
            {
                _repository.Update(plannedTask);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new TaskManagerException($"Unable to save {nameof(PlannedTask)} to database.", ex);
            }
        }
    }
}
