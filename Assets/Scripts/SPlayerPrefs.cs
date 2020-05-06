using UnityEngine;
using System.Security.Cryptography;
using System.Text;

public class SPlayerPrefs {
	
	public static void SetString(string key, string value) {
		PlayerPrefs.SetString(md5(key), encrypt(value));
	}

	public static string GetString(string key, string defaultValue) {
		if (!HasKey (key))
			return defaultValue;
		try {
			string s = decrypt(PlayerPrefs.GetString(md5(key)));
			return s;
		}
		catch {
			return defaultValue;
		}
	}
	
	public static string GetString(string key) {
		return GetString(key, "");
	}

	public static void SetInt(string key, int value) {
		PlayerPrefs.SetString(md5(key), encrypt(value.ToString()));
	}
	
	public static int GetInt(string key, int defaultValue) {
		if (!HasKey (key))
			return defaultValue;
		try {
			string s = decrypt(PlayerPrefs.GetString(md5(key)));
			int i = int.Parse(s);
			return i;
		}
		catch {
			return defaultValue;
		}
	}
	
	public static int GetInt(string key) {
		return GetInt(key, 0);
	}
	
	
	public static void SetFloat(string key, float value) {
		PlayerPrefs.SetString(md5(key), encrypt(value.ToString()));
	}
	
	
	public static float GetFloat(string key, float defaultValue) {
		if (!HasKey (key))
			return defaultValue;
		try {
			string s = decrypt(PlayerPrefs.GetString(md5(key)));
			float f = float.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
			return f;
		}
		catch {
			return defaultValue;
		}
	}
	
	public static float GetFloat(string key) {
		return GetFloat(key, 0);
	}

	public static bool HasKey(string key) {
		return PlayerPrefs.HasKey(md5(key));
	}

	public static void DeleteAll() {
		PlayerPrefs.DeleteAll();
	}

	public static void DeleteKey(string key) {
		PlayerPrefs.DeleteKey(md5(key));
	}

	public static void Save() {
		PlayerPrefs.Save ();
	}

	/*
	 * Обязательно смените этот секретный код и числа в массивах
	 */
	private static string secretKey = "iwillberich";
	private static byte[] key = new byte[8] {4, 5, 83, 10, 4, 85, 31, 10};
	private static byte[] iv = new byte[8] {14, 5, 17, 13, 8, 6, 59, 19};

	private static string encrypt(string s)
	{
		byte[] inputbuffer = Encoding.Unicode.GetBytes(s);
		byte[] outputBuffer = DES.Create().CreateEncryptor(key, iv).TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
		return System.Convert.ToBase64String(outputBuffer);
	}
	
	private static string decrypt(string s) {
		byte[] inputbuffer = System.Convert.FromBase64String(s);
		byte[] outputBuffer = DES.Create().CreateDecryptor(key, iv).TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
		return Encoding.Unicode.GetString(outputBuffer);
	}
	
	private static string md5(string s) {
		byte[] hashBytes = new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(s + secretKey + SystemInfo.deviceUniqueIdentifier));
		string hashString = "";
		for (int i = 0; i < hashBytes.Length; i++) {
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		return hashString.PadLeft(32, '0');
	}
}