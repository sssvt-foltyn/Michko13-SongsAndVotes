using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsAndVotes.Shared.Entities
{
	public class Band
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public List<Artist> Arists { get; set; }
	}
}
