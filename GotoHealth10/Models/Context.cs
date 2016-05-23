using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace GotoHealth10.Models
{
    public class Context : DbContext
    {
        public DbSet<UserModel> User { get; set; }
        public DbSet<WeighingModel> Weighing { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=GTH.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .Property(b => b.Id)
                .IsRequired();

            modelBuilder.Entity<WeighingModel>()
                .Property(b => b.Id)
                .IsRequired();
        }
    }
}
