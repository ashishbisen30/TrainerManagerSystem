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

            modelBuilder.Entity<Trainer>(b => {
                b.OwnsOne(t => t.Address);
                b.OwnsOne(t => t.Costing);
                b.OwnsOne(t => t.AccountDetails);
                b.HasMany(t => t.TrainingHistory).WithOne().HasForeignKey(h => h.TrainerId);
            });
        }
    }
}
