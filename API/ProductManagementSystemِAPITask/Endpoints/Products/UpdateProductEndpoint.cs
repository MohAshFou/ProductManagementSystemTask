using FastEndpoints;
using ProductManagementSystemِAPITask.Data;
using ProductManagementSystemِAPITask.DTO;
using ProductManagementSystemِAPITask.Services;
using ProductManagementSystemِAPITask.Validators;

namespace ProductManagementSystemِAPITask.Endpoints.Products
{
    public class UpdateProductEndpoint : Endpoint<UpdateProductRequest, ProductResponse>
    {
        private readonly IProductService _productService;

        public UpdateProductEndpoint(IProductService productService)
        {
            _productService = productService;
        }

        public override void Configure()
        {
            Put("/products/Update/{id}");
            AllowAnonymous();
            Validator<UpdateProductValidator>();
            AllowFileUploads();
        }

        public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
        {
            try
            {
                var response = await _productService.UpdateProductAsync(Route<int>("id"), req, ct);
                await SendAsync(response, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                await SendNotFoundAsync();
            }
        }
    }

}

