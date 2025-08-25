using LotCom.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.DbContexts;

/// <summary>
/// Creates a context ("session") on the Process Database table.
/// </summary>
public class ProcessContext : DbContext
{
    /// <summary>
    /// Contains the current set of Processes in the Database context.
    /// </summary>
    public DbSet<ProcessEntity> Processes { get; set; }

    public ProcessContext(DbContextOptions<ProcessContext> Options) : base(Options)
    {
        if (Options is null)
        {
            throw new ArgumentNullException(nameof(Options));
        }
    }
}