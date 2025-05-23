using Ajansim.Core.Abstarcts;
using Ajansim.Core.Enums;
using Ajansim.Entities;
using Ajansim.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Context
{
    public class AjansimDBContext : DbContext
    {

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "server=DESKTOP-BAEHB4D\\SQLEXPRESS;database=AjansDB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);


            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseMap<BaseEntity>).Assembly);

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.ID == Guid.Empty)
                        entry.Entity.ID = Guid.NewGuid();

                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.Status = Status.Active;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        //public override int SaveChanges()
        //{

        //    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.CreatedAt = DateTime.Now;
        //            entry.Entity.Status = Core.Enums.Status.Active;
        //        }
        //        else if (entry.State == EntityState.Deleted && entry.State == EntityState.Modified)
        //        {
        //            entry.Entity.UpdatedAt = DateTime.Now;
        //        }

        //    }

        //    return base.SaveChanges();
        //}
    }
}