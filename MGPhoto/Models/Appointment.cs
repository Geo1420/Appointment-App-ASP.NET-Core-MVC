using System.ComponentModel.DataAnnotations;

namespace MGPhoto.Models
{
    public class Appointment
    {
        [Key]
        public int IdAppointment { get; set; }
        public string? EmailUser { get; set; }
        public DateTime Date { get; set; }
        public string? Message { get; set; }
    }
}
