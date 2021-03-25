

namespace Train.Core.Models
{
    public class ChairModel
    {
        public int ChairId { get; set; }
        public bool NearWindow { get; set; }
        public int Number { get; set; }
        public bool Reserved { get; set; }
        public int WagonId { get; set; }
        public WagonModel Wagon { get; set; }
    }
}
