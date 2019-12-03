using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Dtos.Comments
{
    public class NewCommentDto
    {
        public string TextField { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
