using System.ComponentModel.DataAnnotations;

namespace server.Model.DTO
{
    public class BookDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string UID { get; set; }
    }
}
