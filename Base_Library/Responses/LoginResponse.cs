﻿namespace Base_Library.Responses
{
	public record LoginResponse(bool Flag, string Message = null!, string Token = null!, string RefreshToken = null!);


}
