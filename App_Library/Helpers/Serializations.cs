﻿using System.Text.Json;

namespace App_Library.Helpers
{
	public class Serializations
	{
		public static string SerializeObj<T>(T modelObj) => JsonSerializer.Serialize(modelObj);

		public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString);

		public static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString);
	}
}

