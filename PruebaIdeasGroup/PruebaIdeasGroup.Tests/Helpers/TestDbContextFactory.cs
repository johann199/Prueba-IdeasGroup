using Microsoft.EntityFrameworkCore;
using PruebaIdeasGroup.Infrastructure.Data;

namespace PruebaIdeasGroup.Tests.Helpers;

public static class TestDbContextFactory
{
    public static ApplicationDbContext CreateInMemoryContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new ApplicationDbContext(options);
    }
}