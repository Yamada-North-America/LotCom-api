using LotCom.Database.Entities;
using LotComAPI.Services;
using Microsoft.AspNetCore.Mvc;
using LotCom.Database.Mappers;
using LotCom.Core.Models;
using LotCom.Database.Transfer;

namespace LotComAPI.Controllers;

/// <summary>
/// Provides RESTful operations on the Scan Database.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ScanController : ControllerBase
{
    private readonly IScanService _scanService;

    private readonly IMapper<Scan, ScanEntity, ScanDto> _scanMapper;

    public ScanController(IScanService ScanService, IMapper<Scan, ScanEntity, ScanDto> ScanMapper)
    {
        // confirm a ScanService was injected
        if (ScanService is null)
        {
            throw new ArgumentNullException(nameof(ScanService));
        }
        _scanService = ScanService;
        // confirm a ScanMapper was injected
        if (ScanMapper is null)
        {
            throw new ArgumentNullException(nameof(ScanMapper));
        }
        _scanMapper = ScanMapper;
    }

    /// <summary>
    /// Processes a GET HTTP request for all of the Scan objects in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public ActionResult<IEnumerable<ScanDto>> GetAll()
    {
        IEnumerable<ScanEntity> ScansFromDatabase = _scanService.GetAll();
        // convert each of the ScanEntity entities into a Dto
        IEnumerable<ScanDto> Dtos = ScansFromDatabase
            .Select(_scanMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for all of the Scan objects within a set range from current date in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet("within")]
    public ActionResult<IEnumerable<ScanDto>> GetAllWithinRange([FromQuery] int days)
    {
        IEnumerable<ScanEntity>? ScansFromDatabase = _scanService.GetAllWithinRange(days);
        if (ScansFromDatabase is null)
        {
            return BadRequest();
        }
        // convert each of the ScanEntity entities into a Dto
        IEnumerable<ScanDto> Dtos = ScansFromDatabase
            .Select(_scanMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for a single Scan object in the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<ScanDto> Get(int id)
    {
        ScanEntity? ScanFromDatabase = _scanService.Get(id);
        if (ScanFromDatabase is null)
        {
            return NotFound();
        }
        return Ok(_scanMapper.EntityToDto(ScanFromDatabase));
    }

    /// <summary>
    /// Processes a GET HTTP request for all Scan entities that include serialNumber in the database.
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns></returns>
    [HttpGet("serialNumber")]
    public ActionResult<IEnumerable<ScanDto>> GetWithSerialNumber([FromQuery] int serialNumber)
    {
        IEnumerable<ScanEntity>? ScansFromDatabase = _scanService.GetWithSerialNumber(serialNumber);
        if (ScansFromDatabase is null)
        {
            return NotFound();
        }
        IEnumerable<ScanDto> Dtos = ScansFromDatabase
            .Select(_scanMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a POST HTTP request to add a single Scan object to the database.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<ScanDto> Create([FromBody] ScanDto Dto)
    {
        // map the new Scan (as a Dto from the Data Access Layer) to an entity and add it to the Db
        ScanEntity Entity = _scanMapper.DtoToEntity(Dto);
        Entity = _scanService.Create(Entity);
        // remap the entity to a Dto to return its CreatedAtRoute status
        ScanDto ScanToReturn = _scanMapper.EntityToDto(Entity);
        // send a CreatedAtRoute response with a 201 status code
        return new CreatedAtActionResult
        (
            nameof(Get),
            "Scan",
            new { id = ScanToReturn.Id },
            ScanToReturn
        );
    }

    /// <summary>
    /// Processes a PUT HTTP request to update a single Scan object in the database.
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] ScanDto Dto)
    {
        // confirm an id was passed
        if (Dto is null || id != Dto.Id)
        {
            return BadRequest();
        }
        // update the Entity with the service
        bool Result = _scanService.Update(id, _scanMapper.DtoToEntity(Dto));
        if (!Result)
        {
            return NotFound();
        }
        _scanService.Save();
        return NoContent();
    }

    /// <summary>
    /// Processes a DELETE HTTP request to remove a single Scan object from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        ScanEntity? ScanFromDatabase = _scanService.Get(id);
        if (ScanFromDatabase is null)
        {
            return NotFound();
        }
        _scanService.Delete(ScanFromDatabase);
        _scanService.Save();
        return NoContent();
    }
}