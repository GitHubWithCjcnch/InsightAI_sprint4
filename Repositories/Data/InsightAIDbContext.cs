using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsightAI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using InsightAI.Models.Models;

namespace InsightAI.Repositories.Data
{
    public class InsightAIDbContext : DbContext
    {
        public InsightAIDbContext(DbContextOptions<InsightAIDbContext> options) : base(options)
        {
        }

        // Definição dos DbSets para cada tabela
        public DbSet<Company> Companies { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da tabela Company
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("COMPANIES");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Industry).HasMaxLength(100);
            });

            // Configuração da tabela Complaint
            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.ToTable("COMPLAINTS");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Description).IsRequired();
                entity.Property(c => c.DateFiled).IsRequired();

                entity.HasOne(c => c.Company)
                      .WithMany(c => c.Complaints)
                      .HasForeignKey(c => c.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração da tabela Prediction
            modelBuilder.Entity<Prediction>(entity =>
            {
                entity.ToTable("PREDICTIONS");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Type).IsRequired().HasMaxLength(50);
                entity.Property(p => p.PredictionResult).IsRequired();
                entity.Property(p => p.GeneratedOn).IsRequired();

                entity.HasOne(p => p.Company)
                      .WithMany(c => c.Predictions)
                      .HasForeignKey(p => p.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração da tabela User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();
            });
        }
    }
}
