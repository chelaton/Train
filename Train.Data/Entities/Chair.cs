using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train.Data.Entities
{
    public class Chair
    {
        [Key]
        public int ChairId { get; set; }
        public bool NearWindow { get; set; }
        public int Number { get; set; }
        public bool Reserved { get; set; }
        public int WagonId { get; set; }
        public Wagon Wagon { get; set; }
    }
}
