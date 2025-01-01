
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManagementSystemِAPITask.Data;
using ProductManagementSystemِAPITask.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using AspNetCoreRateLimit;

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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowMultipleOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "http://localhost:53043")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });


            // add RateLimit

            builder.Services.AddMemoryCache();

            builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
            builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            builder.Services.AddInMemoryRateLimiting();


                  builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowMultipleOrigins");
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            //  app.UseAuthorization();

            app.UseIpRateLimiting();
            //       app.MapControllers();
            app.UseFastEndpoints();

            app.Run();
        }
    }
}
