using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCors
{
    public class Startup
    {
        // Options Pattern

        IOptions<CorsOptions> options;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //_options = options;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<CorsOptions>(Configuration.GetSection("CorsOptions"));
            var config = Configuration.GetSection("CorsOptions").Get<CorsOptions>();

            // dinamik olarak client çektiðimiz yer.

            services.AddCors(options =>
            {
                options.AddPolicy("client2", builder =>
                builder
                .WithOrigins(config.apss)
                .AllowAnyMethod()
                //.WithHeaders("X-Api-Version","Accept","Authorization","multipart/formdata")
                .AllowAnyHeader()); 

                options.AddPolicy("client1", builder =>
                builder.WithOrigins("https://localhost:44315", "https://localhost:44315")
                .WithMethods(new string[] { "PUT"}) // DELETE // PATCH
                .AllowAnyHeader());

            });


            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(); // bu iki servis arasýna koyalým
            // birden fazla polcy kontrolünü contraol seviyesine býrakýyoruz.
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
