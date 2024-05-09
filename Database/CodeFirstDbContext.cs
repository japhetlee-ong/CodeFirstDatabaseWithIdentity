using CodeFirstDatabase.Database.DbModels;
using CodeFirstDatabase.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstDatabase.Database
{
    public class CodeFirstDbContext : IdentityDbContext<ApplicationUser>
    {

        public CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options): base (options) 
        {
            
        }

        public DbSet<BlogsModel> Blogs { get; set; }
        public DbSet<CommentsModel> Comments { get; set; }
        public DbSet<CategoriesModel> Categories { get;set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoriesModel>().HasData(
                new CategoriesModel {Id = 1, CategoryName = "Sci-fi" },
                new CategoriesModel {Id = 2, CategoryName = "Horror" },
                new CategoriesModel {Id = 3, CategoryName = "Educational" }
            );

        }

    }
}
