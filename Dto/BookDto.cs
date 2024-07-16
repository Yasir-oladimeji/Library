using Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.Dto
{
    public class CreateBookDto
    {
        [Required(ErrorMessage = "CategoryId is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public int Pages { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public DateTime YearPublished { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public int CopiesAvailable { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public string Author { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public Category Category { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public Guid CategoryId { get; set; }
    }

    public class UpdateBookDto
    {
        [Required(ErrorMessage ="Title is required")] 
        public string Title { get; set; }
        [Required(ErrorMessage = "Pages is required")]
        public int Pages { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "YearPublished is required")]

        public DateTime YearPublished { get; set; }
        [Required(ErrorMessage = "CopiesAvailable is required")]

        public int CopiesAvailable { get; set; }
        [Required(ErrorMessage = "Author is required")]

        public string Author { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]

        public Guid CategoryId { get; set; }
    }

    public class BookDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }
        [Required(ErrorMessage = "Pages is required")]
        public int Pages { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "YearPublished is required")]
        public DateTime YearPublished { get; set; }
        [Required(ErrorMessage = "CreatedDate is required")]
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "CopiesAvailable is required")]
        public int CopiesAvailable { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]

        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "CategoryName is required")]
        public string CategoryName { get; set; }
    }
}
