using Entites.Models;
using Entites.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext :DbContext
    {
        public RepositoryContext(DbContextOptions options): base(options){}

        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<JobHeader> jobHeaders { get; set; }
        public DbSet<JobDetail> jobDetail { get; set; }
        public DbSet<CezalıIslerView>CezaliIsler {  get; set; }
        public DbSet<LoginLog>LoginLogs { get; set; }
        public DbSet<userIzın> userIzıns { get; set; }
        public DbSet<UserDetayIzın>UserDetayIzın  { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CezalıIslerView>().HasNoKey();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
