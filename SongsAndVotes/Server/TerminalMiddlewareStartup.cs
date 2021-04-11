using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;



namespace SongsAndVotes.Server
{



    public class TerminalMiddlewareStartup
    {



        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            //services.AddRazorPages();

            // LinkGenerator
            //services.AddG

        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Approach 1: Writing a terminal middleware.
            app.Use(next => async context =>
            {
                if (context.Request.Path == "/")
                {
                    Console.WriteLine("Terminal middleware in action.");
                    await context.Response.WriteAsync("Hello terminal middleware!");
                    return;
                }

                await next(context);
            });

            app.UseRouting();

            app.Use(next => context =>
            {
                Endpoint endpoint = context.GetEndpoint();
                Console.WriteLine(endpoint);
                return next(context);
            });

            app.UseEndpoints(endpoints =>
            {
                // Controllers.
                endpoints.MapControllers();

                // Approach 2: Using routing.
                endpoints.MapGet("/Movie", async context =>
                {
                    await context.Response.WriteAsync("Hello routing!");
                }).WithDisplayName("Routing in action");

                // URL generation.
                endpoints.MapProductsLink("/ShowLink")
                .WithDisplayName("Products Link in action");
            });
        }



    }



}
