using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsAndVotes.Shared.Entities
{
	public class Song
	{
		public int ID { get; set; }
		[Required]
		public string Title { get; set; }
		public string Photo { get; set; }
		public string AudioFile { get; set; }
		public User UserUploaded { get; set; }
		public Artist Artist { get; set; }
		public List<ArtistsSongs> ArtistsSongs { get; set; } = new List<ArtistsSongs>();
	}
}
