namespace TESTMyLib.Models.ResponseModels
{
    public class EmployeeResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Town { get; set; }
        public int Salary { get; set; }
        public int CarId { get; set; }
        public CarResponseModel Car { get; set; }
    }
}
