using MailService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Context
{
    public class MailDbContext : DbContext
    {
        public MailDbContext(DbContextOptions<MailDbContext> options)
        : base((DbContextOptions)options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Smtp
            modelBuilder.Entity<SmtpConfiguration>()
                .Property(p => p.Id)
                .HasColumnName("ConfEmailId")
                .IsRequired();

            modelBuilder.Entity<SmtpConfiguration>()
                .Property(p => p.Mail)
                .HasColumnName("Correo");

            modelBuilder.Entity<SmtpConfiguration>()
                .Property(p => p.Password)
                .HasColumnName("Clave");

            modelBuilder.Entity<SmtpConfiguration>()
                .Property(p => p.Host)
                .HasColumnName("SmptServer");

            modelBuilder.Entity<SmtpConfiguration>()
                .Property(p => p.Port)
                .HasColumnName("port");

            modelBuilder.Entity<SmtpConfiguration>()
                .Property(p => p.EnableSsl)
                .HasColumnName("EnableSsl");

            modelBuilder.Entity<SmtpConfiguration>()
                .Property(p => p.UseDefaultCredentials)
                .HasColumnName("UseDefaultCredentials");

            modelBuilder.Entity<SmtpConfiguration>()
                .ToTable("ConfEmail","ADM")
                .HasKey(x => x.Id);
            #endregion

            #region Mail Template
            modelBuilder.Entity<MailTemplate>()
                .Property(p => p.Id)
                .HasColumnName("EmailFormID")
                .IsRequired();

            modelBuilder.Entity<MailTemplate>()
                .Property(p => p.Code)
                .HasColumnName("EmailFormCode");

            modelBuilder.Entity<MailTemplate>()
                .Property(p => p.Description)
                .HasColumnName("descriptionForm");

            modelBuilder.Entity<MailTemplate>()
                .Property(p => p.Html)
                .HasColumnName("TextForm");

            modelBuilder.Entity<MailTemplate>()
                .Property(p => p.TemplateCode)
                .HasColumnName("CodeEmailForm");

            modelBuilder.Entity<MailTemplate>()
                .ToTable("PlanillaCorreos", "ADM")
                .HasKey(x => x.Id);
            #endregion
        }
        public virtual DbSet<SmtpConfiguration> SmtpConfigurations { get; set; }
        public virtual DbSet<MailTemplate> MailTemplates { get; set; }
    }
}
