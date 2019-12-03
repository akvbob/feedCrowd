using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace FeedTheCrowd.Configurations
{
    public static class ConfigurationExtensionMethod
    {
        public static IServiceCollection UseSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("demo", new Info { Title = "FeedTheCrowd", Version = "v1" });
            });

            return services;
        }
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                //                c.SwaggerEndpoint("/swagger/v1/swagger.json", "version_demo");
                c.SwaggerEndpoint("/swagger/demo/swagger.json", "version_demo");
                c.RoutePrefix = "swagger";
            });

            return app;
        }
    }
}
