
using FastEndpoints;
using ProductManagementSystemِAPITask.DTO;
using ProductManagementSystemِAPITask.Services;


namespace ProductManagementSystemِAPITask.Endpoints.Products
{
    public class GetAllProductsByNameEndpoint : EndpointWithoutRequest<List<ProductResponse>>
    {

        private readonly IProductService _productService;



        public GetAllProductsByNameEndpoint(IProductService productService, IWebHostEnvironment WebHostEnvironment)
        {
            _productService = productService;

        }

        public override void Configure()
        {
            Get("/products/Search/{name}");
            AllowAnonymous();
        }

        public override async Task HandleAsync( CancellationToken ct)
        {
            try
            {


                var response = await _productService.GetAllProductsByNameAsync(Route<string>("name"), ct);


                await SendAsync(response.ToList(), StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                await SendNotFoundAsync();
            }
        }
    }
}
