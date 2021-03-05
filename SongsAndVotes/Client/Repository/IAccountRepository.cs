using SongsAndVotes.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndVotes.Client.Repository
{
	public interface IAccountRepository
	{
		Task<UserToken> Login(UserInfo userInfo);
		Task<UserToken> Register(UserInfo userInfo);
		Task<UserToken> RenewToken();
	}
}
