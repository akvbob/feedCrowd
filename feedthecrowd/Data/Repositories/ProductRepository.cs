using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly FeedTheCrowdContext _dataContext;

        public ProductRepository(FeedTheCrowdContext context)
        {
            _dataContext = context;
        }

        public async Task<ICollection<Product>> GetAll()
        {
            return await _dataContext.Product.ToListAsync();
        }
        public async Task<Product> GetById(int id)
        {
            return await _dataContext.Product.FindAsync(id);
        }
        public async Task<ICollection<Product>> GetByRecipeId(int id)
        {
            var a = _dataContext.Product.Where(x => x.FkRecipe.Equals(id)).ToArrayAsync();
            return await a;
        }
        public async Task<bool> Update(int id, NewProductDto[] newProducts)
        {
            var findProducts = await _dataContext.Product.Where(x => x.FkRecipe.Equals(id)).ToListAsync();
            if (findProducts.Count == 0 || findProducts == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var item in findProducts)
            {
                var findProduct = await _dataContext.Product.FirstOrDefaultAsync(x => x.Id.Equals(item.Id));
                foreach (var a in newProducts)
                {
                    findProduct.Name = a.Name;
                    findProduct.Quantity = a.Quantity;
                }


                _dataContext.Update(findProduct);
            }

            await _dataContext.SaveChangesAsync();

            return true;
        }



    }
}
