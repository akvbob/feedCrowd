using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FeedTheCrowd.Models
{
    public partial class Event
    {
        public Event()
        {
            EventRecipes = new HashSet<EventRecipe>();
        }
        [Key]
        public int Id { get; set; }
        public int FkUser { get; set; }
        public string Title { get; set; }
        public int PeopleCount { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<EventRecipe> EventRecipes { get; set; }
    }
}
