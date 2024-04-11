using Blazored.LocalStorage;

namespace App_Library.Helpers
{
	public class LocalStorageService(ILocalStorageService localStorageService)
	{
		private readonly ILocalStorageService _localStorageService = localStorageService;
		private const string storageKey = "auth-token";
		public async Task<string> GetToken() => await _localStorageService.GetItemAsStringAsync(storageKey);
		public async Task SetToken(string item) => await _localStorageService.SetItemAsStringAsync(storageKey, item);
		public async Task RemoveToken() => await _localStorageService.RemoveItemAsync(storageKey);


	}
}
