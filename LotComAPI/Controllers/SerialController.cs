using LotComAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LotComAPI.Controllers;

/// <summary>
/// Provides RESTful operations on the Serial Feed Database.
/// </summary>
[ApiController]
[Route("[controller]")]
public class SerialController : ControllerBase
{
    private readonly ISerialService _serialService;

    public SerialController(ISerialService Service)
    {
        // confirm a SerialService was injected
        if (Service is null)
        {
            throw new ArgumentNullException(nameof(Service));
        }
        _serialService = Service;
    }

    /// <summary>
    /// Processes a GET HTTP request for the next JBK Number for a Part in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet("consumeJBKFor")]
    public ActionResult<int> GetNextJBK([FromQuery] int partId)
    {
        int? NumberFromDatabase = _serialService.GetNextJBK(partId);
        // ensure the requested Part has a Serial Feed
        if (NumberFromDatabase is null)
        {
            return NotFound();
        }
        return Ok(NumberFromDatabase);
    }

    /// <summary>
    /// Processes a GET HTTP request for the next Lot Number for a Part in the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet("consumeLotFor")]
    public ActionResult<int> GetNextLot([FromQuery] int partId)
    {
        int? NumberFromDatabase = _serialService.GetNextLot(partId);
        // ensure the requested Part has a Serial Feed
        if (NumberFromDatabase is null)
        {
            return NotFound();
        }
        return Ok(NumberFromDatabase);
    }
}