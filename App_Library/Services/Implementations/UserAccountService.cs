using System.Net.Http.Json;
using App_Library.Helpers;
using App_Library.Services.Contracts;
using Base_Library.DTOs;
using Base_Library.Responses;

namespace App_Library.Services.Implementations
{
    public class UserAccountService(GetHttpClient getHttp) : IUserAccountService
    {
        public const string AuthUrl = "api/authentication";
        public async Task<GeneralResponse> CreateAsync(RegisterDTO user)
        {
            var httpClient = getHttp.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occurred");
            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }

        public async Task<LoginResponse> SignInAsync(LoginDTO user)
        {
            var httpClient = getHttp.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);
            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error occurred");
            return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
        }


        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO tokenDTO)
        {
            var httpClient = getHttp.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/refresh-token", tokenDTO);
            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error occurred");
            return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
        }


        public async Task<WeatherForecastDTO[]> GetWeatherForecasts()
        {
            var httpClient = await getHttp.GetPrivateHttpClient();

            var result = await httpClient.GetFromJsonAsync<WeatherForecastDTO[]>($"{AuthUrl}/weatherforecast");
            return result!;
        }
    }
}
