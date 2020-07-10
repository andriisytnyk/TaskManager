using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Commands.GlobalTaskCommands;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.CommandHandlers.GlobalTaskCommandHandlers
{
    public class CreateGlobalTaskCommandHandler : ICommandHandler<CreateGlobalTaskCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<GlobalTask> _repository;
        private readonly IStorage _storage;

        public CreateGlobalTaskCommandHandler(IUnitOfWork unitOfWork, IRepository<GlobalTask> repository, IStorage storage)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async System.Threading.Tasks.Task Handle(CreateGlobalTaskCommand command)
        {
            var globalTask = CreateGlobalTask(command);

            await SaveChanges(globalTask);

            await _storage.SaveData(globalTask);
        }

        private GlobalTask CreateGlobalTask(CreateGlobalTaskCommand createGlobalTaskCommand)
        {
            try
            {
                return new GlobalTask()
                {
                    Name = createGlobalTaskCommand.Name,
                    Description = createGlobalTaskCommand.Description,
                    Status = createGlobalTaskCommand.Status,
                    FinishDate = createGlobalTaskCommand.FinishDate,
                    SubTasks = createGlobalTaskCommand.SubTasks?.Select(st => new SubTask
                    {
                        Id = st.Id,
                        Name = st.Name,
                        Description = st.Description,
                        Status = st.Status
                    })
                };
            }
            catch (Exception ex)
            {
                throw new TaskManagerException($"Error has occured during {nameof(GlobalTask)} creation.", ex);
            }
        }

        private async System.Threading.Tasks.Task SaveChanges(GlobalTask globalTask)
        {
            try
            {
                _repository.Add(globalTask);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new TaskManagerException($"Unable to save {nameof(GlobalTask)} to database.", ex);
            }
        }
    }
}
