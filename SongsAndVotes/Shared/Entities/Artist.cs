using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsAndVotes.Shared.Entities
{
	public class Artist : Person
	{
		public List<ArtistsSongs> ArtistsSongs { get; set; } = new List<ArtistsSongs>();
	}
}
