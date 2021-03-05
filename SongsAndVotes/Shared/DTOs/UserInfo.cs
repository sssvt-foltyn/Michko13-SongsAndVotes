using SongsAndVotes.Shared.Reg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsAndVotes.Shared.DTOs
{
	public class UserInfo
	{
		[Required]
		[EmailAddress(ErrorMessage = "This is not a valid email address.")]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
