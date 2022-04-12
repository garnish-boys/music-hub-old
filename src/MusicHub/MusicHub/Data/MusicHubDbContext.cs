using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data
{
    public class MusicHubDbContext : IdentityDbContext<User, Role, string>
    {
        public MusicHubDbContext()
        {
        }

        public MusicHubDbContext(DbContextOptions<MusicHubDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conStr = "Server=192.168.0.192; Database=MusicHubIdentity; Uid=AppUser; Pwd=Password1;";
            var serverVersion = new MySqlServerVersion(new Version(10, 6, 7));

            optionsBuilder.UseMySql(conStr, serverVersion)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
                

            //optionsBuilder.UseSqlServer("Server=192.168.0.205; Database=MusicHubDb; User Id=app-user; Password=GarnishBoys215;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
