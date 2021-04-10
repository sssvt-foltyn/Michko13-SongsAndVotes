using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Threading.Tasks;



namespace SongsAndVotes.Server
{



    public class Startup
	{



		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}



		public IConfiguration Configuration { get; }



		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllersWithViews();
			services.AddRazorPages();

			// For the Health Check example (see below).
			// ***
			//services.AddHealthChecks();
			// ***
			//services.AddAuthentication();
			//services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			//	.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
			//	options =>
			//	{
			//		options.LoginPath = new PathString("/auth/login");
			//		options.AccessDeniedPath = new PathString("/auth/denied");
			//	});

		}



		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}


			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();


			// Location 1: before routing runs, endpoint is always null here
			app.Use(next => context =>
			{
				Console.WriteLine($"1. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
				return next(context);
			});


			app.UseRouting();


			// Endpoint aware middleware.
			// Middleware can use metadata from the matched endpoint.
			//app.UseAuthentication();
			//app.UseAuthorization();


			//app.Use(next => context =>
			//{
			//    var endpoint = context.GetEndpoint();
			//    if (endpoint is null)
			//    {
			//        //return Task.CompletedTask;
			//        return next(context);
			//    }

			//    Console.WriteLine($"Endpoint: {endpoint.DisplayName}");

			//    if (endpoint is RouteEndpoint routeEndpoint)
			//    {
			//        Console.WriteLine("Endpoint has route pattern: " + routeEndpoint.RoutePattern.RawText);
			//    }

			//    foreach (var metada in endpoint.Metadata)
			//    {
			//        Console.WriteLine($"Endpoint has metadata: {metada}");
			//    }

			//    //return Task.CompletedTask;
			//    return next(context);
			//});


			// Location 2: after routing runs, endpoint will be non-null if routing found a match
			app.Use(next => context =>
			{
				Console.WriteLine($"2. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
				return next(context);
			});


            //app.Use(next => async context =>
            //{
            //    await Task.Run(() =>
            //    {
            //        var endpoint = context.GetEndpoint();
            //        if (endpoint is null)
            //        {
            //            //return Task.CompletedTask;
            //            return;
            //        }

            //        Console.WriteLine($"Endpoint: {endpoint.DisplayName}");

            //        if (endpoint is RouteEndpoint routeEndpoint)
            //        {
            //            Console.WriteLine("Endpoint has route pattern: " + routeEndpoint.RoutePattern.RawText);
            //        }

            //        foreach (var metada in endpoint.Metadata)
            //        {
            //            Console.WriteLine($"Endpoint has metadata: {metada}");
            //        }

            //        //return Task.CompletedTask;
            //        return;
            //    });
            //});


            app.UseEndpoints(endpoints =>
			{
				//endpoints.MapRazorPages();
				//endpoints.MapControllers();
				//endpoints.MapFallbackToFile("index.html");

				//endpoints.MapGet("/", async context =>
				//{
				//    await context.Response.WriteAsync("Hello World!");
				//});
				//endpoints.MapPost("/", async context =>
				//{
				//    await context.Response.WriteAsync("Hello World!");
				//});

				//endpoints.MapGet("/hello/{name:alpha}", async context =>
				//{
				//	var name = context.Request.RouteValues["name"];
				//	await context.Response.WriteAsync($"Hello {name}!");
				//});

				// Configure the Health Check endpoint and require an authorized user.
				// For this to work, you need to add "services.AddHealthChecks();" to ConfigureServices
				//endpoints.MapHealthChecks("/healthz").RequireAuthorization();

				// Configure another endpoint, no authorization requirements.
				//endpoints.MapGet("/", async context =>
				//{
				//	await context.Response.WriteAsync("   Hello World!");
				//	await context.Response.WriteAsync($"   Endpoint: {context.GetEndpoint()}");
				//});

				//endpoints.MapGet("/", context =>
				//{
				//	Console.WriteLine($"   Endpoint: {context.GetEndpoint()}");
				//	context.Response.WriteAsync("   Hello World!");
				//	return Task.CompletedTask;
				//}).WithDisplayName("Hello");

				//endpoints.MapGet("/", async context =>
				//{
				//	//Console.WriteLine($"   Endpoint: {context.GetEndpoint()}");
				//	//await Task.Run(() => Console.WriteLine($"   Endpoint: {context.GetEndpoint()}"));
				//	//await context.Response.WriteAsync("   Hello World!");
				//	//await Task.WhenAll(
				//	//	Task.Run(() => Console.WriteLine($"   Endpoint: {context.GetEndpoint()}")),
				//	//	context.Response.WriteAsync("   Hello World!"),
				//	//	context.Response.WriteAsync($"   Endpoint: {context.GetEndpoint()}")
				//	//);
				//	await context.Response.WriteAsync("Hello World!");
				//}).WithDisplayName("Slash");

				//endpoints.MapGet("/hello", async context =>
				//{
				//	await context.Response.WriteAsync("Hello!");
				//}).WithDisplayName("SlashHello");

				// Location 3a: runs when this endpoint matches
				endpoints.MapGet("/", context =>
				{
					Console.WriteLine($"3a. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
					context.Response.WriteAsync("Hello World!");
					return Task.CompletedTask;
				}).WithDisplayName("Slash");

				// Location 3b: runs when this endpoint matches
				endpoints.MapGet("/hello", context =>
				{
					Console.WriteLine($"3b. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
					context.Response.WriteAsync("Hello!");
					return Task.CompletedTask;
				}).WithDisplayName("SlashHello");

				// Location 3c: runs when this endpoint matches
				endpoints.MapGet("/yeah", context =>
				{
					Console.WriteLine($"3c. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
					context.Response.WriteAsync("Yeah!");
					return Task.CompletedTask;
				}).WithDisplayName("SlashYeah");

			});


			// Location 4: runs after UseEndpoints - will only run if there was no match
			app.Use(next => context =>
			{
				Console.WriteLine($"4. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
				return next(context);
			});

		}



	}



}
