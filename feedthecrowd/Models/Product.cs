using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FeedTheCrowd.Models
{
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int FkRecipe { get; set; }
        public virtual Recipe FkRecipeNavigation { get; set; }
    }
}
