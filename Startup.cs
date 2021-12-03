using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Microsoft.AspNetCore.Identity;
using Spotify_clone2.Configuration;
using Stripe;
using Spotify_clone2.Repositories;

namespace Spotify_clone2
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
            services.Configure<StripeOptions>(options =>
            {
                options.PublishableKey = Configuration["Stripe:PublishableKey"];
                options.SecretKey = Configuration["Stripe:SecretKey"];
                options.WebhookSecret = Configuration["Stripe:WebhookKey"];
                options.PremiumPrice = Configuration["Stripe:PremiumPriceKey"];
                options.Domain = Configuration["Stripe:Domain"];
            });


            services.AddDbContext<AppDbContext>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddHttpContextAccessor();
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = Configuration["Stripe:SecretKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

