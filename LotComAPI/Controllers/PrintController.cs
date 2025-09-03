using LotComAPI.Services;
using LotCom.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using LotCom.Database.Mappers;
using LotCom.Core.Models;
using LotCom.Database.Transfer;

namespace LotComAPI.Controllers;

/// <summary>
/// Provides RESTful operations on the Print database.
/// </summary>
[ApiController] // assigns the class an attribute that gives it more API-friendly functions
[Route("[controller]")] // defines the HTTP route (resolves to api/print)
public class PrintController : ControllerBase
{
    private readonly IPrintService _printService;

    private readonly IMapper<Print, PrintEntity, PrintDto> _printMapper;

    public PrintController(IPrintService PrintService, IMapper<Print, PrintEntity, PrintDto> PrintMapper)
    {
        // confirm a PrintService was injected
        if (PrintService is null)
        {
            throw new ArgumentNullException(nameof(PrintService));
        }
        _printService = PrintService;
        // confirm a PrintMapper was injected
        if (PrintMapper is null)
        {
            throw new ArgumentNullException(nameof(PrintMapper));
        }
        _printMapper = PrintMapper;
    }

    /// <summary>
    /// Processes a GET HTTP request for all of the Print objects in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public ActionResult<IEnumerable<PrintDto>> GetAll()
    {
        IEnumerable<PrintEntity> PrintsFromDatabase = _printService.GetAll();
        // convert each of the PrintEntity entities into a Dto
        IEnumerable<PrintDto> Dtos = PrintsFromDatabase
            .Select(_printMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for Print objects created on a specific Date in the database.
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [HttpGet("onDate")]
    public ActionResult<IEnumerable<PrintDto>> GetOnDate([FromQuery] string day, [FromQuery] string month, [FromQuery] string year)
    {
        // parse the date to a DateTime object
        string Date = $"{month}/{day}/{year}";
        bool ValidDate = DateTime.TryParse(Date, out DateTime ParsedDate);
        if (!ValidDate)
        {
            return BadRequest();
        }
        IEnumerable<PrintEntity> PrintsFromDatabase = _printService.GetOnDate(ParsedDate);
        // convert each of the PrintEntity entities into a Dto
        IEnumerable<PrintDto> Dtos = PrintsFromDatabase
            .Select(_printMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for Print objects created by a specific Process on a specific Date in the database.
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [HttpGet("onDateBy")]
    public ActionResult<IEnumerable<PrintDto>> GetOnDateByProcess([FromQuery] string day, [FromQuery] string month, [FromQuery] string year, [FromQuery] int processId)
    {
        // parse the date to a DateTime object
        string Date = $"{month}/{day}/{year}";
        bool ValidDate = DateTime.TryParse(Date, out DateTime ParsedDate);
        if (!ValidDate)
        {
            return BadRequest();
        }
        IEnumerable<PrintEntity> PrintsFromDatabase = _printService.GetOnDateByProcess(ParsedDate, processId);
        // convert each of the PrintEntity entities into a Dto
        IEnumerable<PrintDto> Dtos = PrintsFromDatabase
            .Select(_printMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for all Print entities that include serialNumber in the database.
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns></returns>
    [HttpGet("serialNumber")]
    public ActionResult<IEnumerable<PrintDto>> GetWithSerialNumber([FromQuery] int serialNumber)
    {
        IEnumerable<PrintEntity>? PrintsFromDatabase = _printService.GetWithSerialNumber(serialNumber);
        if (PrintsFromDatabase is null)
        {
            return NotFound();
        }
        IEnumerable<PrintDto> Dtos = PrintsFromDatabase
            .Select(_printMapper.EntityToDto);
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
        PrintEntity? PrintFromDatabase = _printService.Get(id);
        if (PrintFromDatabase is null)
        {
            return NotFound();
        }
        return Ok(_printMapper.EntityToDto(PrintFromDatabase));
    }

    /// <summary>
    /// Processes a POST HTTP request to add a single Print object to the database. 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<PrintDto> Create([FromBody] PrintDto Dto)
    {
        // map the new Print (as a Dto from the Data Access Layer) to an entity and add it to the Db
        PrintEntity Entity = _printMapper.DtoToEntity(Dto);
        Entity = _printService.Create(Entity);
        // remap the entity to a Dto to return its CreatedAtRoute status
        PrintDto PrintToReturn = _printMapper.EntityToDto(Entity);
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
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] PrintDto Dto)
    {
        // confirm an id was passed
        if (Dto is null || id != Dto.Id)
        {
            return BadRequest();
        }
        // update the Entity with the service
        bool Result = _printService.Update(id, _printMapper.DtoToEntity(Dto));
        if (!Result)
        {
            return NotFound();
        }
        _printService.Save();
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
        PrintEntity? PrintFromDatabase = _printService.Get(id);
        if (PrintFromDatabase is null)
        {
            return NotFound();
        }
        _printService.Delete(PrintFromDatabase);
        _printService.Save();
        return NoContent();
    }
}