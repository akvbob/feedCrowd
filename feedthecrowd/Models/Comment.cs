using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FeedTheCrowd.Models
{
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }
        public string TextField { get; set; }
        public int FkUser { get; set; }
        public int FkRecipe { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Recipe FkRecipeNavigation { get; set; }
        public virtual User FkUserNavigation { get; set; }
    }
}
