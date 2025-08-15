using LotCom.DataAccess.Models;
using LotComAPI.Entities;
using LotComAPI.Mappers;
using LotComAPI.Models;
using LotComAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LotComAPI.Controllers;

/// <summary>
/// Provides RESTful operations on the Part database.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PartController : ControllerBase
{
    private readonly IPartService _partService;

    public PartController(IPartService PartService)
    {
        // confirm a PartService was injected
        if (PartService is null)
        {
            throw new ArgumentNullException(nameof(PartService));
        }
        _partService = PartService;
    }

    /// <summary>
    /// Processes a GET HTTP request for all of the Part objects in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public ActionResult<IEnumerable<PartDto>> GetAll()
    {
        IEnumerable<Part> PartsFromDatabase = _partService.GetAll();
        // convert each of the Print entities into a Dto
        IEnumerable<PartDto> Dtos = PartsFromDatabase
            .Select(PartMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for all of the Part objects printed by a Process Id in the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("printedById")]
    public ActionResult<IEnumerable<PartDto>> GetAllPrintedBy([FromQuery] int processId)
    {
        IEnumerable<Part> PartsFromDatabase = _partService.GetPrintedBy(processId);
        // convert each entity from Parts to Dtos
        IEnumerable<PartDto> Dtos = PartsFromDatabase
            .Select(PartMapper.EntityToDto);
        return Ok(Dtos);
    }
    
    /// <summary>
    /// Processes a GET HTTP request for all of the Part objects scanned by a Process Id in the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("scannedById")]
    public ActionResult<IEnumerable<PartDto>> GetAllScannedBy([FromQuery] int processId)
    {
        IEnumerable<Part> PartsFromDatabase = _partService.GetScannedBy(processId);
        // convert each entity from Parts to Dtos
        IEnumerable<PartDto> Dtos = PartsFromDatabase
            .Select(PartMapper.EntityToDto);
        return Ok(Dtos);
    }

    /// <summary>
    /// Processes a GET HTTP request for a single Part object in the database.
    /// </summary>
    /// <param name="id">The unique ID of the object that is requested.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<PartDto> Get(int id)
    {
        Part? PartFromDatabase = _partService.Get(id);
        if (PartFromDatabase is null)
        {
            return NotFound();
        }
        return Ok(PartMapper.EntityToDto(PartFromDatabase));
    }

    /// <summary>
    /// Processes a POST HTTP request to add a single Part object to the database. 
    /// </summary>
    /// <param name="Part">The Part object that will be POSTed to the database.</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<PartDto> Create([FromBody] PartDao Dao)
    {
        // map the new Part (as a DAO from the Data Access Layer) to an entity and add it to the Db
        Part Entity = PartMapper.DaoToEntity(Dao);
        Entity = _partService.Create(Entity);
        // remap the entity to a Dto to return its CreatedAtRoute status
        PartDto PartToReturn = PartMapper.EntityToDto(Entity);
        // send a CreatedAtRoute response with a 201 status code
        return new CreatedAtActionResult
        (
            nameof(Get),
            "Part",
            new { id = PartToReturn.Id },
            PartToReturn
        );
    }

    /// <summary>
    /// Processes a PUT HTTP request to update a single Part object in the database.
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] PartDao Dao)
    {
        // confirm an id was passed
        if (Dao is null || id != Dao.Id)
        {
            return BadRequest();
        }
        // update the Entity with the service
        bool Result = _partService.Update(id, PartMapper.DaoToEntity(Dao));
        if (!Result)
        {
            return NotFound();
        }
        _partService.Save();
        return NoContent();
    }

    /// <summary>
    /// Processes a DELETE HTTP request to remove a single Part object in the database.
    /// </summary>
    /// <param name="id">The unique ID of the object that is requested.</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Part? PartFromDatabase = _partService.Get(id);
        if (PartFromDatabase is null)
        {
            return NotFound();
        }
        _partService.Delete(PartFromDatabase);
        _partService.Save();
        return NoContent();
    }
}