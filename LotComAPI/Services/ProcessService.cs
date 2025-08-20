using LotComAPI.DbContexts;
using LotComAPI.Entities;
using LotComAPI.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.Services;

/// <summary>
/// Provides logic layer for CRUD operations on the Process Database.
/// </summary>
public class ProcessService : IProcessService
{
    /// <summary>
    /// A context ("Session") that allows manipulation of the Process Database.
    /// </summary>
    private readonly ProcessContext _context;

    /// <summary>
    /// Creates a new Service that enables RESTful operations on the Process Database.
    /// </summary>
    /// <param name="Context"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ProcessService(ProcessContext Context)
    {
        // confirm that there is a context injected
        if (Context is null)
        {
            throw new ArgumentNullException(nameof(Context));
        }
        _context = Context;
    }

    /// <summary>
    /// Queries all of the existing Processes from the Process Database.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProcessEntity> GetAll()
    {
        return _context.Processes;
    }


    public IEnumerable<ProcessEntity> GetAllFromStoredProcedure()
    {
        IEnumerable<ProcessEntity> ProcessesFromSP = _context
            .Set<ProcessEntity>()
            .FromSql($"EXEC dbo.GetAllProcesses");
        return ProcessesFromSP;
    }

    /// <summary>
    /// Queries a specific Process from the Database by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ProcessEntity? Get(int id)
    {
        // confirm that a valid id was passed
        if (id < 1)
        {
            return null;
        }
        return _context.Processes
            .Where(x => x.Id.Equals(id))
            .FirstOrDefault();
    }

    /// <summary>
    /// Creates a new Process in the Database.
    /// </summary>
    /// <param name="Entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public ProcessEntity Create(ProcessEntity Entity)
    {
        // confirm that a Process was passed
        if (Entity is null)
        {
            throw new ArgumentNullException(nameof(Entity));
        }
        // configure db timestamps
        Entity.Created = new LotCom.Types.Timestamp(DateTime.Now).Stamp;
        Entity.Updated = Entity.Created;
        // create the new entry and save to the Db
        _context.Processes.Add(Entity);
        _context.Processes.Entry(Entity).State = EntityState.Added;
        Save();
        // return the newly created entity for CreatedAtRoute
        return _context.Entry(Entity).Entity;
    }

    /// <summary>
    /// Updates an existing Process in the Database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Process"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public bool Update(int id, ProcessEntity Process)
    {
        // confirm a Process is passed
        if (Process is null)
        {
            throw new ArgumentNullException(nameof(Process));
        }
        // confirm that the Process exists in the Database
        ProcessEntity? ProcessFromDatabase = Get(id);
        if (ProcessFromDatabase is null)
        {
            return false;
        }
        // update the entry in context
        ProcessMapper.EntityToEntity(ProcessFromDatabase, Process);
        ProcessFromDatabase.Updated = new LotCom.Types.Timestamp(DateTime.Now).Stamp;
        _context.Entry(ProcessFromDatabase).State = EntityState.Modified;
        return true;
    }

    /// <summary>
    /// Removes an existing Process from the Database.
    /// </summary>
    /// <param name="Entity"></param>
    public void Delete(ProcessEntity Entity)
    {
        _context.Processes.Remove(Entity);
        _context.Entry(Entity).State = EntityState.Deleted;
    }

    /// <summary>
    /// Saves all changes in the ProcessContext's Processes DbSet to the Database.
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        int Result = _context.SaveChanges();
        return Result >= 0;
    }
}