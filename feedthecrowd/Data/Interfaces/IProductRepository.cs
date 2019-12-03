using FeedTheCrowd.Data.Repositories;
using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<ICollection<Product>> GetByRecipeId(int id);
        Task<bool> Update(int id, NewProductDto[] newProducts);
    }
}
