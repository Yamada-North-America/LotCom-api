using LotCom.DataAccess.Models;
using LotCom.DataAccess.Entities;
using LotCom.DataAccess.Mappers;
using LotComAPI.Services;
using Microsoft.AspNetCore.Mvc;
using LotCom.Types;

namespace LotComAPI.Controllers;

/// <summary>
/// Provides RESTful operations on the Part database.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PartController : ControllerBase
{
    private readonly IPartService _partService;
    
    private readonly IMapper<Part, PartEntity, PartDto> _partMapper;

    public PartController(IPartService PartService, IMapper<Part, PartEntity, PartDto> PartMapper)
    {
        // confirm a PartService was injected
        if (PartService is null)
        {
            throw new ArgumentNullException(nameof(PartService));
        }
        _partService = PartService;
        // confirm a PartMapper was injected
        if (PartMapper is null)
        {
            throw new ArgumentNullException(nameof(PartMapper));
        }
        _partMapper = PartMapper;
    }

    /// <summary>
    /// Processes a GET HTTP request for all of the Part objects in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    public ActionResult<IEnumerable<PartDto>> GetAll()
    {
        IEnumerable<PartEntity> PartsFromDatabase = _partService.GetAll();
        // convert each of the Print entities into a Dto
        IEnumerable<PartDto> Dtos = PartsFromDatabase
            .Select(_partMapper.EntityToDto);
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
        IEnumerable<PartEntity> PartsFromDatabase = _partService.GetPrintedBy(processId);
        // convert each entity from Parts to Dtos
        IEnumerable<PartDto> Dtos = PartsFromDatabase
            .Select(_partMapper.EntityToDto);
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
        IEnumerable<PartEntity> PartsFromDatabase = _partService.GetScannedBy(processId);
        // convert each entity from Parts to Dtos
        IEnumerable<PartDto> Dtos = PartsFromDatabase
            .Select(_partMapper.EntityToDto);
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
        PartEntity? PartFromDatabase = _partService.Get(id);
        if (PartFromDatabase is null)
        {
            return NotFound();
        }
        return Ok(_partMapper.EntityToDto(PartFromDatabase));
    }

    /// <summary>
    /// Processes a POST HTTP request to add a single Part object to the database. 
    /// </summary>
    /// <param name="Part">The Part object that will be POSTed to the database.</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<PartDto> Create([FromBody] PartDto Dto)
    {
        // map the new Part (as a Dto from the Data Access Layer) to an entity and add it to the Db
        PartEntity Entity = _partMapper.DtoToEntity(Dto);
        Entity = _partService.Create(Entity);
        // remap the entity to a Dto to return its CreatedAtRoute status
        PartDto PartToReturn = _partMapper.EntityToDto(Entity);
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
    public IActionResult Update(int id, [FromBody] PartDto Dto)
    {
        // confirm an id was passed
        if (Dto is null || id != Dto.Id)
        {
            return BadRequest();
        }
        // update the Entity with the service
        bool Result = _partService.Update(id, _partMapper.DtoToEntity(Dto));
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
        PartEntity? PartFromDatabase = _partService.Get(id);
        if (PartFromDatabase is null)
        {
            return NotFound();
        }
        _partService.Delete(PartFromDatabase);
        _partService.Save();
        return NoContent();
    }
}