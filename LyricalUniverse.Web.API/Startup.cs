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
using LyricalUniverse.Web.API.FileHelper.UserFileManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddCors();
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
            services.AddTransient<IUserFileManager, UserFileManager>();
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
            app.UseCors(
   options => options.WithOrigins("http://127.0.0.1:5500").AllowAnyMethod()
                      );
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
