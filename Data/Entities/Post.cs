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
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}
