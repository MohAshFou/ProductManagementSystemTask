using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystemِAPITask.Data;
using ProductManagementSystemِAPITask.DTO;
using ProductManagementSystemِAPITask.Services;

namespace ProductManagementSystemِAPITask.Endpoints.Products
{
    public class GetAllProductsEndpoint : Endpoint<EmptyRequest, List<ProductResponse>>
    {
        private readonly IProductService _productService;
       


        public GetAllProductsEndpoint(IProductService productService , IWebHostEnvironment WebHostEnvironment)
        {
            _productService = productService;
          
        }

        public override void Configure()
        {
            Get("/products/GetAll");
            AllowAnonymous();
        }

        public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
        {
            try
            {

              
                var response = await _productService.GetAllProductsAsync(ct);

          
                await SendAsync(response.ToList(), StatusCodes.Status200OK);
            }
            catch (KeyNotFoundException ex)
            {
                await SendNotFoundAsync();
            }
        }
    }
}
