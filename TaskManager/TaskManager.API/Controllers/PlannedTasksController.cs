using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.Core.Repositories;
using TaskManager.Core.Specifications.PlannedTasksSpecifications;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlannedTasksController : ControllerBase
    {
        public IRepository<PlannedTask> PlannedTaskRepository { get; set; }

        public PlannedTasksController(IRepository<PlannedTask> plannedTaskRepository)
        {
            PlannedTaskRepository = plannedTaskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlannedTasks()
        {
            try
            {
                var plannedTaskWithDependenciesSpecification = new PlannedTaskWithDependenciesSpecification();

                return Ok(await PlannedTaskRepository.GetList(plannedTaskWithDependenciesSpecification));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPlannedTask(int id)
        {
            try
            {
                var plannedTaskByIdSpecification = new PlannedTaskByIdSpecification(id);

                return Ok((await PlannedTaskRepository.GetList(plannedTaskByIdSpecification)).SingleOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlannedTask([FromBody] PlannedTaskDTO plannedTaskDTO)
        {
            try
            {
                PlannedTaskRepository.Add(new PlannedTask
                {
                    Name = plannedTaskDTO.Name,
                    Description = plannedTaskDTO.Description,
                    Frequency = plannedTaskDTO.Frequency,
                    Estimation = plannedTaskDTO.Estimation,
                    FinishDateTime = plannedTaskDTO.FinishDateTime,
                    Requirement = plannedTaskDTO.Requirement,
                    StartDateTime = plannedTaskDTO.StartDateTime,
                    Status = plannedTaskDTO.Status,
                    SubTasks = plannedTaskDTO.SubTasks?.Select(st => new SubTask
                    {
                        Id = st.Id,
                        Name = st.Name,
                        Description = st.Description,
                        Status = st.Status
                    })
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlannedTask([FromBody] PlannedTaskDTO plannedTaskDTO)
        {
            try
            {
                PlannedTaskRepository.Update(new PlannedTask
                {
                    Id = plannedTaskDTO.Id,
                    Name = plannedTaskDTO.Name,
                    Description = plannedTaskDTO.Description,
                    Frequency = plannedTaskDTO.Frequency,
                    Estimation = plannedTaskDTO.Estimation,
                    FinishDateTime = plannedTaskDTO.FinishDateTime,
                    Requirement = plannedTaskDTO.Requirement,
                    StartDateTime = plannedTaskDTO.StartDateTime,
                    Status = plannedTaskDTO.Status,
                    SubTasks = plannedTaskDTO.SubTasks?.Select(st => new SubTask
                    {
                        Id = st.Id,
                        Name = st.Name,
                        Description = st.Description,
                        Status = st.Status
                    })
                });

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePlannedTask(int id)
        {
            try
            {
                PlannedTaskRepository.Delete(await PlannedTaskRepository.GetById(id));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
