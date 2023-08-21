using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.DataAccess;

public class QimiaSchoolDbContextFactory : IDesignTimeDbContextFactory<QimiaSchoolDbContext>
{
    public QimiaSchoolDbContext CreateDbContext(string[] args)
    {
        if (args.Length < 1)
        {
            throw new ArgumentException("Missing connection string");
        }

        var connectionString = args[0];

        var builder = new DbContextOptionsBuilder<QimiaSchoolDbContext>()
            .UseSqlServer(connectionString);

        return new QimiaSchoolDbContext(builder.Options);
    }
}