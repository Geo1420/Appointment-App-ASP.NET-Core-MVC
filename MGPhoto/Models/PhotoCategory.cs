using System.ComponentModel.DataAnnotations;

namespace MGPhoto.Models
{
    public class PhotoCategory
    {
        [Key]
        public int IdCategory { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        //public ICollection<GalleryPhoto>? GalleryPhotos { get; set; }
    }
}
