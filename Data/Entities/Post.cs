using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Content { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public List<string>? Images { get; set; }
        
        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;
    }
}
