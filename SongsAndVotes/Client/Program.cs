using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SongsAndVotes.Client.Helpers;
using SongsAndVotes.Client.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using SongsAndVotes.Client.Auth;

namespace SongsAndVotes.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddMudServices();

			ConfigureServices(builder.Services);

			await builder.Build().RunAsync();

		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<IHttpService, HttpService>();
			services.AddScoped<IArtistRepository, ArtistRepository>();
			services.AddScoped<ISongRepository, SongRepository>();
			services.AddScoped<IAccountRepository, AccountRepository>();
			services.AddAuthorizationCore();

			services.AddScoped<JWTAuthenticationStateProvider>();
			services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(
				provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

			services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(
				provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());
		}
	}
}
