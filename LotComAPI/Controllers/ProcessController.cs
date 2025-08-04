using LotComAPI.Entities;
using LotComAPI.Mappers;
using LotComAPI.Models;
using LotComAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LotComAPI.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProcessController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    private IProcessService _service;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Service"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ProcessController(IProcessService Service)
    {
        // confirm that the ProcessService was injected
        if (Service is null)
        {
            throw new ArgumentNullException(nameof(Service));
        }
        _service = Service;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<ProcessDto>> Get()
    {
        IEnumerable<Process> ProcessesFromDatabase = _service.GetAll();
        return Ok(ProcessesFromDatabase
            .Select(ProcessMapper.EntityToDto));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<ProcessDto> Get(int id)
    {
        Process? ProcessFromDatabase = _service.Get(id);
        if (ProcessFromDatabase is null)
        {
            return NotFound();
        }
        return Ok(ProcessMapper.EntityToDto(ProcessFromDatabase));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="LineCode"></param>
    /// <param name="LineName"></param>
    /// <param name="Title"></param>
    /// <param name="Serialization"></param>
    /// <param name="Type"></param>
    /// <param name="Origination"></param>
    /// <param name="PassThroughType"></param>
    /// <param name="DoesPrint"></param>
    /// <param name="DoesScan"></param>
    /// <param name="UsesJBKNumber"></param>
    /// <param name="UsesLotNumber"></param>
    /// <param name="UsesDieNumber"></param>
    /// <param name="UsesDeburrJBKNumber"></param>
    /// <param name="UsesHeatNumber"></param>
    /// <param name="Previous1"></param>
    /// <param name="Previous2"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<ProcessDto> Create(int LineCode, string LineName, string Title, string? Serialization, string Type, int Origination, string? PassThroughType, int DoesPrint, int DoesScan, int UsesJBKNumber, int UsesLotNumber, int UsesDieNumber, int UsesDeburrJBKNumber, int UsesHeatNumber, int? Previous1, int? Previous2)
    {
        ProcessDto Dto = ProcessMapper.HttpToDto
        (
            LineCode,
            LineName,
            Title,
            Serialization,
            Type,
            Origination,
            PassThroughType,
            DoesPrint,
            DoesScan,
            UsesJBKNumber,
            UsesLotNumber,
            UsesDieNumber,
            UsesDeburrJBKNumber,
            UsesHeatNumber,
            Previous1,
            Previous2
        );
        Process Entity = ProcessMapper.DtoToEntity(Dto);
        Entity = _service.Create(Entity);
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
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public IActionResult Update()
    {
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
        Process? ProcessFromDatabase = _service.Get(id);
        if (ProcessFromDatabase is null)
        {
            return NotFound();
        }
        _service.Delete(ProcessFromDatabase);
        _service.Save();
        return NoContent();
    }
}