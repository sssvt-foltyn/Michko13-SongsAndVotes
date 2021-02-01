using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsAndVotes.Shared.Entities
{
	public class Playlist
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Photo { get; set; }
		public List<Song> Songs { get; set; }
		public User UserBy { get; set; }
	}
}
