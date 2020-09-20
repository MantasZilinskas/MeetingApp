using MeetingApp.Api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace MeetingApp.Api.Data.Context
{
    public class MeetingAppContext : DbContext
    {
        public MeetingAppContext(DbContextOptions<MeetingAppContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserMeeting>().HasKey(sc => new { sc.UserId, sc.MeetingId });

            modelBuilder.Entity<UserMeeting>()
                .HasOne<Meeting>(sc => sc.Meeting)
                .WithMany(s => s.MeetingUsers)
                .HasForeignKey(sc => sc.MeetingId);

            modelBuilder.Entity<UserMeeting>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.UserMeetings)
                .HasForeignKey(sc => sc.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<UserMeeting> UserMeetings { get; set; }

    }
}
