using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Dtos.Events
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int PeopleCount { get; set; }
    }
}
