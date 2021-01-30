using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsAndVotes.Shared.Entities
{
	public class Person
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string Biography { get; set; }
		public string Picture { get; set; }
		public DateTime? DateOfBirth { get; set; }
	}
}
