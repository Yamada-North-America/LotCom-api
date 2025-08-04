using LotComAPI.DbContexts;
using LotComAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.Services;

/// <summary>
/// Provides logic layer for CRUD operations on the Print Database.
/// </summary>
public class PrintService : IPrintService
{
    /// <summary>
    /// A context ("Session") that allows manipulation of the Print Database.
    /// </summary>
    private readonly PrintContext _context;

    /// <summary>
    /// Creates a new Service that enables RESTful operations on the Print Database.
    /// </summary>
    /// <param name="Context"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PrintService(PrintContext Context)
    {
        // confirm that the context passed to the service exists
        if (Context is null)
        {
            throw new ArgumentNullException(nameof(Context));
        }
        _context = Context;
    }

    /// <summary>
    /// Queries all of the existing Prints from the Print Database.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Print> GetAll()
    {
        return _context.Prints;
    }

    /// <summary>
    /// Queries a specific Print from the Database by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Print? Get(int id)
    {
        // confirm that a valid id was passed
        if (id < 0)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return _context.Prints
            .Where(x => x.Id.Equals(id))
            .FirstOrDefault();
    }

    /// <summary>
    /// Creates a new Print in the Database.
    /// </summary>
    /// <param name="Print"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Print Create(Print Print)
    {
        // confirm a Print is passed
        if (Print is null)
        {
            throw new ArgumentNullException(nameof(Print));
        }
        // set the Print's timestamps
        Print.Created = new LotCom.Types.Timestamp(DateTime.Now).Stamp;
        Print.Updated = Print.Created;
        // add the Print to the DbSet and set its state
        _context.Prints.Add(Print);
        _context.Entry(Print).State = EntityState.Added;
        // save the DbContext and return the newly added entity
        Save();
        return _context.Entry(Print).Entity;
    }

    /// <summary>
    /// Updates an existing Print in the Database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Print"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public void Update(int id, Print Print)
    {
        // confirm that a valid id was passed
        if (id > 0)
        {
            throw new ArgumentNullException(nameof(id));
        }
        // confirm a Print is passed
        if (Print is null)
        {
            throw new ArgumentNullException(nameof(Print));
        }
        throw new NotImplementedException();
        // Print.Updated = new Timestamp(DateTime.Now).Stamp;
        // _context.Entry(Print).State = EntityState.Modified
    }

    /// <summary>
    /// Removes an existing Print from the Database.
    /// </summary>
    /// <param name="Print"></param>
    public void Delete(Print Print)
    {
        // remove the Print from the DbSet and set its state
        _context.Prints.Remove(Print);
        _context.Entry(Print).State = EntityState.Deleted;
    }

    /// <summary>
    /// Saves all changes in the PrintContext's Prints DbSet to the Database.
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        int Result = _context.SaveChanges();
        return Result >= 0;
    }
}