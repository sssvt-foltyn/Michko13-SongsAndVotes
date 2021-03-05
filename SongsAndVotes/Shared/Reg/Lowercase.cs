using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndVotes.Shared.Reg
{
	public class Lowercase : RegularExpressionAttribute
	{
		public Lowercase() : base("^[a-z]*$")
		{

		}
	}
}
