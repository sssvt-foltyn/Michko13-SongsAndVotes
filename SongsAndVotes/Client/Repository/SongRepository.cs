using SongsAndVotes.Client.Helpers;
using SongsAndVotes.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndVotes.Client.Repository
{
	public class SongRepository : ISongRepository
	{
		private readonly IHttpService httpService;
		private string url = "api/songs";

		public SongRepository(IHttpService httpService)
		{
			this.httpService = httpService;
		}

		public async Task<List<Song>> GetSongs()
		{
			var response = await httpService.Get<List<Song>>(url);
			if (!response.Success)
			{
				throw new ApplicationException(await response.GetBody());
			}

			return response.Response;
		}

		public async Task<Song> GetSongDetails(int id)
		{
			var response = await httpService.Get<Song>($"{url}/{id}");
			if (!response.Success)
			{
				throw new ApplicationException(await response.GetBody());
			}

			return response.Response;
		}

		public async Task<List<Song>> GetSongByArtist(int artistID, string artistName)
		{
			var response = await httpService.Get<List<Song>>($"{url}/{artistID}/{artistName}");
			if (!response.Success)
			{
				throw new ApplicationException(await response.GetBody());
			}

			return response.Response;
		}


		public async Task CreateSong(Song song)
		{
			var response = await httpService.Post(url, song);
			if (!response.Success)
			{
				throw new ApplicationException(await response.GetBody());
			}
		}

		public async Task EditSong(Song song)
		{
			var response = await httpService.Put(url, song);
			if (!response.Success)
			{
				throw new ApplicationException(await response.GetBody());
			}
		}

		public async Task DeleteSong(int id)
		{
			var response = await httpService.Delete($"{url}/{id}");
			if (!response.Success)
			{
				throw new ApplicationException(await response.GetBody());
			}
		}
	}
}
