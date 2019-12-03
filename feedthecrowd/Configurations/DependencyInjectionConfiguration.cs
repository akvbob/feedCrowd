using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Data.Repositories;
using FeedTheCrowd.Services;
using FeedTheCrowd.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeService, RecipeService>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();

            return services;
        }
    }
}
