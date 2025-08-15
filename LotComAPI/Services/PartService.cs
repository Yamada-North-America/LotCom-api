using LotComAPI.Entities;
using LotComAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using LotComAPI.Mappers;

namespace LotComAPI.Services;

/// <summary>
/// Provides logic layer for CRUD operations on the Part Database.
/// </summary>
public class PartService : IPartService
{
    /// <summary>
    /// A context ("Session") that allows manipulation of the Part Database.
    /// </summary>
    private readonly PartContext _context;

    /// <summary>
    /// Creates a new Service that enables RESTful operations on the Part Database.
    /// </summary>
    /// <param name="Context"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PartService(PartContext Context)
    {
        // confirm that the context passed to the service exists
        if (Context is null)
        {
            throw new ArgumentNullException(nameof(Context));
        }
        _context = Context;
    }

    /// <summary>
    /// Queries all of the existing Parts from the Part Database.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Part> GetAll()
    {
        return _context.Parts;
    }

    /// <summary>
    /// Queries all of the Parts that can have Labels Printed by ProcessId.
    /// </summary>
    /// <param name="ProcessId"></param>
    /// <returns></returns>
    public IEnumerable<Part> GetPrintedBy(int ProcessId)
    {
        return _context.Parts
            .Where(x => x.PrintedBy.Equals(ProcessId));
    }

    /// <summary>
    /// Queries all of the Parts that can have Labels Scanned by ProcessId.
    /// </summary>
    /// <param name="ProcessId"></param>
    /// <returns></returns>
    public IEnumerable<Part> GetScannedBy(int ProcessId)
    {
        return _context.Parts
            .Where(x => x.ScannedBy.Equals(ProcessId));
    }

    /// <summary>
    /// Queries a specific Part from the Database by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Part? Get(int id)
    {
        // confirm that a valid id was passed
        if (id < 0)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return _context.Parts
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();
    }

    /// <summary>
    /// Creates a new Part in the Database.
    /// </summary>
    /// <param name="Print"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Part Create(Part Part)
    {
        // confirm a Part is passed
        if (Part is null)
        {
            throw new ArgumentNullException(nameof(Part));
        }
        Part.Created = new LotCom.Types.Timestamp(DateTime.Now).Stamp;
        Part.Updated = Part.Created;
        // add the Part to the DbSet and set its state
        _context.Parts.Add(Part);
        _context.Entry(Part).State = EntityState.Added;
        // save the DbContext and return the newly added entity
        Save();
        return _context.Entry(Part).Entity;
    }

    /// <summary>
    /// Updates an existing Part in the Database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Part"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public bool Update(int id, Part Part)
    {
        // confirm a Part is passed
        if (Part is null)
        {
            throw new ArgumentNullException(nameof(Part));
        }
        // confirm that the Part exists in the Database
        Part? PartFromDatabase = Get(id);
        if (PartFromDatabase is null)
        {
            return false;
        }
        // update the entry in context
        PartMapper.EntityToEntity(PartFromDatabase, Part);
        PartFromDatabase.Updated = new LotCom.Types.Timestamp(DateTime.Now).Stamp;
        _context.Entry(PartFromDatabase).State = EntityState.Modified;
        return true;
    }

    /// <summary>
    /// Removes an existing Part from the Database.
    /// </summary>
    /// <param name="Print"></param>
    public void Delete(Part Part)
    {
        // remove the Part from the DbSet and set its state
        _context.Parts.Remove(Part);
        _context.Entry(Part).State = EntityState.Deleted;
    }


    /// <summary>
    /// Saves all changes in the PartContext's Parts DbSet to the Database.
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }
}