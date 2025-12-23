using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using TrainerManager.Domain.Entities;

namespace TrainerManager.Infrastructure.Data
{
    public class TrainerDbContext : DbContext
    {
        public TrainerDbContext(DbContextOptions<TrainerDbContext> options) : base(options) { }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<ClientTrainingHistory> TrainingHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainer>(b =>
            {
                // 1. Core Trainer configuration
                //b.HasKey(t => t.Id);

                // 2. Existing Owned Entities
                b.OwnsOne(t => t.Address);
                b.OwnsOne(t => t.Costing);
                b.OwnsOne(t => t.AccountDetails);

                // 3. New Visa Configuration (Owned One)
                // FORCE VISA TO BE COLUMNS IN THE SAME TABLE (Fixes SQLite Error)
                // This 'flattening' prevents the SQLite Foreign Key error
                b.OwnsOne(t => t.Visa, visa => {
                    visa.Property(v => v.VisaType).HasColumnName("VisaType");
                    visa.Property(v => v.Country).HasColumnName("VisaCountry");
                    visa.Property(v => v.ExpiryDate).HasColumnName("VisaExpiryDate");
                    visa.Property(v => v.IsWorkAuthorized).HasColumnName("VisaIsWorkAuthorized");
                });

                // 4. New Certifications Configuration (Owned Many)
                // We explicitly define a Shadow Property "Id" to act as the Primary Key
                //b.OwnsMany(t => t.Certifications, cert =>
                //{
                //    cert.ToTable("TrainerCertifications");
                //    cert.WithOwner().HasForeignKey("TrainerId");

                //    // This creates a column named 'Id' in the DB but not in your class
                //    cert.Property<int>("Id");
                //    cert.HasKey("Id");
                //});
                b.OwnsMany(t => t.Certifications, cert => {
                    cert.ToTable("TrainerCertifications");
                    cert.Property<int>("Id");
                    cert.HasKey("Id");
                });
                // 5. Training History
                b.HasMany(t => t.TrainingHistory)
                 .WithOne()
                 .HasForeignKey(h => h.TrainerId);
            });
            //modelBuilder.Entity<Trainer>(b => {
            //    b.HasKey(t => t.Id);

            //    b.OwnsOne(t => t.Address);
            //    b.OwnsOne(t => t.Costing);
            //    b.OwnsOne(t => t.AccountDetails);
            //    b.HasMany(t => t.TrainingHistory).WithOne().HasForeignKey(h => h.TrainerId);
            //});
        }
    }
}
