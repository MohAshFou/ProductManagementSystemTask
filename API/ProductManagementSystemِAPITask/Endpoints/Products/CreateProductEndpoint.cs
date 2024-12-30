using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystemِAPITask.Data;
using ProductManagementSystemِAPITask.DTO;
using ProductManagementSystemِAPITask.Models;
using ProductManagementSystemِAPITask.Services;
using ProductManagementSystemِAPITask.Validators;


namespace ProductManagementSystemِAPITask.Endpoints.Products
{
    public class CreateProductEndpoint : Endpoint<ProductRequest, ProductResponse>
    {
        private readonly IProductService _productService;

        public CreateProductEndpoint(IProductService productService)
        {
            _productService = productService;
        }

        public override void Configure()
        {
            Post("/products/Create");
            AllowAnonymous();
            Validator<ProductValidator>();

            AllowFileUploads();



        }


        public override async Task HandleAsync(ProductRequest req, CancellationToken ct)
        {
            try
            {
                var response = await _productService.CreateProductAsync(req, ct);
                await SendAsync(response, StatusCodes.Status201Created);
            }
            catch (BadHttpRequestException ex)
            {
                await SendAsync(new ProductResponse() { StatusCode=ex.StatusCode.ToString()}); 
            }



        }


    }
}
