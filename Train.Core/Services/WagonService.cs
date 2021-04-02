using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train.Core.Interfaces;
using Train.Core.Models;
using Train.Data.Interfaces;

namespace Train.Core.Services
{
    public class WagonService : IWagonService
    {
        private readonly IWagonRepository _wagonRepository;

        public WagonService(IWagonRepository wagonRepository)
        {
            _wagonRepository = wagonRepository;
        }
        public async Task<WagonModel> CreateWagonAsync(WagonModel wagonModel)
        {
            if (wagonModel is null)
            {
                throw new ArgumentNullException(nameof(wagonModel));
            }

            var chairs = new List<Data.Entities.Chair>();
            for (int i = 0; i < wagonModel.NumberOfChairs; i++)
            {
                chairs.Add(new Data.Entities.Chair { NearWindow = false, Number = i, Reserved = false });
            }

            var wagonEntity = new Data.Entities.Wagon
            {
                Chairs = chairs
            };

            wagonEntity = await _wagonRepository.AddAsync(wagonEntity);

            var listChairs = wagonEntity.Chairs
                      .Select(x => new ChairModel() { ChairId = x.ChairId, NearWindow = x.NearWindow, Number = x.Number, Reserved = x.Reserved, WagonId=x.WagonId })
                      .ToList();
            return new WagonModel
            {
                WagonId = wagonEntity.WagonId,
                NumberOfChairs = wagonEntity.Chairs.Count,
                Chairs = listChairs
            };
        }

        public async Task DeleteWagonAsync(int wagonId)
        {
            await _wagonRepository.RemoveAsync(wagonId);
        }

        public async Task<WagonModel> GetWagonAsync(int wagonId)
        {
            var wagonEntity = await _wagonRepository.FindAsync(wagonId);

            if (wagonEntity is null)
            {
                return null;
            }

            return new WagonModel
            {
                WagonId = wagonEntity.WagonId,
                NumberOfChairs = wagonEntity.Chairs.Count,
                Chairs = wagonEntity.Chairs
                      .Select(x => new ChairModel() { ChairId=x.ChairId, NearWindow = x.NearWindow, Number = x.Number, Reserved = x.Reserved, WagonId = x.WagonId })
                      .ToList()
            };
        }

        public async Task<List<WagonModel>> GetWagonsAsync()
        {
            IQueryable<Data.Entities.Wagon> query = _wagonRepository.Get();
            return await query.Select(wagonEntity => new WagonModel
            {
                WagonId = wagonEntity.WagonId,
                NumberOfChairs = wagonEntity.Chairs.Count,
                Chairs = wagonEntity.Chairs
                      .Select(x => new ChairModel() { ChairId = x.ChairId, NearWindow = x.NearWindow, Number = x.Number, Reserved = x.Reserved, WagonId = x.WagonId })
                      .ToList()
            })
            .ToListAsync();
        }

        public async Task<WagonModel> UpdateWagonAsync(WagonModel wagonModel)
        {
            var wagonEntity = new Data.Entities.Wagon
            {
                WagonId = wagonModel.WagonId,
                Chairs = wagonModel.Chairs
                      .Select(x => new Data.Entities.Chair() { ChairId = x.ChairId, NearWindow = x.NearWindow, Number = x.Number, Reserved = x.Reserved, WagonId = x.WagonId })
                      .ToList()
            };

            wagonEntity = await _wagonRepository.UpdateAsync(wagonEntity);

            return new WagonModel
            {
                WagonId = wagonEntity.WagonId,
                NumberOfChairs = wagonEntity.Chairs.Count,
                Chairs = wagonEntity.Chairs
                      .Select(x => new ChairModel() { ChairId = x.ChairId, NearWindow = x.NearWindow, Number = x.Number, Reserved = x.Reserved, WagonId = x.WagonId })
                      .ToList()
            };
        }
    }
}
