using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Train.Data.Entities;
using Train.Data.Interfaces;

namespace Train.Data.Repositories
{
    public class WagonRepository : IWagonRepository
    {
        private readonly TrainContext _trainContext;
        public WagonRepository(TrainContext trainContext)
        {
            _trainContext = trainContext;
        }
        public async Task<Wagon> AddAsync(Wagon wagon)
        {
            _trainContext.Add(wagon);
            await _trainContext.SaveChangesAsync();
            return wagon;
        }

        public async Task<Wagon> FindAsync(int wagonId)
        {
            return await _trainContext.Wagons.Include(c => c.Chairs).FirstOrDefaultAsync(x => x.WagonId == wagonId);
        }

        public IQueryable<Wagon> Get()
        {
            return _trainContext.Wagons.AsQueryable();
        }

        public async Task RemoveAsync(int wagonId)
        {
            var wagon = await _trainContext.Wagons.FindAsync(wagonId);
            if (wagon is not null)
            {
                _trainContext.Wagons.Remove(wagon);
                await _trainContext.SaveChangesAsync();
            }
        }

        public async Task<Wagon> UpdateAsync(Wagon wagon)
        {
            var local = _trainContext.Wagons.Local.FirstOrDefault(x => x.WagonId == wagon.WagonId);
            if (local is not null)
            {
                _trainContext.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _trainContext.Entry(wagon).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _trainContext.SaveChangesAsync();
            return wagon;
        }
    }
}
