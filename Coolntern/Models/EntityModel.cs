using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Coolntern.Models
{
    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<DetailJob> DetailJobs { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Logo> Logoes { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<NCategory> NCategories { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<Slogan> Slogans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.DetailJobs)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.id_user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.meta)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.meta)
                .IsUnicode(false);

            modelBuilder.Entity<Footer>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Footer>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Footer>()
                .Property(e => e.meta)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.nameCompany)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.requirement)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.meta)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .HasMany(e => e.DetailJobs)
                .WithRequired(e => e.Job)
                .HasForeignKey(e => e.id_job)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Logo>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Logo>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Logo>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Logo>()
                .Property(e => e.meta)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.meta)
                .IsUnicode(false);

            modelBuilder.Entity<NCategory>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<NCategory>()
                .Property(e => e.desciption)
                .IsUnicode(false);

            modelBuilder.Entity<NCategory>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<NCategory>()
                .Property(e => e.meta)
                .IsUnicode(false);

            modelBuilder.Entity<NCategory>()
                .HasMany(e => e.News)
                .WithOptional(e => e.NCategory)
                .HasForeignKey(e => e.categoryId);

            modelBuilder.Entity<News>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Process>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Process>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Process>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Process>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Slogan>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Slogan>()
                .Property(e => e.job)
                .IsUnicode(false);

            modelBuilder.Entity<Slogan>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<Slogan>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Slogan>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Slogan>()
                .Property(e => e.meta)
                .IsUnicode(false);
        }
    }
}
