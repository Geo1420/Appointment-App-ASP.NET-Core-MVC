using System.ComponentModel.DataAnnotations;

namespace MGPhoto.Models
{
    public class Feedback
    {
        [Key]
        public int IdFeedback { get; set; }
        public string? Description { get; set; }
        public int Grade { get; set; }
        public string? Email { get; set; }
    }
}
