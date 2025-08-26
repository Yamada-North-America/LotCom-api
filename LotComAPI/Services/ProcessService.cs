using LotComAPI.DbContexts;
using LotCom.Database.Entities;
using Microsoft.EntityFrameworkCore;
using LotCom.Database.Mappers;
using LotCom.Core.Models;
using LotCom.Database.Transfer;
using LotCom.Core.Types;

namespace LotComAPI.Services;

/// <summary>
/// Provides logic layer for CRUD operations on the Process Database.
/// </summary>
public class ProcessService : IProcessService
{
    /// <summary>
    /// A context ("Session") that allows manipulation of the Process Database.
    /// </summary>
    private readonly ProcessContext _processContext;

    /// <summary>
    /// A mapper that allows translation between Process classes.
    /// </summary>
    private readonly IMapper<Process, ProcessEntity, ProcessDto> _processMapper;

    /// <summary>
    /// Creates a new Service that enables RESTful operations on the Process Database.
    /// </summary>
    /// <param name="ProcessContext"></param>
    /// <param name="ProcessMapper"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ProcessService(ProcessContext ProcessContext, IMapper<Process, ProcessEntity, ProcessDto> ProcessMapper)
    {
        // confirm that a ProcessContext was injected
        if (ProcessContext is null)
        {
            throw new ArgumentNullException(nameof(ProcessContext));
        }
        _processContext = ProcessContext;
        // confirm that a ProcessMapper was injected
        if (ProcessMapper is null)
        {
            throw new ArgumentNullException(nameof(ProcessMapper));
        }
        _processMapper = ProcessMapper;
    }

    /// <summary>
    /// Queries all of the existing Processes from the Process Database.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProcessEntity> GetAll()
    {
        return _processContext.Processes;
    }

    /// <summary>
    /// Queries a specific Process from the Database by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ProcessEntity? Get(int id)
    {
        // confirm that a valid id was passed
        if (id < 0)
        {
            return null;
        }
        return _processContext.Processes
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
        Entity.Created = new Timestamp(DateTime.Now).Stamp;
        Entity.Updated = Entity.Created;
        // create the new entry and save to the Db
        _processContext.Processes.Add(Entity);
        _processContext.Processes.Entry(Entity).State = EntityState.Added;
        Save();
        // return the newly created entity for CreatedAtRoute
        return _processContext.Entry(Entity).Entity;
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
        _processMapper.UpdateEntity(ProcessFromDatabase, Process);
        ProcessFromDatabase.Updated = new Timestamp(DateTime.Now).Stamp;
        _processContext.Entry(ProcessFromDatabase).State = EntityState.Modified;
        return true;
    }

    /// <summary>
    /// Removes an existing Process from the Database.
    /// </summary>
    /// <param name="Entity"></param>
    public void Delete(ProcessEntity Entity)
    {
        _processContext.Processes.Remove(Entity);
        _processContext.Entry(Entity).State = EntityState.Deleted;
    }

    /// <summary>
    /// Saves all changes in the ProcessContext's Processes DbSet to the Database.
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        int Result = _processContext.SaveChanges();
        return Result >= 0;
    }
}