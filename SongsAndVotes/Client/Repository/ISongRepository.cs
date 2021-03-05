
using SongsAndVotes.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndVotes.Client.Repository
{
	public interface ISongRepository
	{
		Task CreateSong(Song song);
		Task DeleteSong(int id);
		Task EditSong(Song song);
		Task<List<Song>> GetSongByArtist(int artistID, string artistName);
		Task<Song> GetSongDetails(int id);
		Task<List<Song>> GetSongs();
	}
}
