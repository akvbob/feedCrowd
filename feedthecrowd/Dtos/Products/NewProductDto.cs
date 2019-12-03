using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Dtos.Products
{
    public class NewProductDto
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int FkRecipe { get; set; }
    }
}
