using TESTMyLib.Data.Abstract;

namespace TESTMyLib.Data.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Town { get; set; }
        public int Salary { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
