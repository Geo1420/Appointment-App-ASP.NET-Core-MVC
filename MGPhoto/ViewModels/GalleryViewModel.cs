using System;
using System.ComponentModel.DataAnnotations;

namespace MGPhoto.ViewModels
{
    public class GalleryViewModel : EditImageViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime UploadDate { get; set; }

        [Required]
        public string? Category { get; set; }
    }
}
