using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Train.Core.Models;

namespace Train.Core.Interfaces
{
    public interface IWagonService
    {
        Task<WagonModel> CreateWagonAsync(WagonModel wagonModel);
        Task<WagonModel> UpdateWagonAsync(WagonModel wagonModel);
        Task<WagonModel> GetWagonAsync(int wagonId);
        Task DeleteWagonAsync(int wagonId);
        Task<List<WagonModel>> GetWagonsAsync();
    }
}
