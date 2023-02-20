using System.ComponentModel.DataAnnotations;

namespace MGPhoto.Models
{
    public class GalleryPhoto
    {
        [Key]
        public int IdPhoto { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime UploadDate { get; set; }
        [Required]
        [Display(Name = "Image")]
        public string? Picture { get; set; }
        public string? Category { get; set; }
    }
}
