using LotComAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.DbContexts;

/// <summary>
/// Creates a context ("session") on the Prints Database table.
/// </summary>
public class ProcessContext : DbContext
{
    /// <summary>
    /// Contains the current set of Prints in the Database context.
    /// </summary>
    public DbSet<Process> Processes { get; set; }

    public ProcessContext(DbContextOptions<ProcessContext> Options) : base(Options)
    {
        if (Options is null)
        {
            throw new ArgumentNullException(nameof(Options));
        }
    }
}