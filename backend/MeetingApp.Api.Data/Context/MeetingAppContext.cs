using MeetingApp.Api.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeetingApp.Api.Data.Context
{
    public class MeetingAppContext : IdentityDbContext<User>
    {
        public MeetingAppContext(DbContextOptions<MeetingAppContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meeting>()
                    .HasMany(x => x.Users)
                    .WithMany(x => x.Meetings);
        }

        public override DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
