using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train.API.Models
{
    public class UpdateChairModel
    {
        public int ChairId { get; set; }
        public bool NearWindow { get; set; }
        public int Number { get; set; }
        public bool Reserved { get; set; }
        public int WagonId { get; set; }
        public UpdateWagonModel UpdateWagon { get; set; }
    }
}
