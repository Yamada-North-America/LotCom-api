using LotCom.DataAccess.Models;
using LotComAPI.Entities;
using LotComAPI.Mappers;
using LotComAPI.Models;
using LotComAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LotComAPI.Controllers;

/// <summary>
/// Provides RESTful operations on the Scan Database.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ScanController : ControllerBase
{
    private readonly IScanService _scanService;

    public ScanController(IScanService ScanService)
    {
        // confirm a ScanService was injected
        if (ScanService is null)
        {
            throw new ArgumentNullException(nameof(ScanService));
        }
        _scanService = ScanService;
    }

    /// <summary>
    /// Processes a GET HTTP request for all of the Scan objects in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public ActionResult<IEnumerable<ScanDto>> GetAll()
    {
        IEnumerable<Scan> ScansFromDatabase = _scanService.GetAll();
        // convert each of the Scan entities into a Dto
        IEnumerable<ScanDto> Dtos = ScansFromDatabase
            .Select(ScanMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for all of the Scan objects within a set range from current date in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet("within")]
    public ActionResult<IEnumerable<ScanDto>> GetAllWithinRange([FromQuery] int days)
    {
        IEnumerable<Scan>? ScansFromDatabase = _scanService.GetAllWithinRange(days);
        if (ScansFromDatabase is null)
        {
            return BadRequest();
        }
        // convert each of the Scan entities into a Dto
        IEnumerable<ScanDto> Dtos = ScansFromDatabase
            .Select(ScanMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for a single Print object in the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<ScanDto> Get(int id)
    {
        Scan? ScanFromDatabase = _scanService.Get(id);
        if (ScanFromDatabase is null)
        {
            return NotFound();
        }
        return Ok(ScanMapper.EntityToDto(ScanFromDatabase));
    }

    /// <summary>
    /// Processes a POST HTTP request to add a single Scan object to the database.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<ScanDto> Create([FromBody] ScanDao Dao)
    {
        // map the new Scan (as a DAO from the Data Access Layer) to an entity and add it to the Db
        Scan Entity = ScanMapper.DaoToEntity(Dao);
        Entity = _scanService.Create(Entity);
        // remap the entity to a Dto to return its CreatedAtRoute status
        ScanDto ScanToReturn = ScanMapper.EntityToDto(Entity);
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
    public IActionResult Update(int id, [FromBody] ScanDao Dao)
    {
        // confirm an id was passed
        if (Dao is null || id != Dao.Id)
        {
            return BadRequest();
        }
        // update the Entity with the service
        bool Result = _scanService.Update(id, ScanMapper.DaoToEntity(Dao));
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
        Scan? ScanFromDatabase = _scanService.Get(id);
        if (ScanFromDatabase is null)
        {
            return NotFound();
        }
        _scanService.Delete(ScanFromDatabase);
        _scanService.Save();
        return NoContent();
    }
}