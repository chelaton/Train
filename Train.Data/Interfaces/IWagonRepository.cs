
using System.Linq;
using System.Threading.Tasks;

namespace Train.Data.Interfaces
{
    public interface IWagonRepository
    {
        Task<Entities.Wagon> FindAsync(int wagonId);
        Task<Entities.Wagon> UpdateAsync(Entities.Wagon wagon);
        Task<Entities.Wagon> AddAsync(Entities.Wagon wagon);
        Task RemoveAsync(int wagonId);
        IQueryable<Entities.Wagon> Get();
    }
}
