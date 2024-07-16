using Microsoft.EntityFrameworkCore;

namespace Library.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public DateTime YearPublished { get; set; }
        public int CopiesAvailable { get; set; }
        public string Author { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
