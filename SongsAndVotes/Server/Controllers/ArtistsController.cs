using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongsAndVotes.Server.Helpers;
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
		private readonly IFileStorageService fileStorageService;

		public ArtistsController(ApplicationDbContext context, IFileStorageService fileStorageService)
		{
			this.context = context;
			this.fileStorageService = fileStorageService;
		}

		[HttpGet("search/{searchText}")]
		public async Task<ActionResult<List<Artist>>> GetFilteredByName(string searchText)
		{
			if(string.IsNullOrWhiteSpace(searchText))
			{
				return new List<Artist>();
			}

			return await context.Artists.Where(x => x.Name.Contains(searchText))
				.Take(5)
				.ToListAsync();
		}

		[HttpGet("{ID}")]
		public async Task<ActionResult<Artist>> GetDetails(int ID)
		{
			var artist = await context.Artists.FirstOrDefaultAsync(x => x.ID == ID);
			if(artist == null)
			{
				return NotFound();
			}
			else
			{
				return artist;
			}
		}

		[HttpGet]
		public async Task<ActionResult<List<Artist>>> Get()
		{
			return await context.Artists.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult<int>> Post(Artist artist)
		{
			if(!string.IsNullOrWhiteSpace(artist.Photo))
			{
				var artistPhoto = Convert.FromBase64String(artist.Photo);
				artist.Photo = await fileStorageService.SaveFile(artistPhoto, "jpg", "artists");
			}

			context.Add(artist);
			await context.SaveChangesAsync();
			return artist.ID;
		}

		[HttpPut]
		public async Task<ActionResult> Put(Artist artist)
		{
			if (!string.IsNullOrWhiteSpace(artist.Photo))
			{
				var artistPhoto = Convert.FromBase64String(artist.Photo);
				artist.Photo = await fileStorageService.EditFile(artistPhoto, "jpg", "artists", artist.Photo);
			}

			context.Attach(artist).State = EntityState.Modified;
			await context.SaveChangesAsync();
			return NoContent();
		}
	}
}
