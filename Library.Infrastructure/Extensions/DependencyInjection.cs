using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Library.Infrastructure.Persistence;
using Library.Domain.Repositories;
using Library.Infrastructure.Repositories;
using Library.Application.Interfaces;
using Library.Infrastructure.Services;

namespace Library.Infrastructure {
    public static class DependencyInjection {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ILibraryReadStoredProc, BookStoredProcReader>();

            return services;
        }
    }
}
