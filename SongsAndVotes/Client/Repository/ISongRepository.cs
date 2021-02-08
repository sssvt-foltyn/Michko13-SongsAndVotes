using SongsAndVotes.Shared.DTOs;
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
		Task<Song> GetSongDetails(int id);
		Task<List<Song>> GetSongs();
	}
}
