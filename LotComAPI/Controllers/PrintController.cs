using LotComAPI.Services;
using LotComAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using LotComAPI.Models;
using LotComAPI.Mappers;

namespace LotComAPI.Controllers;

/// <summary>
/// Provides RESTful operations on the Print database.
/// </summary>
[ApiController] // assigns the class an attribute that gives it more API-friendly functions
[Route("[controller]")] // defines the HTTP route (resolves to api/print)
public class PrintController : ControllerBase
{
    private readonly IPrintService _printService;

    public PrintController(IPrintService PrintService)
    {
        // confirm a PrintService was injected
        if (PrintService is null)
        {
            throw new ArgumentNullException(nameof(PrintService));
        }
        _printService = PrintService;
    }

    /// <summary>
    /// Processes a GET HTTP request for all of the Print objects in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public ActionResult<IEnumerable<PrintDto>> GetAll()
    {
        IEnumerable<Print> PrintsFromDatabase = _printService.GetAll();
        // convert each of the Print entities into a Dto
        IEnumerable<PrintDto> Dtos = PrintsFromDatabase
            .Select(PrintMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for Print objects created on a specific Date in the database.
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [HttpGet("onDate")]
    public ActionResult<IEnumerable<Print>> GetOnDate([FromQuery] string day, [FromQuery] string month, [FromQuery] string year)
    {
        // parse the date to a DateTime object
        string Date = $"{month}/{day}/{year}";
        bool ValidDate = DateTime.TryParse(Date, out DateTime ParsedDate);
        if (!ValidDate)
        {
            return BadRequest();
        }
        IEnumerable<Print> PrintsFromDatabase = _printService.GetOnDate(ParsedDate);
        // convert each of the Print entities into a Dto
        IEnumerable<PrintDto> Dtos = PrintsFromDatabase
            .Select(PrintMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for Print objects created by a specific Process on a specific Date in the database.
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [HttpGet("onDateBy")]
    public ActionResult<IEnumerable<Print>> GetOnDateByProcess([FromQuery] string day, [FromQuery] string month, [FromQuery] string year, [FromQuery] int processId)
    {
        // parse the date to a DateTime object
        string Date = $"{month}/{day}/{year}";
        bool ValidDate = DateTime.TryParse(Date, out DateTime ParsedDate);
        if (!ValidDate)
        {
            return BadRequest();
        }
        IEnumerable<Print> PrintsFromDatabase = _printService.GetOnDateByProcess(ParsedDate, processId);
        // convert each of the Print entities into a Dto
        IEnumerable<PrintDto> Dtos = PrintsFromDatabase
            .Select(PrintMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for a single Print object in the database.
    /// </summary>
    /// <param name="id">The unique ID of the object that is requested.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<PrintDto> Get(int id)
    {
        Print? PrintFromDatabase = _printService.Get(id);
        if (PrintFromDatabase is null)
        {
            return NotFound();
        }
        return Ok(PrintMapper.EntityToDto(PrintFromDatabase));
    }

    /// <summary>
    /// Processes a POST HTTP request to add a single Print object to the database. 
    /// </summary>
    /// <param name="Print">The Print object that will be POSTed to the database.</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<PrintDto> Create(int ProcessId, int PartId, int Quantity, int Shift, string Operator, string ProductionDate, int? SecondaryQuantity = null, int? TertiaryQuantity = null, int? SecondaryShift = null, int? TertiaryShift = null, string? SecondaryOperator = null, string? TertiaryOperator = null, int? JBKNumber = null, string? LotNumber = null, int? DieNumber = null, int? DeburrJBKNumber = null, string? HeatNumber = null)
    {
        // collect a Dto from the HTTP request parameters
        PrintDto DtoFromHttp = PrintMapper.HttpToDto
        (
            ProcessId,
            PartId,
            Quantity,
            Shift,
            Operator,
            ProductionDate,
            SecondaryQuantity,
            TertiaryQuantity,
            SecondaryShift,
            TertiaryShift,
            SecondaryOperator,
            TertiaryOperator,
            JBKNumber,
            LotNumber,
            DieNumber,
            DeburrJBKNumber,
            HeatNumber
        );
        // map the new Print (as a Dto) to an entity and add it to the Db
        Print Entity = PrintMapper.DtoToEntity(DtoFromHttp);
        Entity = _printService.Create(Entity);
        // remap the entity to a Dto to return its CreatedAtRoute status
        PrintDto PrintToReturn = PrintMapper.EntityToDto(Entity);
        // send a CreatedAtRoute response with a 201 status code
        return new CreatedAtActionResult
        (
            nameof(Get),    // the HTTP method
            "Print",        // the Controller name
            new { id = PrintToReturn.Id },  // the parameter(s) and value(s) to pass
            PrintToReturn   // the entity that was created
        );
    }

    /// <summary>
    /// Processes a PUT HTTP request to update a single Print object in the database.
    /// </summary>
    /// <param name="id">The unique ID of the object that is requested.</param>
    /// <param name="Print">The Print object that will be POSTed to the database.</param>
    /// <returns></returns>
    [HttpPut]
    public IActionResult Update(int id, Print Print)
    {
        return NoContent();
    }

    /// <summary>
    /// Processes a DELETE HTTP request to remove a single Print object in the database.
    /// </summary>
    /// <param name="id">The unique ID of the object that is requested.</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }
        Print? PrintFromDatabase = _printService.Get(id);
        if (PrintFromDatabase is null)
        {
            return NotFound();
        }
        _printService.Delete(PrintFromDatabase);
        _printService.Save();
        return NoContent();
    }
}