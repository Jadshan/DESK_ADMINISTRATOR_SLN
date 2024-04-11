using Base_Library.DTOs;

namespace App_Library.Helpers
{
	public class GetHttpClient(IHttpClientFactory httpClientFactory, LocalStorageService localStorageService)
	{
		private const string headerKey = "auth";
		public async Task<HttpClient> GetPrivateHttpClient()
		{
			var client = httpClientFactory.CreateClient("SystemApplicant");
			var stringToken = await localStorageService.GetToken();
			if (string.IsNullOrEmpty(stringToken)) return client;

			var deserializeToken = Serializations.DeserializeJsonString<UserSessionDTO>(stringToken);
			if (deserializeToken == null) return client;

			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializeToken.Token);
			return client;
		}

		public HttpClient GetPublicHttpClient()
		{
			var client = httpClientFactory.CreateClient("SystemApplicant");
			client.DefaultRequestHeaders.Remove(headerKey);
			return client;
		}
	}
}
