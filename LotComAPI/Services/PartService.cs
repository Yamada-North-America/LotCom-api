using LotCom.Database.Entities;
using LotCom.Database.Mappers;
using LotCom.Database.Transfer;
using LotCom.Core.Models;
using LotCom.Core.Types;
using LotComAPI.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.Services;

/// <summary>
/// Provides logic layer for CRUD operations on the Part Database.
/// </summary>
public class PartService : IPartService
{
    /// <summary>
    /// A context ("Session") that allows manipulation of the Part Database.
    /// </summary>
    private readonly PartContext _partContext;

    /// <summary>
    /// A mapper that allows translation between Part classes.
    /// </summary>
    private readonly IMapper<Part, PartEntity, PartDto> _partMapper;

    /// <summary>
    /// Creates a new Service that enables RESTful operations on the Part Database.
    /// </summary>
    /// <param name="PartContext"></param>
    /// <param name="PartMapper"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PartService(PartContext PartContext, IMapper<Part, PartEntity, PartDto> PartMapper)
    {
        // confirm that a PartContext was injected
        if (PartContext is null)
        {
            throw new ArgumentNullException(nameof(PartContext));
        }
        _partContext = PartContext;
        // confirm that a PartMapper was injected
        if (PartMapper is null)
        {
            throw new ArgumentNullException(nameof(PartMapper));
        }
        _partMapper = PartMapper;
    }

    /// <summary>
    /// Queries all of the existing Parts from the Part Database.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<PartEntity> GetAll()
    {
        return _partContext.Parts;
    }

    /// <summary>
    /// Queries all of the Parts that can have Labels Printed by ProcessId.
    /// </summary>
    /// <param name="ProcessId"></param>
    /// <returns></returns>
    public IEnumerable<PartEntity> GetPrintedBy(int ProcessId)
    {
        return _partContext.Parts
            .Where(x => x.PrintedBy.Equals(ProcessId));
    }

    /// <summary>
    /// Queries all of the Parts that can have Labels Scanned by ProcessId.
    /// </summary>
    /// <param name="ProcessId"></param>
    /// <returns></returns>
    public IEnumerable<PartEntity> GetScannedBy(int ProcessId)
    {
        return _partContext.Parts
            .Where(x => x.ScannedBy.Equals(ProcessId));
    }

    /// <summary>
    /// Queries a specific Part from the Database by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public PartEntity? Get(int id)
    {
        // confirm that a valid id was passed
        if (id < 0)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return _partContext.Parts
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();
    }

    /// <summary>
    /// Creates a new Part in the Database.
    /// </summary>
    /// <param name="Print"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PartEntity Create(PartEntity Part)
    {
        // confirm a Part is passed
        if (Part is null)
        {
            throw new ArgumentNullException(nameof(Part));
        }
        Part.Created = new Timestamp(DateTime.Now).Stamp;
        Part.Updated = Part.Created;
        // add the Part to the DbSet and set its state
        _partContext.Parts.Add(Part);
        _partContext.Entry(Part).State = EntityState.Added;
        // save the DbContext and return the newly added entity
        Save();
        return _partContext.Entry(Part).Entity;
    }

    /// <summary>
    /// Updates an existing Part in the Database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Part"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public bool Update(int id, PartEntity Part)
    {
        // confirm a Part is passed
        if (Part is null)
        {
            throw new ArgumentNullException(nameof(Part));
        }
        // confirm that the Part exists in the Database
        PartEntity? PartFromDatabase = Get(id);
        if (PartFromDatabase is null)
        {
            return false;
        }
        // update the entry in context
        _partMapper.UpdateEntity(PartFromDatabase, Part);
        PartFromDatabase.Updated = new Timestamp(DateTime.Now).Stamp;
        _partContext.Entry(PartFromDatabase).State = EntityState.Modified;
        return true;
    }

    /// <summary>
    /// Removes an existing Part from the Database.
    /// </summary>
    /// <param name="Print"></param>
    public void Delete(PartEntity Part)
    {
        // remove the Part from the DbSet and set its state
        _partContext.Parts.Remove(Part);
        _partContext.Entry(Part).State = EntityState.Deleted;
    }


    /// <summary>
    /// Saves all changes in the PartContext's Parts DbSet to the Database.
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        return _partContext.SaveChanges() >= 0;
    }
}