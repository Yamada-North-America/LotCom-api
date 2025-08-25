using LotCom.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI.DbContexts;

/// <summary>
/// Creates a context on the Serial Number feeds Database table.
/// </summary>
public class SerialContext : DbContext
{
    /// <summary>
    /// Contains the current set of Serial Number feeds in the Database context.
    /// </summary>
    public DbSet<SerialSetEntity> Serials { get; set; }

    public SerialContext(DbContextOptions<SerialContext> Options) : base(Options)
    {
        if (Options is null)
        {
            throw new ArgumentNullException(nameof(Options));
        }
    }
}