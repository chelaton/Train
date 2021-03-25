using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train.Data.Entities
{
    public class Wagon
    {
        [Key]
        public int WagonId { get; set; }
        public int NumberOfChairs { get; set; }
        public List<Chair> Chairs { get; set; }
    }
}
