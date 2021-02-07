using Microsoft.AspNetCore.Mvc;
using SongsAndVotes.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndVotes.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SongsController
	{
		private readonly ApplicationDbContext context;

		public SongsController(ApplicationDbContext context)
		{
			this.context = context;
		}

		[HttpPost]
		public async Task<ActionResult<int>> Post(Song song)
		{
			context.Add(song);
			await context.SaveChangesAsync();
			return song.ID;
		}
	}
}
