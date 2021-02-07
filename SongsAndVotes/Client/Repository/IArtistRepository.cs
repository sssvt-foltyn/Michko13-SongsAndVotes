using SongsAndVotes.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndVotes.Client.Repository
{
	public interface IArtistRepository
	{
		Task CreateArtist(Artist artist);
		Task<List<Artist>> GetArtists();
	}
}
