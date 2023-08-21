using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QimiaSchool.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.IntegrationTests;


public class CustomWebApplicationFactory : WebApplicationFactory<Program> // this will create an instance of our application for testing.
{
    private readonly string _connectionString;

    public CustomWebApplicationFactory(string connectionString)
    {
        _connectionString = connectionString; // it takes a connecitonString parameter and use it to configure our database context.
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<QimiaSchoolDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
                // this will remove any database configuration so we can provide it with our own.
            }

            services.AddDbContext<QimiaSchoolDbContext>(options => options.UseSqlServer(_connectionString));
        });
    }
}
