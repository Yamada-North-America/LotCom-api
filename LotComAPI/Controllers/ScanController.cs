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
    /// <param name="Date"></param>
    /// <param name="Address"></param>
    /// <param name="ProcessId"></param>
    /// <param name="PartId"></param>
    /// <param name="Quantity"></param>
    /// <param name="SecondaryQuantity"></param>
    /// <param name="TertiaryQuantity"></param>
    /// <param name="Shift"></param>
    /// <param name="SecondaryShift"></param>
    /// <param name="TertiaryShift"></param>
    /// <param name="Operator"></param>
    /// <param name="SecondaryOperator"></param>
    /// <param name="TertiaryOperator"></param>
    /// <param name="JBKNumber"></param>
    /// <param name="LotNumber"></param>
    /// <param name="DieNumber"></param>
    /// <param name="DeburrJBKNumber"></param>
    /// <param name="HeatNumber"></param>
    /// <param name="ProductionDate"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<ScanDto> Create(string Date, string Address, int ProcessId, int PartId, int Quantity, int? SecondaryQuantity, int? TertiaryQuantity, int Shift, int? SecondaryShift, int? TertiaryShift, string Operator, string? SecondaryOperator, string? TertiaryOperator, int? JBKNumber, string? LotNumber, int? DieNumber, int? DeburrJBKNumber, string? HeatNumber, string ProductionDate)
    {
        ScanDto DtoFromHttp = ScanMapper.HttpToDto
        (
            Date,
            Address,
            ProcessId,
            PartId,
            Quantity,
            SecondaryQuantity,
            TertiaryQuantity,
            Shift,
            SecondaryShift,
            TertiaryShift,
            Operator,
            SecondaryOperator,
            TertiaryOperator,
            JBKNumber,
            LotNumber,
            DieNumber,
            DeburrJBKNumber,
            HeatNumber,
            ProductionDate
        );
        // map the new Scan (as a Dto) to an entity and add it to the Db
        Scan Entity = ScanMapper.DtoToEntity(DtoFromHttp);
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
    /// <param name="id"></param>
    /// <param name="Scan"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPut]
    public IActionResult Update(int id, Scan Scan)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        if (Scan is null)
        {
            return BadRequest();
        }
        Scan? ScanFromDatabase = _scanService.Get(id);
        if (ScanFromDatabase is null)
        {
            return NotFound();
        }
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