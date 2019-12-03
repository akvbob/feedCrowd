using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Dtos.Comments
{
    public class AllCommentsDto
    {
        public int Id { get; set; }
        public string TextField { get; set; }
        public string User { get; set; }
        public string UserPic { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
