using AutoMapper;
using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Services
{
    public class ProductService :IProductService
    {

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<AllProductsDto>> GetAll()
        {
            var products = await _repository.GetAll();
            var productsDto = _mapper.Map<AllProductsDto[]>(products);
            return productsDto;
        }

        public async Task<ProductDto> GetById(int id)
        {
            var product = await _repository.GetById(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<ICollection<AllProductsDto>> GetByRecipeId(int recipeId)
        {
            var products = await _repository.GetByRecipeId(recipeId);
            var productsDto = _mapper.Map<AllProductsDto[]>(products);
            return productsDto;
        }
        public async Task Update(int id, NewProductDto[] products)
        {
            if (products.Length == 0 || products == null)
                throw new ArgumentNullException();
            await _repository.Update(id, products);
        }

    }
}
