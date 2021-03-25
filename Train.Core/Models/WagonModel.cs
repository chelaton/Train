using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train.Core.Models
{
    public class WagonModel
    {
        public int WagonId { get; set; }
        public int NumberOfChairs { get; set; }
        public List<ChairModel> Chairs { get; set; }
    }
}
