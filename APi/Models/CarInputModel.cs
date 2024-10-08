using APi.Models.Abstract;

namespace APi.Models
{
    public class CarInputModel : IInputModel
    {
        public string Brand { get; set; }
        public int Price { get; set; }
    }
}
