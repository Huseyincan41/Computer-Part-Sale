using Data.Identity;
using Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ComputerDbContext:IdentityDbContext<AppUser, AppRole, int>
	{
        public ComputerDbContext(DbContextOptions<ComputerDbContext> options) : base(options) { }
        public DbSet<ComputerPart> ComputerParts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ComputerPartSale> ComputerPartSales { get; set; }
        public DbSet<ComputerPartSaleDetail> computerPartSaleDetails { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API kullanarak ilişkileri tanımlayın
            modelBuilder.Entity<Category>()
                .HasMany(c => c.ComputerParts)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId);


            modelBuilder.Entity<Category>().HasData(
                
                new Category { CategoryId=1,Name="Mother Board"},
                new Category { CategoryId=2,Name="Ram"}
               
                
                );
            modelBuilder.Entity<ComputerPart>().HasData(
                 new ComputerPart { ComputerPartId=1, CategoryId = 1, Name = "Asus Anakart", Price = 1500, Description = "Asus Anakart çift ram",Stock=25 },
                new ComputerPart { ComputerPartId = 2, CategoryId = 1, Name = "Rampage Ram", Price = 1000, Description = "Ram page 12gb ram", Stock = 30 }
                );
        }
    }
}
