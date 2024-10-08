using TESTMyLib.Data.Abstract;

namespace TESTMyLib.Data.Models
{
    public class Car : BaseEntity
    {
        public string Brand { get; set; }
        public string Color { get; set; }
        public Employee Employee { get; set; }
    }
}
