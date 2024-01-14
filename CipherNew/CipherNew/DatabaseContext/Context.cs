using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherNew.DatabaseContext
{
    internal class Context : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = ConfigurationManager.ConnectionStrings["database_connection"].ConnectionString;
            optionsBuilder.UseNpgsql(conn);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasIndex(e => e.Id);

            modelBuilder.Entity<User>()
                .HasIndex(p => p.Username)
                .IsUnique();

            modelBuilder.Entity<Message>()
                .HasOne<User>(p => p.Sender)
                .WithMany(p => p.SentMessages)
                .HasForeignKey(e => e.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne<User>(p => p.Receiver)
                .WithMany(p => p.ReceivedMessages)
                .HasForeignKey(p => p.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
