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

		public async Task CreateSong(Song song)
		{
			var response = await httpService.Post(url, song);
			if (!response.Success)
			{
				throw new ApplicationException(await response.GetBody());
			}
		}
	}
}
