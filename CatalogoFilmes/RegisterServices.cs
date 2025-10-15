using CatalogoFilmes.Repositories;
using CatalogoFilmes.Repositories.Interfaces;
using CatalogoFilmes.Services;
using CatalogoFilmes.Services.Interfaces;

namespace CatalogoFilmes
{
    public class RegisterServices
    {
        public RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFilmeService, FilmeService>();
            services.AddScoped<IFilmeRepository, FilmeRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAdminRepository, AdminRepository>();
        }
    }
}
