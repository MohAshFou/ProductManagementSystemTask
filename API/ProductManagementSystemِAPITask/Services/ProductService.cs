using ProductManagementSystemِAPITask.Data;
using ProductManagementSystemِAPITask.DTO;
using ProductManagementSystemِAPITask.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Core;


namespace ProductManagementSystemِAPITask.Services
{

    public interface IProductService
    {
        Task<ProductResponse> CreateProductAsync(ProductRequest req, CancellationToken ct);
        Task<ProductResponse> UpdateProductAsync(int id, UpdateProductRequest req, CancellationToken ct);
        Task DeleteProductAsync(int id, CancellationToken ct);
        Task<ProductResponse> GetProductByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync(CancellationToken ct);

        Task<IEnumerable<ProductResponse>> GetAllProductsByNameAsync(string name, CancellationToken ct);
    }
    public class ProductService : IProductService
    {
        private readonly ProductManagementSystemContext _context;
        private readonly IMediaService _mediaService;
        private readonly IHttpContextAccessor _httpContextAccessor;
      

        public ProductService(ProductManagementSystemContext context, IMediaService mediaService , IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mediaService = mediaService;
            _httpContextAccessor = httpContextAccessor;
              }


        private string GetDomain()
        {
            
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return "";

            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/images/";
        }
        public async Task<ProductResponse> CreateProductAsync(ProductRequest req, CancellationToken ct)
        {
            var imageUrl = await _mediaService.SaveImageAsync(req.ImageProduct, ct);

            var product = new Product
            {
                Name = req.Name,
                Description = req.Description,
                Price = req.Price,
               
                UrlimageProduct = imageUrl
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(ct);

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                URLImageProduct = product.UrlimageProduct
            };
        }

        public async Task<ProductResponse> UpdateProductAsync(int id, UpdateProductRequest req, CancellationToken ct)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            if (req.NewImageProduct != null)
            {
                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(product.UrlimageProduct))
                {
                    var oldImagePath = Path.Combine(await _mediaService.GetImagesFolderPath(), product.UrlimageProduct);
                    await _mediaService.DeleteImageAsync(oldImagePath, ct);
                }

                // Save the new image
                var newImageUrl = await _mediaService.SaveImageAsync(req.NewImageProduct, ct);
                product.UrlimageProduct = newImageUrl;
            }

            product.Name = req.Name;
            product.Description = req.Description;
            product.Price = req.Price;

            await _context.SaveChangesAsync(ct);

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                URLImageProduct = product.UrlimageProduct
            };
        }


        public async Task DeleteProductAsync(int id, CancellationToken ct)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            var oldImagePath = Path.Combine(await _mediaService.GetImagesFolderPath(), product.UrlimageProduct);

            await _mediaService.DeleteImageAsync(oldImagePath, ct);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id, CancellationToken ct)
        {
          
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedDate= product.CreatedDate,
                URLImageProduct = GetDomain()+product.UrlimageProduct 
            };
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(CancellationToken ct)
        {

          //  var domain = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}"; // استخدام IHttpContextAccessor

            return await _context.Products
                .Select(product => new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    URLImageProduct = GetDomain() + product.UrlimageProduct
                    ,
                    CreatedDate = product.CreatedDate,
                })
                .ToListAsync(ct);
        }


        public async Task<IEnumerable<ProductResponse>> GetAllProductsByNameAsync(string name, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(name))
            {
                var allProducts = await _context.Products
                    .Select(product => new ProductResponse
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        URLImageProduct = GetDomain() + product.UrlimageProduct , CreatedDate = product.CreatedDate
                    })
                    .ToListAsync(ct);

              
                if (!allProducts.Any())
                {
                    throw new KeyNotFoundException("No products found.");
                }

                return allProducts;
            }

            var productsByName = await _context.Products
                .Where(p => EF.Functions.Like(p.Name, $"%{name}%"))
                .Select(product => new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    URLImageProduct = GetDomain() + product.UrlimageProduct
                })
                .ToListAsync(ct);

           
            if (!productsByName.Any())
            {
                throw new KeyNotFoundException("No products found matching the given name.");
            }

            return productsByName;
        }







    }
}
