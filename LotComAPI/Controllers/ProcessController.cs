using LotCom.Database.Entities;
using LotComAPI.Services;
using Microsoft.AspNetCore.Mvc;
using LotCom.Database.Mappers;
using LotCom.Core.Models;
using LotCom.Database.Transfer;

namespace LotComAPI.Controllers;

/// <summary>
/// Provides RESTful operations on the Process database.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProcessController : ControllerBase
{
    private readonly IProcessService _processService;

    private readonly IMapper<Process, ProcessEntity, ProcessDto> _processMapper;

    public ProcessController(IProcessService ProcessService, IMapper<Process, ProcessEntity, ProcessDto> ProcessMapper)
    {
        // confirm a ProcessService was injected
        if (ProcessService is null)
        {
            throw new ArgumentNullException(nameof(ProcessService));
        }
        _processService = ProcessService;
        // confirm a ProcessMapper was injected
        if (ProcessMapper is null)
        {
            throw new ArgumentNullException(nameof(ProcessMapper));
        }
        _processMapper = ProcessMapper;
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
            .Select(_processMapper.EntityToDto));
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
        return Ok(_processMapper.EntityToDto(ProcessFromDatabase));
    }

    /// <summary>
    /// Processes a POST HTTP request to add a single Process object to the database. 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<ProcessDto> Create([FromBody] ProcessDto Dto)
    {
        ProcessEntity Entity = _processMapper.DtoToEntity(Dto);
        Entity = _processService.Create(Entity);
        ProcessDto DtoToReturn = _processMapper.EntityToDto(Entity);
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
    public IActionResult Update(int id, [FromBody] ProcessDto Dto)
    {
        // confirm an id was passed
        if (Dto is null || id != Dto.Id)
        {
            return BadRequest();
        }
        // update the Entity with the service
        bool Result = _processService.Update(id, _processMapper.DtoToEntity(Dto));
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