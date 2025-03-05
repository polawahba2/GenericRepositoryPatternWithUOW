using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Core.Entities
{
    public class Author : BaseEntity
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;
    }
}