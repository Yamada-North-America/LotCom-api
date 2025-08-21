using LotComAPI.DbContexts;
using LotCom.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using LotCom.DataAccess.Mappers;
using LotCom.Types;
using LotCom.DataAccess.Models;

namespace LotComAPI.Services;

/// <summary>
/// Provides logic layer for CRUD operations on the Print Database.
/// </summary>
public class PrintService : IPrintService
{
    /// <summary>
    /// A context ("Session") that allows manipulation of the Print Database.
    /// </summary>
    private readonly PrintContext _printContext;

    /// <summary>
    /// A mapper that allows translation between Print classes.
    /// </summary>
    private readonly IMapper<Print, PrintEntity, PrintDto> _printMapper;

    /// <summary>
    /// Creates a new Service that enables RESTful operations on the Print Database.
    /// </summary>
    /// <param name="PrintContext"></param>
    /// <param name="PrintMapper"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PrintService(PrintContext PrintContext, IMapper<Print, PrintEntity, PrintDto> PrintMapper)
    {
        // confirm that a PrintContext was injected
        if (PrintContext is null)
        {
            throw new ArgumentNullException(nameof(PrintContext));
        }
        _printContext = PrintContext;
        // confirm that a PrintMapper was injected
        if (PrintMapper is null)
        {
            throw new ArgumentNullException(nameof(PrintMapper));
        }
        _printMapper = PrintMapper;
    }

    /// <summary>
    /// Queries all of the existing Prints from the Print Database.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<PrintEntity> GetAll()
    {
        return _printContext.Prints;
    }

    /// <summary>
    /// Queries all of the existing Prints that occured on a specified Date from the Print Database.
    /// </summary>
    /// <param name="Date"></param>
    /// <returns></returns>
    public IEnumerable<PrintEntity> GetOnDate(DateTime Date)
    {
        string QueryDate = new LotCom.Types.Timestamp(Date).Stamp.Split("-")[0];
        return _printContext.Prints
            .Where(x => x.ProductionDate.Contains(QueryDate));
    }

    /// <summary>
    /// Queries all of the existing Prints produced by a sepcific Process on a specified Date from the Print Database.
    /// </summary>
    /// <param name="Date"></param>
    /// <param name="Process"></param>
    /// <returns></returns>
    public IEnumerable<PrintEntity> GetOnDateByProcess(DateTime Date, int ProcessId)
    {
        return GetOnDate(Date)
            .Where(x => x.ProcessId.Equals(ProcessId));
    }

    /// <summary>
    /// Queries a specific Print from the Database by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public PrintEntity? Get(int id)
    {
        // confirm that a valid id was passed
        if (id < 0)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return _printContext.Prints
            .Where(x => x.Id.Equals(id))
            .FirstOrDefault();
    }

    /// <summary>
    /// Creates a new Print in the Database.
    /// </summary>
    /// <param name="Print"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PrintEntity Create(PrintEntity Print)
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
        _printContext.Prints.Add(Print);
        _printContext.Entry(Print).State = EntityState.Added;
        // save the DbContext and return the newly added entity
        Save();
        return _printContext.Entry(Print).Entity;
    }

    /// <summary>
    /// Updates an existing Print in the Database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Print"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public bool Update(int id, PrintEntity Print)
    {
        // confirm a Print is passed
        if (Print is null)
        {
            throw new ArgumentNullException(nameof(Print));
        }
        // confirm that the Print exists in the Database
        PrintEntity? PrintFromDatabase = Get(id);
        if (PrintFromDatabase is null)
        {
            return false;
        }
        // update the entry in context
        _printMapper.UpdateEntity(PrintFromDatabase, Print);
        PrintFromDatabase.Updated = new Timestamp(DateTime.Now).Stamp;
        _printContext.Entry(PrintFromDatabase).State = EntityState.Modified;
        return true;
    }

    /// <summary>
    /// Removes an existing Print from the Database.
    /// </summary>
    /// <param name="Print"></param>
    public void Delete(PrintEntity Print)
    {
        // remove the Print from the DbSet and set its state
        _printContext.Prints.Remove(Print);
        _printContext.Entry(Print).State = EntityState.Deleted;
    }

    /// <summary>
    /// Saves all changes in the PrintContext's Prints DbSet to the Database.
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        int Result = _printContext.SaveChanges();
        return Result >= 0;
    }
}