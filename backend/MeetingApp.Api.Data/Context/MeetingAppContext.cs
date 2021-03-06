﻿using System.ComponentModel.DataAnnotations.Schema;
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
        public MeetingAppContext()
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
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<TodoItem> TodoItems { get; set; }
    }
}
