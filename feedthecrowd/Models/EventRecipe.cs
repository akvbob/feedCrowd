using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Models
{
    public partial class EventRecipe
    {
        [Key]
        public int Id { get; set; }
        public int FkEvent { get; set; }
        public int FkRecipe { get; set; }
        public virtual Recipe FkRecipeNavigation { get; set; }
        public virtual Event FkEventNavigation { get; set; }
    }
}
