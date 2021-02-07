using SongsAndVotes.Client.Helpers;
using SongsAndVotes.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndVotes.Client.Repository
{
	public class ArtistRepository : IArtistRepository
	{
		private readonly IHttpService httpService;
		private string url = "api/artists";

		public ArtistRepository(IHttpService httpService)
		{
			this.httpService = httpService;
		}

		public async Task<List<Artist>> GetArtists()
		{
			var response = await httpService.Get<List<Artist>>(url);
			if (!response.Success)
			{
				throw new ApplicationException(await response.GetBody());
			}

			return response.Response;
		}

		public async Task CreateArtist(Artist artist)
		{
			var response = await httpService.Post(url, artist);
			if (!response.Success)
			{
				throw new ApplicationException(await response.GetBody());
			}
		}
	}
}
