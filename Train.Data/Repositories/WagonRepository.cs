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
            Wagon actualWagon = _trainContext.Wagons.Include(x=>x.Chairs).FirstOrDefault(x => x.WagonId == wagon.WagonId);
            // Update parent

            if (actualWagon != null)
            {
                _trainContext.Entry(actualWagon).CurrentValues.SetValues(wagon);

                // Delete children
                foreach (var existingChild in actualWagon.Chairs.ToList())
                {
                    if (!wagon.Chairs.Any(c => c.ChairId == existingChild.ChairId))
                        _trainContext.Chairs.Remove(existingChild);
                }

                // Update and Insert children
                foreach (var childModel in wagon.Chairs)
                {
                    var existingChild = actualWagon.Chairs
                        .Where(c => c.ChairId == childModel.ChairId && c.ChairId != default(int))
                        .SingleOrDefault();

                    if (existingChild != null)
                        // Update child
                        _trainContext.Entry(existingChild).CurrentValues.SetValues(childModel);
                    else
                    {
                        // Insert child
                        var newChild = new Chair
                        {
                            ChairId = childModel.ChairId,
                            NearWindow  = childModel.NearWindow,
                            Number  = childModel.Number,
                            Reserved  = childModel.Reserved,
                            WagonId  = childModel.WagonId
                        };
                        actualWagon.Chairs.Add(newChild);
                    }
                }

                await _trainContext.SaveChangesAsync();
            }
            return wagon;
        }
    }
}
