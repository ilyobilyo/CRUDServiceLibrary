using APi.Models.Abstract;

namespace APi.Models
{
    public class CarResponseModel : ResponseModel<int>
    {
        public string Brand { get; set; }
        public int Price { get; set; }
    }
}
