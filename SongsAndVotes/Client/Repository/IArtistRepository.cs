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
		Task DeleteArtist(int id);
		Task EditArtist(Artist artist);
		Task<Artist> GetArtistDetails(int id);
		Task<List<Artist>> GetArtists();
		Task<List<Artist>> GetArtistsByName(string name);
	}
}
