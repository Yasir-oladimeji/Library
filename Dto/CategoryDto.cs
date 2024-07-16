using Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.Dto
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage ="Category name is required")]
        public required string Name { get; set; }
    }

    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Category name is required")]
        public required string Name { get; set; }
    }

    public class CategoryDto 
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        public string Name { get;set; }
        public DateTime CreatedDate { get; set; }

    }

    public class SelectCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
