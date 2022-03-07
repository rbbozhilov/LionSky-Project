using LionSkyNot.Data.Models.Class;
using LionSkyNot.Data.Models.Product;
using LionSkyNot.Data.Models.Reception;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LionSkyNot.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public virtual DbSet<Categorie> Categories { get; set; }

        public virtual DbSet<Class> Classes { get; set; }

        public virtual DbSet<Trainer> Trainers { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Models.Product.Type> Types { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Reception> Receptions { get; set; }

    }
}