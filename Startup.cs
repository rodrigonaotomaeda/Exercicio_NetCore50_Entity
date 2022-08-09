using MeuTeste.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeuTeste
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //vai ficar disponivel pra todos os métodos na Injeção de dependencias
            services.AddDbContext<AppDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => //: IEndpointRouteBuilder
            {
                //formato padrao de criação
                endpoints.MapControllerRoute(
                       name: "default",
                    pattern: "{controler-Home}/{action=Index}/{id?}");
            });
        }
    }
}
