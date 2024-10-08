using CRUDServiceLibrary.Abstract;

namespace TESTMyLib.Data.Abstract
{
    public class BaseEntity : IBaseEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
