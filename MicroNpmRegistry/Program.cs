
using MicroNpmRegistry.Entities;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace MicroNpmRegistry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Exclude MVC Controllers from Swagger
                c.CustomOperationIds(apiDesc => apiDesc.ActionDescriptor is ControllerActionDescriptor controllerAction &&
                                                 !controllerAction.ControllerTypeInfo.IsSubclassOf(typeof(Controller))
                                                 ? apiDesc.ActionDescriptor.Id
                                                 : null);
            });
            builder.Services.Configure<RegistrySettings>(builder.Configuration.GetSection("RegistrySettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                    c.RoutePrefix = "swagger"; // Swagger UI available at /swagger
                });
            }
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthorization();
           
            app.MapControllerRoute(
                   name: "default",
                    pattern: "{controller=adminui}/{action=Index}/{id?}");
            app.MapControllers();
            app.Run();
        }
    }
}
