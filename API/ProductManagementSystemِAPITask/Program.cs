
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManagementSystemِAPITask.Data;
using ProductManagementSystemِAPITask.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductManagementSystemِAPITask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

       //     builder.Services.AddControllers();



            builder.Services.AddDbContext<ProductManagementSystemContext>(options => {


                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }
           

              
           );
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddFastEndpoints();




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

          //  app.UseAuthorization();


     //       app.MapControllers();
            app.UseFastEndpoints();

            app.Run();
        }
    }
}
