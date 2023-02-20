using System.ComponentModel.DataAnnotations;

namespace MGPhoto.Models
{
    public class ReplyMessage
    {
        [Key]
        public int ReplyId { get; set; }
        public string? Message { get; set; }
        public DateTime? Sended { get; set; }
        public string? Recipient { get; set; }
    }
}
