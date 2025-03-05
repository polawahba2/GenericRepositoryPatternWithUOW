using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Core.Entities
{
    public class Book:BaseEntity
    {
        [Required, MaxLength(150)]
        public string Title { get; set; } = null!;

        public Author Author { get; set; } = null!;

        public int AuthorId { get; set; }
    }
}