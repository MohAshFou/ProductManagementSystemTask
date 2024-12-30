using FastEndpoints;
using ProductManagementSystemِAPITask.Services;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystemِAPITask.Data;
using ProductManagementSystemِAPITask.DTO;

namespace ProductManagementSystemِAPITask.Endpoints.Products
{
    public class GetAllProductByIDEndpoint : EndpointWithoutRequest<ProductResponse>
    {
        private readonly IProductService _productService;

        public GetAllProductByIDEndpoint(IProductService productService)
        {
            _productService = productService;
        }

        public override void Configure()
        {
            Get("/products/GetById/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            try
            {
               
                var response = await _productService.GetProductByIdAsync(Route<int>("id"), ct);
                await SendAsync(response);
            }
            catch (KeyNotFoundException ex)
            {
                await SendNotFoundAsync();
            }
        }

    }
}
