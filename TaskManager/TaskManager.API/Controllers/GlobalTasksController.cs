using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.API.Controllers
{
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalTasksController : ControllerBase
    {
        public IRepository<GlobalTask> GlobalTaskRepository { get; set; }

        public GlobalTasksController(IRepository<GlobalTask> globalTaskRepository)
        {
            GlobalTaskRepository = globalTaskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGlobalTasks()
        {
            try
            {
                return Ok(await GlobalTaskRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGlobalTask([FromBody] GlobalTaskDTO globalTaskDTO)
        {
            try
            {
                GlobalTaskRepository.Add(new GlobalTask
                {
                    Name = globalTaskDTO.Name,
                    Description = globalTaskDTO.Description,
                    FinishDate = globalTaskDTO.FinishDate,
                    Status = globalTaskDTO.Status,
                    SubTasks = globalTaskDTO.SubTasks.Select(st => new SubTask
                    {
                        Id = st.Id,
                        Name = st.Name,
                        Description = st.Description,
                        Status = st.Status
                    }).ToList()
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
