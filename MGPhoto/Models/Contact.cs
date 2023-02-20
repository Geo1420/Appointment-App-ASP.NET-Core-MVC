using System.ComponentModel.DataAnnotations;

namespace MGPhoto.Models
{
    public class Contact
    {
        [Key]
        public int Id_contact { get; set; }
        public string? Message { get; set; }
        public DateTime? DateTime_contact { get; set; }
        public string? EmailUser { get; set; }
    }
}
