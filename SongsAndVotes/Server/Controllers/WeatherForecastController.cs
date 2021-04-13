using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SongsAndVotes.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;



namespace SongsAndVotes.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			string routeInfo = ControllerContext.ToCtxString();
			_logger.LogInformation(routeInfo);
			Console.WriteLine(routeInfo);

			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			//.Select((wfc, i) => wfc)
			.Select((wfc, i) =>
			{
				//Thread.Sleep(1000);
				Thread.Sleep(200);
				return wfc;
			})
			.ToArray();
		}
	}
}
