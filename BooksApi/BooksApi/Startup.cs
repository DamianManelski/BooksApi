using BooksApi.Infrastructure;
using BooksApi.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BooksApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddMvc();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = Configuration["ApiInformation:Name"],
                    Version = "v1",
                    Description = Configuration["ApiInformation:Description"],
                    Contact = new OpenApiContact
                    {
                        Name = "Damian M.",
                        Email = string.Empty,
                    },
                });
            });

            services.AddDbContext<BooksDbContext>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IBooksService, BooksService>();

            services.AddScoped<IBookOpinionService, BookOpinionService>();

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", Configuration["ApiInformation: Name"]);

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                //  c.RoutePrefix = string.Empty;
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BooksDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
