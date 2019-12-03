using FeedTheCrowd.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Services.Interfaces
{
    public interface IProductService
    {
        Task<ICollection<AllProductsDto>> GetAll();
        Task<ProductDto> GetById(int id);
        Task<ICollection<AllProductsDto>> GetByRecipeId(int recipeId);
        Task Update(int id, NewProductDto[] products);
    }
}
