using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradChat.Common;
using GradChat.Data.Entity;
using GradChat.Data.Repo.PostRepository;
using GradChat.Data.Repo.PostRepository.Interface;
using GradChat.Data.Repo.UserRepository;
using GradChat.Data.Repo.UserRepository.Interface;
using GradChat.Service.Interface;
using GradChat.Service.PostService;
using GradChat.Service.PostService.Interface;
using GradChat.Service.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GradChat
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
            services.AddCors();
            services.AddMvc();
         services.AddTransient<AppSettingsHelper>();
      // configure strongly typed settings objects
      var appSettingsSection = Configuration.GetSection("AppSettings");
          services.Configure<AppSettingsHelper>(appSettingsSection);

         // configure jwt authentication
         var appSettings = appSettingsSection.Get<AppSettingsHelper>();
         var key = Encoding.ASCII.GetBytes(appSettings.Secret);
         services.AddAuthentication(x =>
         {
          x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
          .AddJwtBearer(x =>
          {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
            };
          });


      RegisterDbContext(services);
            setDatabase(services);

            // register repository
            RegisterRepos(services);

            // register Service
            RegisterServie(services);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          // global cors policy
          app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

          app.UseAuthentication();
        app.UseMvc();
        }

        public virtual void setDatabase(IServiceCollection services)
        {
          services.AddDbContext<GradChatDbContext>(option =>
            option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
          );
        }

        // to the register the dbcontext
        protected virtual void RegisterDbContext(IServiceCollection services)
        {
            services.AddScoped<DbContext, GradChatDbContext>();
        }

        // to register repository
        protected virtual void RegisterRepos(IServiceCollection services)
        {
          services.AddScoped(typeof(IUserRepo), typeof(UserRepo));
          services.AddScoped(typeof(IPostRepo), typeof(PostRepo));
        }

        // to register the service
        protected virtual void RegisterServie(IServiceCollection service)
        {
            service.AddTransient(typeof(IUserService), typeof(UserService));
            service.AddTransient(typeof(IPostService), typeof(PostService));
        }
    }
}
