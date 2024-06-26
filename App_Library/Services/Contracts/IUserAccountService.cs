﻿using Base_Library.DTOs;
using Base_Library.Responses;

namespace App_Library.Services.Contracts
{
	public interface IUserAccountService
	{
		Task<GeneralResponse> CreateAsync(RegisterDTO user);
		Task<LoginResponse> SignInAsync(LoginDTO user);
		Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO tokenDTO);
		Task<WeatherForecastDTO[]> GetWeatherForecasts();

	}
}
