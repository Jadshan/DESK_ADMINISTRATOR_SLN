using System.Net.Http.Json;
using App_Library.Helpers;
using App_Library.Services.Contracts;
using Base_Library.DTOs;
using Base_Library.Responses;

namespace App_Library.Services.Implementations
{
	public class UserAccountService(GetHttpClient getHttp) : IUserAccountService
	{
		public const string AuthUrl = "api/auth";
		public async Task<GeneralResponse> CreateAsync(RegisterDTO user)
		{
			var httpClient = getHttp.GetPublicHttpClient();
			var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
			if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");
			return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
		}

		public async Task<LoginResponse> SignInAsync(LoginDTO user)
		{
			var httpClient = getHttp.GetPublicHttpClient();
			var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);
			if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error occured");
			return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
		}


		public Task<LoginResponse> RefereshTokenAsync(RefereshTokenDTO tokenDTO)
		{
			throw new NotImplementedException();
		}


		public async Task<WeatherForecastDTO[]> GetWeatherForecasts()
		{
			var httpClient = getHttp.GetPublicHttpClient();
			var result = await httpClient.GetFromJsonAsync<WeatherForecastDTO[]>("api/weatherforecast");
			return result!;
		}
	}
}
