using LotCom.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.DbContexts;

/// <summary>
/// Creates a context ("session") on the Parts Database table.
/// </summary>
public class PartContext : DbContext
{
    /// <summary>
    /// Contains the current set of Parts in the Database context.
    /// </summary>
    public DbSet<PartEntity> Parts { get; set; }

    public PartContext(DbContextOptions<PartContext> Options) : base(Options)
    {
        if (Options is null)
        {
            throw new ArgumentNullException(nameof(Options));
        }
    }
}