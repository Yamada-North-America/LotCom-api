using LotComAPI.DbContexts;
using LotCom.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.Services;

/// <summary>
/// Provides logic layer for CRUD operations on the Serial Feed Database.
/// </summary>
public class SerialService : ISerialService
{
    /// <summary>
    /// A context that allows manipulation of the Serial Feed database.
    /// </summary>
   private readonly SerialContext _context;

    /// <summary>
    /// Creates a Service that enables RESTful operations on the Serial Feed database.
    /// </summary>
    /// <param name="Context"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public SerialService(SerialContext Context)
    {
        // confirm that a context was injected
        if (Context is null)
        {
            throw new ArgumentNullException(nameof(Context));
        }
        _context = Context;
    }

    /// <summary>
    /// Increments the JBK portion of the Serial Feed for PartId.
    /// </summary>
    /// <param name="PartId"></param>
    /// <returns></returns>
    private SerialSetEntity? UpdateNextJBK(int PartId)
    {
        // ensure a valid Part Id was passed
        if (PartId is < 1)
        {
            return null;
        }
        // retrieve the Serial Feed for the Part
        SerialSetEntity? SerialFromDatabase = _context.Serials
            .Where(x => x.PartId.Equals(PartId))
            .FirstOrDefault();
        if (SerialFromDatabase is null)
        {
            return null;
        }
        // either reset or increment the JBK number
        int NewJBK;
        if (SerialFromDatabase.NextJBK >= 999)
        {
            NewJBK = 1;
        }
        else
        {
            NewJBK = SerialFromDatabase.NextJBK + 1;
        }
        _context.Entry(SerialFromDatabase).Entity.NextJBK = NewJBK;
        SerialFromDatabase.NextJBK = NewJBK;
        // update the entity in the context and save changes
        _context.Entry(SerialFromDatabase).State = EntityState.Modified;
        Save();
        return _context.Entry(SerialFromDatabase).Entity;
    }

    /// <summary>
    /// Increments the Lot portion of the Serial Feed for PartId.
    /// </summary>
    /// <param name="PartId"></param>
    /// <returns></returns>
    private SerialSetEntity? UpdateNextLot(int PartId)
    {
        // ensure a valid Part Id was passed
        if (PartId is < 1)
        {
            return null;
        }
        // retrieve the Serial Feed for the Part
        SerialSetEntity? SerialFromDatabase = _context.Serials
            .Where(x => x.PartId.Equals(PartId))
            .FirstOrDefault();
        if (SerialFromDatabase is null)
        {
            return null;
        }
        // either reset or increment the Lot number
        int NewLot;
        if (SerialFromDatabase.NextLot >= 999999999)
        {
            NewLot = 1;
        }
        else
        {
            NewLot = SerialFromDatabase.NextLot + 1;
        }
        _context.Entry(SerialFromDatabase).Entity.NextLot = NewLot;
        SerialFromDatabase.NextLot = NewLot;
        // update the entity in the context and save changes
        _context.Entry(SerialFromDatabase).State = EntityState.Modified;
        Save();
        return _context.Entry(SerialFromDatabase).Entity;
    }

    /// <summary>
    /// Queries the Serial Feed Database for the next JBK Number assigned to the specified Part Id.
    /// </summary>
    /// <param name="PartId"></param>
    /// <returns></returns>
    public int? GetNextJBK(int PartId)
    {
        // ensure a valid Part Id was passed
        if (PartId is < 1)
        {
            return null;
        }
        // retrieve the Serial Feed from the Database
        SerialSetEntity? SerialFromDatabase = _context.Serials
            .Where(x => x.PartId.Equals(PartId))
            .FirstOrDefault();
        if (SerialFromDatabase is null)
        {
            return null;
        }
        // save the next JBK for the Part and increment the feed for the Part
        int NextJBK = SerialFromDatabase.NextJBK;
        UpdateNextJBK(PartId);
        return NextJBK;
    }

    
    /// <summary>
    /// Queries the Serial Feed Database for the next Lot Number assigned to the specified Part Id.
    /// </summary>
    /// <param name="PartId"></param>
    /// <returns></returns>
    public int? GetNextLot(int PartId)
    {
        // ensure a valid Part Id was passed
        if (PartId is < 1)
        {
            return null;
        }
        // retrieve the Serial Feed from the Database
        SerialSetEntity? SerialFromDatabase = _context.Serials
            .Where(x => x.PartId.Equals(PartId))
            .FirstOrDefault();
        if (SerialFromDatabase is null)
        {
            return null;
        }
        // save the next Lot for the Part and increment the feed for the Part
        int NextLot = SerialFromDatabase.NextLot;
        UpdateNextLot(PartId);
        return NextLot;
    }

    /// <summary>
    /// Saves all state changes to the Serial Feed Database.
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }
}