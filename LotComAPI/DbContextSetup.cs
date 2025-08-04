using LotComAPI.DbContexts;
using LotComAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace LotComAPI;

public static class DbContextSetup
{
    /// <summary>
    /// Adds DI for all of the LotCom Database dependencies.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder InjectDatabaseContext(WebApplicationBuilder builder)
    {
        // add the Services and DbContexts as app services
        builder.Services.AddScoped<IPrintService, PrintService>();
        builder.Services.AddDbContext<PrintContext>
        (
            options =>
            {
                options.UseSqlServer
                (
                    builder.Configuration.GetConnectionString("YNA")
                );
            }
        );
        builder.Services.AddScoped<IScanService, ScanService>();
        builder.Services.AddDbContext<ScanContext>
        (
            options =>
            {
                options.UseSqlServer
                (
                    builder.Configuration.GetConnectionString("YNA")
                );
            }
        );
        builder.Services.AddScoped<IProcessService, ProcessService>();
        builder.Services.AddDbContext<ProcessContext>
        (
            options =>
            {
                options.UseSqlServer
                (
                    builder.Configuration.GetConnectionString("YNA")
                );
            }
        );
        builder.Services.AddScoped<IPartService, PartService>();
        builder.Services.AddDbContext<PartContext>
        (
            options =>
            {
                options.UseSqlServer
                (
                    builder.Configuration.GetConnectionString("YNA")
                );
            }
        );
        builder.Services.AddScoped<ISerialService, SerialService>();
        builder.Services.AddDbContext<SerialContext>
        (
            options =>
            {
                options.UseSqlServer
                (
                    builder.Configuration.GetConnectionString("YNA")
                );
            }
        );
        return builder;
    }

    /// <summary>
    /// Ensures that each of the DbContext services are able to connect to the Database.
    /// </summary>
    /// <param name="Scope"></param>
    public static void EnsureDatabaseConnection(IServiceScope Scope)
    {
        // create DbContexts and ensure connections
        PrintContext _printContext = Scope.ServiceProvider.GetRequiredService<PrintContext>();
        _printContext.Database.EnsureCreated();
        ScanContext _scanContext = Scope.ServiceProvider.GetRequiredService<ScanContext>();
        _scanContext.Database.EnsureCreated();
        ProcessContext _processContext = Scope.ServiceProvider.GetRequiredService<ProcessContext>();
        _processContext.Database.EnsureCreated();
        PartContext _partContext = Scope.ServiceProvider.GetRequiredService<PartContext>();
        _partContext.Database.EnsureCreated();
        SerialContext _serialContext = Scope.ServiceProvider.GetRequiredService<SerialContext>();
        _serialContext.Database.EnsureCreated();
    }
}