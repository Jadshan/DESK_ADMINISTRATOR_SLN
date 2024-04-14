using Base_Library.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server_Library.Repos.RepoInterfaces;

namespace DESK_SERVER.Controllers
{
	[Route("api/authentication")]
	[ApiController]
	[AllowAnonymous]
	public class AuthController(IUserAccountRepo accountRepo) : ControllerBase
	{
		private readonly IUserAccountRepo _accountRepo = accountRepo;
		[HttpPost("register")]
		public async Task<IActionResult> CreateAsync(RegisterDTO registerDTO)
		{
			if (registerDTO == null) return BadRequest("Model is empty");
			var result = await _accountRepo.CreateAsync(registerDTO);
			return Ok(result);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> SignInAsync(LoginDTO login)
		{
			if (login == null) return BadRequest("Model is empty");
			var result = await _accountRepo.SignInAsync(login);
			return Ok(result);
		}

		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshTokenAsync(RefreshTokenDTO tokenDTO)
		{
			if (tokenDTO == null) return BadRequest("Model is empty");
			var result = await _accountRepo.RefreshTokenAsync(tokenDTO);
			return Ok(result);
		}

	}
}
