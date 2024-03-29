﻿using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Data.Models.Exercise;
using LionSkyNot.Data.Models.Shop;
using LionSkyNot.Data.Models.Recipe;
using LionSkyNot.Data.Models.User;
using LionSkyNot.Data.Models.Gym;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace LionSkyNot.Data
{
    public class LionSkyDbContext : IdentityDbContext<User>
    {
        public LionSkyDbContext(DbContextOptions<LionSkyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorie> Categories { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Class> Classes { get; set; }

        public virtual DbSet<Trainer> Trainers { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Models.Shop.Type> Types { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Recipe> Recipes { get; set; }

        public virtual DbSet<Exercise> Exercises { get; set; }

        public virtual DbSet<TypeExercise> TypeExercises { get; set; }

        public virtual DbSet<WishList> WishLists { get; set; }

        public virtual DbSet<ClassUser> ClassUsers { get; set; }

        public virtual DbSet<WishListsProducts> WishListsProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

       
            builder.Entity<WishList>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<WishList>(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<ClassUser>()
                .HasOne(u => u.User)
                .WithMany(c => c.Classes)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ClassUser>()
                .HasOne(c => c.Class)
                .WithMany(u => u.Users)
                .HasForeignKey(c => c.ClassId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<WishListsProducts>()
                .HasOne(w => w.WishList)
                .WithMany(p => p.Products)
                .HasForeignKey(w => w.WishListId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WishListsProducts>()
                .HasOne(p => p.Product)
                .WithMany(w => w.WishLists)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Trainer>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Trainer>(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

    }
}