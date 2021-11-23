using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentCare.Model;
using ParentCare.Model.Medications;
using ParentCare.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ParentCare.Plugins.Database
{
    public class ParentCareContext : IdentityDbContext<User>
    {
        public virtual DbSet<MedicationAlert> MedicationAlerts { get; set; }

        public ParentCareContext()
        {

        }

        public ParentCareContext(DbContextOptions<ParentCareContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                // TODO: Figure out a way to remove the connection string below. This is being used by the EF Migrations
                //throw new Exception("Make sure you've selected the correct DB on the PhotosOfUsContext class");
                string conn = "";

                builder.UseSqlServer(conn);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicationAlert>(ConfigureMedicationAlert);

            modelBuilder.Entity<User>(ConfigureUser);
        }

        private void ConfigureMedicationAlert(EntityTypeBuilder<MedicationAlert> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(e => e.Title).IsRequired();

            //entity.Ignore(x => x.WaterMarkUrl);

            entity.Property(x => x.Weekdays)
            .IsRequired()
            .HasConversion(
                g => JsonSerializer.Serialize(g, null),
                y => JsonSerializer.Deserialize<int[]>(y, null));

            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //entity.HasOne(x => x.Event)
            //    .WithMany(x => x.Photos)
            //    .HasForeignKey(x => x.EventId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //entity.OwnsMany(e => e.PhotoTag, x =>
            //{
            //    x.HasKey(y => new { y.PhotoId, y.TagId });
            //    x.Property(y => y.RegisterDateUtc).HasColumnType("datetime");
            //});
        }

        private void ConfigureUser(EntityTypeBuilder<User> entity)
        {
            //entity.ToTable("User");
            entity.Property(e => e.Id);

            entity.Property(x => x.Role)
            .IsRequired()
            .HasConversion(
                g => g.Value,
                y => Role.Get(y));
        }
    }
}
