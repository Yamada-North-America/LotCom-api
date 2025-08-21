using LotComAPI.DbContexts;
using LotCom.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using LotCom.DataAccess.Mappers;
using LotCom.Types;
using LotCom.DataAccess.Models;

namespace LotComAPI.Services;

/// <summary>
/// Provides logic layer for CRUD operations on the Scan Database.
/// </summary>
public class ScanService : IScanService
{
    /// <summary>
    /// A context ("Session") that allows manipulation of the Scan Database.
    /// </summary>
    private readonly ScanContext _scanContext;

    /// <summary>
    /// A mapper that allows translation between Scan classes.
    /// </summary>
    private readonly IMapper<Scan, ScanEntity, ScanDto> _scanMapper;

    /// <summary>
    /// Creates a new Service that enables RESTful operations on the Scan Database.
    /// </summary>
    /// <param name="ScanContext"></param>
    /// <param name="ScanMapper"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ScanService(ScanContext ScanContext, IMapper<Scan, ScanEntity, ScanDto> ScanMapper)
    {
        // confirm that a ScanContext was injected
        if (ScanContext is null)
        {
            throw new ArgumentNullException(nameof(ScanContext));
        }
        _scanContext = ScanContext;
        // confirm that a ScanMapper was injected
        if (ScanMapper is null)
        {
            throw new ArgumentNullException(nameof(ScanMapper));
        }
        _scanMapper = ScanMapper;
    }

    /// <summary>
    /// Queries all of the existing Scans from the Scan database.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ScanEntity> GetAll()
    {
        return _scanContext.Scans;
    }

    /// <summary>
    /// Queries a specific Scan from the Database by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public ScanEntity? Get(int id)
    {
        // confirm that a valid id was passed
        if (id < 1)
        {
            return null;
        }
        return _scanContext.Scans
            .Where(x => x.Id.Equals(id))
            .FirstOrDefault();
    }

    /// <summary>
    /// Queries all of the existing Scans that occurred within a given range (from the current date) from the Scan database.
    /// </summary>
    /// <param name="days">Must be at least 0 (non-negative).</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public IEnumerable<ScanEntity>? GetAllWithinRange(int days)
    {
        // confirm that a valid day range was passed
        if (days < 0)
        {
            return null;
        }
        return _scanContext.Scans.AsEnumerable()
            .Where(x => x.CompareDateWithinRange(days, DateTime.Now));
    }

    /// <summary>
    /// Creates a new Scan in the Database.
    /// </summary>
    /// <param name="Entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public ScanEntity Create(ScanEntity Entity)
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
        _scanContext.Scans.Add(Entity);
        _scanContext.Scans.Entry(Entity).State = EntityState.Added;
        Save();
        // return the newly created entity for CreatedAtRoute
        return _scanContext.Entry(Entity).Entity;
    }

    /// <summary>
    /// Updates an existing Scan in the Database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Scan"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public bool Update(int id, ScanEntity Scan)
    {
        // confirm a Scan is passed
        if (Scan is null)
        {
            throw new ArgumentNullException(nameof(Scan));
        }
        // confirm that the Scan exists in the Database
        ScanEntity? ScanFromDatabase = Get(id);
        if (ScanFromDatabase is null)
        {
            return false;
        }
        // update the entry in context
        _scanMapper.UpdateEntity(ScanFromDatabase, Scan);
        ScanFromDatabase.Updated = new Timestamp(DateTime.Now).Stamp;
        _scanContext.Entry(ScanFromDatabase).State = EntityState.Modified;
        return true;
    }

    /// <summary>
    /// Removes an existing Scan from the Database.
    /// </summary>
    /// <param name="Entity"></param>
    public void Delete(ScanEntity Entity)
    {
        _scanContext.Scans.Remove(Entity);
        _scanContext.Entry(Entity).State = EntityState.Deleted;
    }

    /// <summary>
    /// Saves all changes in the ScanContext's Scans DbSet to the Database.
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        return _scanContext.SaveChanges() >= 0;
    }
}