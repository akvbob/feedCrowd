using System;
using FeedTheCrowd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FeedTheCrowd.Data
{
    public partial class FeedTheCrowdContext : DbContext
    {
        public FeedTheCrowdContext()
        {
        }

        public FeedTheCrowdContext(DbContextOptions<FeedTheCrowdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        //public virtual DbSet<RecipeProduct> RecipeProduct { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<EventRecipe> EventRecipe { get; set; }

        //protected override void OnModelCreating(ModelBuilder model)
        //{
        //    model.Entity<Comment>().
        //        HasOne(x => x.FkUserNavigation)
        //        .WithMany(x => x.Comment);
            
        //}
    }
}
