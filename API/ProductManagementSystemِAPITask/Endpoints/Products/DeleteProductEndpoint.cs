using ProductManagementSystemِAPITask.Data;
using FastEndpoints;
using ProductManagementSystemِAPITask.Services;
using ProductManagementSystemِAPITask.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagementSystemِAPITask.Endpoints.Products
{
    public class DeleteProductEndpoint : Endpoint<DeleteProductRequest>
    {
        private readonly IProductService _productService;

        public DeleteProductEndpoint(IProductService productService)
        {
            _productService = productService;
        }

        public override void Configure()
        {
            Delete("/products/Delete/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync([FromRoute] DeleteProductRequest request, CancellationToken ct)
        {
            try
            {
                await _productService.DeleteProductAsync(request.Id, ct);
                await SendNoContentAsync();
            }
            catch (Exception ex)
            {
                await SendNotFoundAsync();
            }
            
        }
    }
}
