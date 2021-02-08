using SongsAndVotes.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsAndVotes.Shared.DTOs
{
	public class ListSongDTO
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string Photo { get; set; }
		public User UserUploaded { get; set; }
		public Artist Artist { get; set; }
	}
}
