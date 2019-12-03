using FeedTheCrowd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Dtos.Recipes
{
    public class RecipeDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Servings { get; set; }
        public int FkUser { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
