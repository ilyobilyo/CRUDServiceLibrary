using System.ComponentModel.DataAnnotations;
using System;
namespace CRUDServiceLibrary.Abstract
{
    /// <summary>
    /// Serves as a base interface for all entities in the application.
    /// This interface provides common properties that all entities must have,
    /// ensuring a consistent structure across different data models.
    /// </summary>
    public interface IBaseEntity<TEndityId>
    {
        [Key]
        public TEndityId Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
