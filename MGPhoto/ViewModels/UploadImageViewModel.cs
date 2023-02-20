using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MGPhoto.ViewModels
{
    public class UploadImageViewModel
    {
        [Required]
        [Display(Name = "Picture")]
        public IFormFile? GalleryPicture { get; set; }
    }
}
