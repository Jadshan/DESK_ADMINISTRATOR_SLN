using Base_Library.DTOs;
using Microsoft.AspNetCore.Mvc;
using Server_Library.Repos.RepoInterfaces;

namespace DESK_SERVER.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController(IUserAccountRepo accountRepo) : ControllerBase
	{
		private readonly IUserAccountRepo _accountRepo = accountRepo;
		[HttpPost("register")]
		public async Task<IActionResult> CreateAsync(RegisterDTO registerDTO)
		{
			if (registerDTO == null) return BadRequest("Model is empty");
			var result = await _accountRepo.CreateAsynsc(registerDTO);
			return Ok(result);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> SignInAsync(LoginDTO login)
		{
			if (login == null) return BadRequest("Model is empty");
			var result = await _accountRepo.SignInAsync(login);
			return Ok(result);
		}

		[HttpPost("referesh-token")]
		public async Task<IActionResult> RefereshTokenAsync(RefereshTokenDTO tokenDTO)
		{
			if (tokenDTO == null) return BadRequest("Model is empty");
			var result = await _accountRepo.RefershTokenAsync(tokenDTO);
			return Ok(result);
		}

	}
}
