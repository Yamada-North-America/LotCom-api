using LotCom.DataAccess.Models;
using LotComAPI.Entities;
using LotComAPI.Mappers;
using LotComAPI.Models;
using LotComAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LotComAPI.Controllers;

/// <summary>
/// Provides RESTful operations on the Process database.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProcessController : ControllerBase
{
    private IProcessService _processService;

    public ProcessController(IProcessService Service)
    {
        // confirm that the ProcessService was injected
        if (Service is null)
        {
            throw new ArgumentNullException(nameof(Service));
        }
        _processService = Service;
    }

    /// <summary>
    /// Processes a GET HTTP request for all of the Process objects in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<ProcessDto>> Get()
    {
        IEnumerable<ProcessEntity> ProcessesFromDatabase = _processService.GetAllFromStoredProcedure();
        return Ok(ProcessesFromDatabase
            .Select(ProcessMapper.EntityToDto));
    }

    /// <summary>
    /// Processes a GET HTTP request for a single Process object in the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<ProcessDto> Get(int id)
    {
        ProcessEntity? ProcessFromDatabase = _processService.Get(id);
        if (ProcessFromDatabase is null)
        {
            return NotFound();
        }
        return Ok(ProcessMapper.EntityToDto(ProcessFromDatabase));
    }

    /// <summary>
    /// Processes a POST HTTP request to add a single Process object to the database. 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<ProcessDto> Create([FromBody] ProcessDao Dao)
    {
        ProcessEntity Entity = ProcessMapper.DaoToEntity(Dao);
        Entity = _processService.Create(Entity);
        ProcessDto DtoToReturn = ProcessMapper.EntityToDto(Entity);
        return new CreatedAtActionResult
        (
            nameof(Get),
            "Process",
            new { id = Entity.Id },
            DtoToReturn
        );
    }

    /// <summary>
    /// Processes a PUT HTTP request to update a single Process object in the database.
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] ProcessDao Dao)
    {
        // confirm an id was passed
        if (Dao is null || id != Dao.Id)
        {
            return BadRequest();
        }
        // update the Entity with the service
        bool Result = _processService.Update(id, ProcessMapper.DaoToEntity(Dao));
        if (!Result)
        {
            return NotFound();
        }
        _processService.Save();
        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        ProcessEntity? ProcessFromDatabase = _processService.Get(id);
        if (ProcessFromDatabase is null)
        {
            return NotFound();
        }
        _processService.Delete(ProcessFromDatabase);
        _processService.Save();
        return NoContent();
    }
}