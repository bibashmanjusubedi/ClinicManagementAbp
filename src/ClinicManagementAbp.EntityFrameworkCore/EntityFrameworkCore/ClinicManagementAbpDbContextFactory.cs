using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ClinicManagementAbp.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class ClinicManagementAbpDbContextFactory : IDesignTimeDbContextFactory<ClinicManagementAbpDbContext>
{
    public ClinicManagementAbpDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        ClinicManagementAbpEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<ClinicManagementAbpDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new ClinicManagementAbpDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ClinicManagementAbp.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
