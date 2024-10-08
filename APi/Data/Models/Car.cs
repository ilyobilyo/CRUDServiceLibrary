using CRUDServiceLibrary.Abstract;

namespace APi.Data.Models
{
    public class Car : BaseEntity
    {
        public string Brand { get; set; }
        public int Price { get; set; }
    }
}
