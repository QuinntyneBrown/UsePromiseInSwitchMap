using UsePromiseInSwitchMap.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace UsePromiseInSwitchMap.Api.Interfaces
{
    public interface IUsePromiseInSwitchMapDbContext
    {
        DbSet<Address> Addresses { get; }
        DbSet<CondoInfo> CondoInfos { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
