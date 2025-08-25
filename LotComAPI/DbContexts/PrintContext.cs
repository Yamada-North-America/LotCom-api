using LotCom.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.DbContexts;

/// <summary>
/// Creates a context ("session") on the Prints Database table.
/// </summary>
public class PrintContext : DbContext
{
    /// <summary>
    /// Contains the current set of Prints in the Database context.
    /// </summary>
    public DbSet<PrintEntity> Prints { get; set; }

    public PrintContext(DbContextOptions<PrintContext> Options) : base(Options)
    {
        if (Options is null)
        {
            throw new ArgumentNullException(nameof(Options));
        }
    }
}