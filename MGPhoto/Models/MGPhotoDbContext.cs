using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MGPhoto.Models
{
    public class MGPhotoDbContext: IdentityDbContext<IdentityUser>
    {
        public MGPhotoDbContext(DbContextOptions<MGPhotoDbContext> options) : base(options) { }

        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Feedback>? Feedbacks { get; set; }
        public DbSet<Appointment>? Appointments { get; set; }
        public DbSet<PhotoCategory>? PhotoCategories { get; set; }
        public DbSet<GalleryPhoto>? GalleryPhotos { get; set; }
        public DbSet<SlidePhoto>? SlidePhotos { get; set; }
        public DbSet<ReplyMessage>? ReplyMessages { get; set; }

    }
}
