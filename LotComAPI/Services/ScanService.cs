using LotComAPI.DbContexts;
using LotComAPI.Entities;
using LotComAPI.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.Services;

/// <summary>
/// Provides logic layer for CRUD operations on the Scan Database.
/// </summary>
public class ScanService : IScanService
{
    /// <summary>
    /// A context that allows manipulation of the Scan database.
    /// </summary>
    private readonly ScanContext _context;

    /// <summary>
    /// Creates a new Service that enables RESTful operations on the Scan database.
    /// </summary>
    /// <param name="Context"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ScanService(ScanContext Context)
    {
        // confirm that a context was injected
        if (Context is null)
        {
            throw new ArgumentNullException(nameof(Context));
        }
        _context = Context;
    }

    /// <summary>
    /// Queries all of the existing Scans from the Scan database.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Scan> GetAll()
    {
        return _context.Scans;
    }

    /// <summary>
    /// Queries a specific Scan from the Database by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Scan? Get(int id)
    {
        // confirm that a valid id was passed
        if (id < 1)
        {
            return null;
        }
        return _context.Scans
            .Where(x => x.Id.Equals(id))
            .FirstOrDefault();
    }

    /// <summary>
    /// Creates a new Scan in the Database.
    /// </summary>
    /// <param name="Entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Scan Create(Scan Entity)
    {
        // confirm that a Scan was passed
        if (Entity is null)
        {
            throw new ArgumentNullException(nameof(Entity));
        }
        // configure db timestamps
        Entity.Created = new LotCom.Types.Timestamp(DateTime.Now).Stamp;
        Entity.Updated = Entity.Created;
        // create the new entry and save to the Db
        _context.Scans.Add(Entity);
        _context.Scans.Entry(Entity).State = EntityState.Added;
        Save();
        // return the newly created entity for CreatedAtRoute
        return _context.Entry(Entity).Entity;
    }

    /// <summary>
    /// Updates an existing Scan in the Database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Scan"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public bool Update(int id, Scan Scan)
    {
        // confirm a Scan is passed
        if (Scan is null)
        {
            throw new ArgumentNullException(nameof(Scan));
        }
        // confirm that the Scan exists in the Database
        Scan? ScanFromDatabase = Get(id);
        if (ScanFromDatabase is null)
        {
            return false;
        }
        // update the entry in context
        ScanMapper.EntityToEntity(ScanFromDatabase, Scan);
        ScanFromDatabase.Updated = new LotCom.Types.Timestamp(DateTime.Now).Stamp;
        _context.Entry(ScanFromDatabase).State = EntityState.Modified;
        return true;
    }

    /// <summary>
    /// Removes an existing Scan from the Database.
    /// </summary>
    /// <param name="Entity"></param>
    public void Delete(Scan Entity)
    {
        _context.Scans.Remove(Entity);
        _context.Entry(Entity).State = EntityState.Deleted;
    }

    /// <summary>
    /// Saves all changes in the ScanContext's Scans DbSet to the Database.
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }
}