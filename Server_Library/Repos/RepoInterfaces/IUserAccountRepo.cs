using Base_Library.DTOs;
using Base_Library.Responses;

namespace Server_Library.Repos.RepoInterfaces
{
	public interface IUserAccountRepo
	{

		Task<GeneralResponse> CreateAsync(RegisterDTO user);
		Task<LoginResponse> SignInAsync(LoginDTO user);
		Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO token);


	}

}
