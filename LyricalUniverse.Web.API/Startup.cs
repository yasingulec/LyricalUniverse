using AutoMapper;
using LyricalUniverse.Data;
using LyricalUniverse.Data.Albums.Concrete;
using LyricalUniverse.Data.Albums.Interface;
using LyricalUniverse.Data.Repository;
using LyricalUniverse.Data.Repository.Users.Concrete;
using LyricalUniverse.Data.Repository.Users.Interface;
using LyricalUniverse.Manager.Albums.Concrete;
using LyricalUniverse.Manager.Albums.Interface;
using LyricalUniverse.Manager.Users.Concrete;
using LyricalUniverse.Manager.Users.Interface;
using LyricalUniverse.Web.API.FileHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LyricalUniverse.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<LyricalUniverseDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LyricalUniverse.Web.API", Version = "v1" });
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<IAlbumManager, AlbumManager>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LyricalUniverse.Web.API v1"));
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
