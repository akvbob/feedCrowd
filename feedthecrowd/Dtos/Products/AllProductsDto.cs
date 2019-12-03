using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Dtos.Products
{
    public class AllProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
    }
}
