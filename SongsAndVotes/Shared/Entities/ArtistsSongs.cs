using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsAndVotes.Shared.Entities
{
	public class ArtistsSongs
	{
		public int ID { get; set; }
		public int ArtistID { get; set; }
		public int SongsID { get; set; }
		public Artist Artist { get; set; }
		public Song Songs { get; set; }
		public int Order { get; set; }
	}
}
