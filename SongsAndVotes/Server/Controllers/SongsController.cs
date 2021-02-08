using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongsAndVotes.Shared.Entities;
using SongsAndVotes.Shared.DTOs;
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

		[HttpGet]
		public async Task<ActionResult<List<Song>>> Get()
		{
			var response = context.Songs
				.Select(x => new Song
				{
					ID = x.ID,
					Title = x.Title,
					Photo = x.Photo,
					UserUploaded = x.UserUploaded,
					Artist = x.Artist,
				}).ToListAsync();

			return await response;
		}

		[HttpGet("{ID}")]
		public async Task<ActionResult<Song>> Get(int ID)
		{
			var response = context.Songs
				.Where(x => x.ID == ID)
				.Select(x => new Song
				{
					ID = x.ID,
					Title = x.Title,
					Photo = x.Photo,
					UserUploaded = x.UserUploaded,
					Artist = x.Artist,
					AudioFile = x.AudioFile
				});

			return response.FirstOrDefault();
		}

		[HttpPost]
		public async Task<ActionResult<int>> Post(Song song)
		{
			var artist = context.Artists.Find(song.Artist.ID);
			song.Artist = artist;
			context.Add(song);
			await context.SaveChangesAsync();
			return song.ID;
		}
	}
}
