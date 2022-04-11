using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
namespace MusicHub.IdentityServer.Data;

public class IdentityConfigDbContext : ConfigurationDbContext<IdentityConfigDbContext>
{
    public IdentityConfigDbContext(DbContextOptions<IdentityConfigDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=192.168.0.205; Database=IdentityConfigDb; User Id=app-user; Password=GarnishBoys215;");
    }
}

