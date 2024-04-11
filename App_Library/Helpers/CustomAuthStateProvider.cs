using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Base_Library.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace App_Library.Helpers
{
	public class CustomAuthStateProvider(LocalStorageService localStorageService) : AuthenticationStateProvider
	{
		private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var stringToken = await localStorageService.GetToken();
			if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(anonymous));

			var deserializedToken = Serializations.DeserializeJsonString<UserSessionDTO>(stringToken);
			if (deserializedToken == null) return await Task.FromResult(new AuthenticationState(anonymous));

			var gotUserClaims = DecryptToken(deserializedToken.Token!);
			if (gotUserClaims == null) return await Task.FromResult(new AuthenticationState(anonymous));

			var claimsPrincipal = SetClaimsPrincipal(gotUserClaims);
			return await Task.FromResult(new AuthenticationState(claimsPrincipal));


		}

		public async Task UpdateAuthState(UserSessionDTO userSessionDTO)
		{
			var claimsPrincipal = new ClaimsPrincipal();
			if (userSessionDTO.Token != null || userSessionDTO.RefereshToken != null)
			{
				var serializeSession = Serializations.SerializeObj(userSessionDTO);
				await localStorageService.SetToken(serializeSession);
				var gotUserClaims = DecryptToken(userSessionDTO.Token!);
				claimsPrincipal = SetClaimsPrincipal(gotUserClaims);
			}
			else
			{
				await localStorageService.RemoveToken();
			}
			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

		}

		public static ClaimsPrincipal SetClaimsPrincipal(CustomUserClaimsDTO claims)
		{
			if (claims.Email is null) return new ClaimsPrincipal();
			return new ClaimsPrincipal(new ClaimsIdentity(
				new List<Claim>
				{
					new (ClaimTypes.NameIdentifier, claims.Id),
					new (ClaimTypes.Name, claims.Name),
					new (ClaimTypes.Email, claims.Email),
					new (ClaimTypes.Role, claims.Role),
				}, "JwtAuth"
				));
		}

		private static CustomUserClaimsDTO DecryptToken(string jwtToken)
		{
			if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaimsDTO();

			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadJwtToken(jwtToken);
			var userId = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier);
			var name = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name);
			var email = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email);
			var role = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role);

			return new CustomUserClaimsDTO(userId!.Value!, name!.Value!, email!.Value!, role!.Value!);


		}
	}
}
