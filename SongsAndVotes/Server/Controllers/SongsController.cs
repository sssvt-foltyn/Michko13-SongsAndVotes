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
	public class SongsController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IFileStorageService fileStorageService;

		public SongsController(ApplicationDbContext context, IFileStorageService fileStorageService)
		{
			this.context = context;
			this.fileStorageService = fileStorageService;
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
		public async Task<ActionResult<Song>> GetDetails(int ID)
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
				}).FirstOrDefaultAsync();

			return await response;
		}

		[HttpGet("{artistID}/{artistName}")]
		public async Task<ActionResult<List<Song>>> GetByArtist(int artistID, string artistName)
		{
			var artist = context.Artists
				.Where(x => x.ID == artistID).FirstOrDefault();

			var response = context.Songs
				.Where(x => x.Artist == artist)
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


		[HttpPost]
		public async Task<ActionResult<int>> Post(Song song)
		{
			var artist = context.Artists.Find(song.Artist.ID);
			song.Artist = artist;

			if (!string.IsNullOrWhiteSpace(song.Photo))
			{
				var songPhoto = Convert.FromBase64String(song.Photo);
				song.Photo = await fileStorageService.SaveFile(songPhoto, ".jpg", "songs");
			}

			if (!string.IsNullOrWhiteSpace(song.AudioFile))
			{
				var songAudioFile = Convert.FromBase64String(song.AudioFile);
				song.AudioFile = await fileStorageService.SaveFile(songAudioFile, ".mp3", "songs");
			}

			context.Add(song);
			await context.SaveChangesAsync();
			return song.ID;
		}

		[HttpPut]
		public async Task<ActionResult> Put(Song song)
		{
			context.Attach(song).State = EntityState.Modified;

			if (!string.IsNullOrWhiteSpace(song.Photo))
			{
				var songPhoto = Convert.FromBase64String(song.Photo);
				song.Photo = await fileStorageService.EditFile(songPhoto, ".jpg", "songs", song.Photo);
			}

			if (!string.IsNullOrWhiteSpace(song.AudioFile))
			{
				var songAudioFile = Convert.FromBase64String(song.AudioFile);
				song.AudioFile = await fileStorageService.EditFile(songAudioFile, ".mp3", "songs", song.AudioFile);
			}

			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var song = await context.Songs.FirstOrDefaultAsync(x => x.ID == id);

			if (song == null)
			{
				return NotFound();
			}

			await fileStorageService.DeleteFile(song.Photo, "songs");
			await fileStorageService.DeleteFile(song.AudioFile, "songs");

			context.Remove(song);
			await context.SaveChangesAsync();
			return NoContent();
		}
	}
}
