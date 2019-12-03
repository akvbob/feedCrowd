using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FeedTheCrowd.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            Comment = new HashSet<Comment>();
            Products = new HashSet<Product>();
            EventRecipes = new HashSet<EventRecipe>();
        }
        [Key]
        public int Id { get; set; }
        public int? FkUser { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Servings { get; set; }

        public virtual User FkUserNavigation { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public ICollection<EventRecipe> EventRecipes { get; set; }
    }
}
