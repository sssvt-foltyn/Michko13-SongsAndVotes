using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongsAndVotes.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndVotes.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ArtistsController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public ArtistsController(ApplicationDbContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Artist>>> Get()
		{
			return await context.Artists.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult<int>> Post(Artist artist)
		{
			context.Add(artist);
			await context.SaveChangesAsync();
			return artist.ID;
		}
	}
}
