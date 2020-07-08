using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.Core.Repositories;
using TaskManager.Core.Specifications.PlannedTasksSpecifications;
using TaskManager.Core.Specifications.SubTasksSpecifications;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SubTasksController : ControllerBase
    {
        public IRepository<SubTask> SubTaskRepository { get; set; }

        public SubTasksController(IRepository<SubTask> subTaskRepository)
        {
            SubTaskRepository = subTaskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubTasks()
        {
            try
            {
                var subTasksAllSpecification = new SubTasksAllSpecification();

                return Ok(await SubTaskRepository.GetList(subTasksAllSpecification));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubTask(int id)
        {
            try
            {
                var subTaskByIdSpecification = new SubTaskByIdSpecification(id);

                return Ok((await SubTaskRepository.GetList(subTaskByIdSpecification)).SingleOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubTask([FromBody] SubTaskDTO subTaskDTO)
        {
            try
            {
                SubTaskRepository.Add(new SubTask
                {
                    Name = subTaskDTO.Name,
                    Description = subTaskDTO.Description,
                    Status = subTaskDTO.Status
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubTask([FromBody] SubTaskDTO subTaskDTO)
        {
            try
            {
                SubTaskRepository.Update(new SubTask
                {
                    Id = subTaskDTO.Id,
                    Name = subTaskDTO.Name,
                    Description = subTaskDTO.Description,
                    Status = subTaskDTO.Status
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
        public async Task<IActionResult> DeleteSubTask(int id)
        {
            try
            {
                SubTaskRepository.Delete(await SubTaskRepository.GetById(id));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
