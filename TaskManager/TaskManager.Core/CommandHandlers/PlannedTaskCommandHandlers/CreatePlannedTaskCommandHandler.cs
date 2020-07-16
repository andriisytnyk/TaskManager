using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TaskManager.Core.Commands.PlannedTaskCommands;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.CommandHandlers.PlannedTaskCommandHandlers
{
    public class CreatePlannedTaskCommandHandler : ICommandHandler<CreatePlannedTaskCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PlannedTask> _repository;
        private readonly IStorage _storage;

        public CreatePlannedTaskCommandHandler(IUnitOfWork unitOfWork, IRepository<PlannedTask> repository, IStorage storage)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async System.Threading.Tasks.Task Handle(CreatePlannedTaskCommand command)
        {
            var plannedTask = CreatePlannedTask(command);

            await SaveChanges(plannedTask);

            await _storage.SaveData(plannedTask);
        }

        private PlannedTask CreatePlannedTask(CreatePlannedTaskCommand createPlannedTaskCommand)
        {
            try
            {
                return new PlannedTask()
                {
                    Name = createPlannedTaskCommand.Name,
                    Description = createPlannedTaskCommand.Description,
                    Status = createPlannedTaskCommand.Status,
                    Frequency = createPlannedTaskCommand.Frequency,
                    Requirement = createPlannedTaskCommand.Requirement,
                    StartDateTime = createPlannedTaskCommand.StartDateTime,
                    FinishDateTime = createPlannedTaskCommand.FinishDateTime,
                    Estimation = createPlannedTaskCommand.Estimation,
                    SubTasks = createPlannedTaskCommand.SubTasks?.Select(st => new SubTask
                    {
                        Id = st.Id,
                        Name = st.Name,
                        Description = st.Description,
                        Status = st.Status
                    }).ToList()
                };
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
                _repository.Add(plannedTask);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new TaskManagerException($"Unable to save {nameof(PlannedTask)} to database.", ex);
            }
        }
    }
}
