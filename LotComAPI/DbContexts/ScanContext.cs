using LotComAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.DbContexts;

/// <summary>
/// Creates a contextr ("session") on the Scans Database table.
/// </summary>
public class ScanContext : DbContext
{
    /// <summary>
    /// Contains the current set of Scans in the Database context.
    /// </summary>
    public DbSet<ScanEntity> Scans { get; set; }

    public ScanContext(DbContextOptions<ScanContext> Options) : base(Options)
    {
        if (Options is null)
        {
            throw new ArgumentNullException(nameof(Options));
        }
    }
}