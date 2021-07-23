using UsePromiseInSwitchMap.Api.Models;
using UsePromiseInSwitchMap.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UsePromiseInSwitchMap.Api.Data
{
    public class UsePromiseInSwitchMapDbContext: DbContext, IUsePromiseInSwitchMapDbContext
    {
        public DbSet<Address> Addresses { get; private set; }
        public DbSet<CondoInfo> CondoInfos { get; private set; }
        public UsePromiseInSwitchMapDbContext(DbContextOptions options)
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsePromiseInSwitchMapDbContext).Assembly);
        }
        
    }
}
