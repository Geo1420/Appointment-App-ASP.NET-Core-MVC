using System.ComponentModel.DataAnnotations;

namespace MGPhoto.Models
{
    public class SlidePhoto
    {
        [Key]
        public int IdSlidePhoto { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Image")]
        public string? Picture { get; set; }
    }
}
