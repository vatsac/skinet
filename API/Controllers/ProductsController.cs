using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Model;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;

        public ProductsController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType>
         productTypeRepo, IMapper mapper )
        {
            _productsRepo = productsRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
           var spec = new ProductWithTypesAndBrandsSpecification();

           var products =  await _productsRepo.ListAsync(spec);

           return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

         [HttpGet("{id}")]
         public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
         {
            var spec = new ProductWithTypesAndBrandsSpecification();

             var product = await _productsRepo.GetEntityWithSpec(spec);

             return _mapper.Map<Product, ProductToReturnDto>(product);
         }

         [HttpGet("brands")]

         public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
         {
             return Ok(await _productBrandRepo.ListAllAsync());
         }

         [HttpGet("types")]
         public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
         {
             return Ok(await _productTypeRepo.ListAllAsync());
         }
    }
}