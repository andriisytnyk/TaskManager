using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Commands.GlobalTaskCommands;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.CommandHandlers.GlobalTaskCommandHandlers
{
    public class UpdateGlobalTaskCommandHandler : ICommandHandler<UpdateGlobalTaskCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<GlobalTask> _repository;
        private readonly IStorage _storage;

        public UpdateGlobalTaskCommandHandler(IUnitOfWork unitOfWork, IRepository<GlobalTask> repository, IStorage storage)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async System.Threading.Tasks.Task Handle(UpdateGlobalTaskCommand command)
        {
            var globalTask = await UpdateGlobalTask(command);

            await SaveChanges(globalTask);

            await _storage.SaveData(globalTask);
        }

        private async Task<GlobalTask> UpdateGlobalTask(UpdateGlobalTaskCommand updateGlobalTaskCommand)
        {
            try
            {
                var globalTask = await _repository.GetById(updateGlobalTaskCommand.GlobalTaskId);

                globalTask.Name = updateGlobalTaskCommand.Name;
                globalTask.Description = updateGlobalTaskCommand.Description;
                globalTask.Status = updateGlobalTaskCommand.Status;
                globalTask.FinishDate = updateGlobalTaskCommand.FinishDate;
                globalTask.SubTasks = updateGlobalTaskCommand.SubTasks?.Select(st => new SubTask
                {
                    Id = st.Id,
                    Name = st.Name,
                    Description = st.Description,
                    Status = st.Status
                }).ToList();

                return globalTask;
            }
            catch (Exception ex)
            {
                throw new TaskManagerException($"Error has occured during {nameof(GlobalTask)} updating.", ex);
            }
        }

        private async System.Threading.Tasks.Task SaveChanges(GlobalTask globalTask)
        {
            try
            {
                _repository.Update(globalTask);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new TaskManagerException($"Unable to save {nameof(GlobalTask)} to database.", ex);
            }
        }
    }
}
