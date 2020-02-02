using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeForms.Context;
using CodeForms.Interface;
using CodeForms.Options;
using CodeForms.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace CodeForms
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
            services.AddDbContext<ApplicationDBContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddScoped<IUserService, UserService>();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                 .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                 .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(x =>
            {
            x.SwaggerDoc("v1",  new Microsoft.OpenApi.Models.OpenApiInfo {Title ="CodeForms API", Version = "v1"} );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {    
                app.UseHsts();
            }
            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(option => {option.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(option => option.SwaggerEndpoint(swaggerOptions.UiEndPoint, swaggerOptions.Description));

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}
