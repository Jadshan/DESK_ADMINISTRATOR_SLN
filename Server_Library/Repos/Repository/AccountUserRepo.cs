using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Base_Library.DTOs;
using Base_Library.Entities;
using Base_Library.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server_Library.Data;
using Server_Library.Helpers;
using Server_Library.Repos.RepoInterfaces;
using Constants = Server_Library.Helpers.Constants;

namespace Server_Library.Repos.Repository
{
	public class AccountUserRepo(IOptions<JwtSection> config, AppDbContext context) : IUserAccountRepo
	{
		private readonly IOptions<JwtSection> _config = config;
		private readonly AppDbContext _context = context;
		public async Task<GeneralResponse> CreateAsynsc(RegisterDTO user)
		{
			if (user == null) return new GeneralResponse(false, "Model is empty");

			var checkUser = await FindUserByEmail(user.EmailAddress!);
			if (checkUser != null) return new GeneralResponse(false, "user registered already");
			//save uiser
			var appUser = await AddToDatabase(new AppUser()
			{
				Name = user.FullName,
				UserName = user.UserName,
				UserEmail = user.EmailAddress,
				Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
			});

			//check, create and assign role
			var checkAdminRole = await _context.SystemRolesTbl.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Admin));
			if (checkAdminRole is null)
			{
				var createAdminRole = await AddToDatabase(new SystemRole() { Name = Constants.Admin });
				await AddToDatabase(new UserRole() { RoleId = createAdminRole.Id, UserId = appUser.Id });
				return new GeneralResponse(true, "Account created");
			}

			var checkUserRole = await _context.SystemRolesTbl.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.User));
			SystemRole response = new();
			if (checkUserRole is null)
			{
				response = await AddToDatabase(new SystemRole() { Name = Constants.Admin });
				await AddToDatabase(new UserRole() { RoleId = response.Id, UserId = appUser.Id });
			}
			else
			{
				await AddToDatabase(new UserRole() { RoleId = checkUserRole.Id, UserId = appUser.Id });
			}
			return new GeneralResponse(true, "Account created");

		}

		public async Task<LoginResponse> SignInAsync(LoginDTO user)
		{
			if (user == null) return new LoginResponse(false, "Model is empty");
			var appUser = await FindUserByEmail(user.EmailAddress!);
			if (appUser == null) return new LoginResponse(false, "User not found");

			//verify password
			if (!BCrypt.Net.BCrypt.Verify(user.Password, appUser.Password))
				return new LoginResponse(false, "Email / Password not valid");

			var getUserRole = await FindUserRole(appUser.Id);
			if (getUserRole == null) return new LoginResponse(false, "User role not found");

			var getRoleName = await FindRoleName(getUserRole.RoleId);
			if (getRoleName == null) return new LoginResponse(false, "User role not found");

			string jwtToken = GenerateToken(appUser, getRoleName!.Name!);
			string refereshToken = GenerateRefereshToken();

			// save referesh token
			var findedUser = await _context.RefereshTokenInfosTbl.FirstOrDefaultAsync(_ => _.UserId == appUser.Id);
			if (findedUser is not null)
			{
				findedUser.Token = refereshToken;
				await _context.SaveChangesAsync();
			}
			else
			{
				await AddToDatabase(new RefereshTokenInfo() { Token = refereshToken, UserId = appUser.Id });
			}
			return new LoginResponse(true, "Login Successfully", jwtToken, refereshToken);
		}

		private string GenerateToken(AppUser appUser, string role)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.Key!));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var userClaims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
				new Claim(ClaimTypes.Name, appUser.Name!),
				new Claim(ClaimTypes.Email, appUser.UserEmail!),
				new Claim(ClaimTypes.Role, role!),
			};
			var token = new JwtSecurityToken(
				issuer: _config.Value.Issuer,
				audience: _config.Value.Audience,
				claims: userClaims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: credentials
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private static string GenerateRefereshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

		private async Task<AppUser> FindUserByEmail(string email) =>
			await _context.AppUsersTbl.FirstOrDefaultAsync(_ => _.UserEmail!.ToLower()!.Equals(email!.ToLower()));

		private async Task<T> AddToDatabase<T>(T model)
		{
			var result = _context.Add(model!);
			await _context.SaveChangesAsync();
			return (T)result.Entity;
		}

		public async Task<LoginResponse> RefershTokenAsync(RefereshTokenDTO token)
		{
			if (token is null) return new LoginResponse(false, "Model is empty");

			var findedToken = await _context.RefereshTokenInfosTbl.FirstOrDefaultAsync(_ => _.Token!.Equals(token.Token));
			if (findedToken is null) return new LoginResponse(false, "Token is required");

			//get user details
			var user = await _context.AppUsersTbl.FirstOrDefaultAsync(_ => _.Id == findedToken.UserId);
			if (user is null) return new LoginResponse(false, "Referesh Token could not be generated because user not found");

			var userRole = await FindUserRole(user.Id);
			var roleName = await FindRoleName(userRole.RoleId);
			string jwtToken = GenerateToken(user, roleName.Name!);
			string refereshToken = GenerateRefereshToken();

			var updatedRefereshToken = await _context.RefereshTokenInfosTbl.FirstOrDefaultAsync(_ => _.UserId == user.Id);
			if (updatedRefereshToken is null) return new LoginResponse(false, "Referesh Token could not be generated because user has not SignIn");

			updatedRefereshToken.Token = refereshToken;
			await _context.SaveChangesAsync();
			return new LoginResponse(true, "Token refeeshed successfully", jwtToken, refereshToken);
		}

		private async Task<UserRole> FindUserRole(int userId) => await _context.UserRolesTbl.FirstOrDefaultAsync(_ => _.UserId == userId);
		private async Task<SystemRole> FindRoleName(int userId) => await _context.SystemRolesTbl.FirstOrDefaultAsync(_ => _.Id == userId);

	}
}
