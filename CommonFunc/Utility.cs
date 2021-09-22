using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CommonLibrary
{
	public static class Utility
	{
		#region XML operate
		public static string SerializeToXml<T>(T obj) where T : class
		{
			XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
			string result = string.Empty;
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add(string.Empty, string.Empty);

			using (var sww = new StringWriter())
			using (XmlWriter writer = XmlWriter.Create(sww))
			{
				xmlSerializer.Serialize(writer, obj, ns);
				result = sww.ToString();  //HttpUtility.HtmlEncode(sww.ToString());
			}

			return result;
		}

		public static T DeserializeToObject<T>(string xml) where T : class
		{
			XmlSerializer ser = new XmlSerializer(typeof(T));

			using (StringReader sr = new StringReader(xml))
			{
				return (T)ser.Deserialize(sr);
			}
		}
		#endregion

		#region Encrypt/Decrypt
		public static string Encrypt(string plainText, string secretKey)
		{
			if (string.IsNullOrEmpty(plainText))
				throw new ArgumentNullException("plainText");

			string encrypted = string.Empty;
			byte[] clearBytes = Encoding.UTF8.GetBytes(plainText);
			MemoryStream msEncrypt = null;
			CryptoStream csEncrypt = null;
			Aes aesAlg = null;
			ICryptoTransform encryptor = null;

			try
			{
				aesAlg = Aes.Create();
				byte[] k;
				byte[] iv;
				GeneralKeyIV(secretKey, out k, out iv);
				aesAlg.Key = k;
				aesAlg.IV = iv;

				encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				msEncrypt = new MemoryStream();
				csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
				csEncrypt.Write(clearBytes, 0, clearBytes.Length);
				encrypted = Convert.ToBase64String(msEncrypt.ToArray());
			}
			catch
			{ throw; }
			finally
			{
				if (aesAlg != null)
					aesAlg.Dispose();
				if (msEncrypt != null)
					msEncrypt.Dispose();
				if (csEncrypt != null)
					csEncrypt.Dispose();
				if (encryptor != null)
					encryptor.Dispose();
			}

			return encrypted;
		}

		public static string Decrypt(string cipherText, string secretKey)
		{
			// Check arguments.
			if (string.IsNullOrEmpty(cipherText))
				throw new ArgumentNullException("cipherText");

			// Declare the string used to hold the decrypted text.
			string plaintext = null;

			// Create an Aes object with the specified key and IV.
			MemoryStream msDecrypt = null;
			CryptoStream csDecrypt = null;
			Aes aesAlg = null;
			ICryptoTransform decryptor = null;
			try
			{
				aesAlg = Aes.Create();
				//Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(secretKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
				//aesAlg.Key = pdb.GetBytes(32);
				//aesAlg.IV = pdb.GetBytes(16);
				byte[] k;
				byte[] iv;
				GeneralKeyIV(secretKey, out k, out iv);
				aesAlg.Key = k;
				aesAlg.IV = iv;

				// Create a decrytor to perform the stream transform.
				decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				msDecrypt = new MemoryStream();
				csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write);
				var bytesToBeDecrypted = Convert.FromBase64String(cipherText);
				csDecrypt.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
				plaintext = Encoding.Unicode.GetString(msDecrypt.ToArray());
			}
			catch (CryptographicException)
			{
				plaintext = string.Empty;
			}
			catch
			{ throw; }
			finally
			{
				if (aesAlg != null)
					aesAlg.Dispose();
				if (msDecrypt != null)
					msDecrypt.Dispose();
				if (csDecrypt != null)
					csDecrypt.Dispose();
				if (decryptor != null)
					decryptor.Dispose();
			}

			return plaintext;
		}

		public static string GenerateMD5Hash(string input)
		{
			if (string.IsNullOrEmpty(input))
				throw new ArgumentException("argument cannot be null", "input");

			using (MD5 md5Hash = MD5.Create())
			{
				byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < data.Length; i++)
					sb.Append(data[i].ToString("x2"));

				return sb.ToString();
			}
		}

		private static void GeneralKeyIV(string keyStr, out byte[] key, out byte[] iv)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(keyStr);
			key = SHA256.Create().ComputeHash(bytes);
			iv = MD5.Create().ComputeHash(bytes);
		}
		#endregion

		/// <summary>
		/// Get the first listed <see cref="AddressFamily.InterNetwork" /> for the host name
		/// </summary>
		/// <param name="hostName">The host name or address to use</param>
		/// <returns>String representation of the IP Address or <see langword="null"/></returns>
		public static string ResolveHostAddress(string hostName)
		{
			try
			{
				return Dns.GetHostAddresses(hostName).FirstOrDefault(ip => ip.AddressFamily.Equals(AddressFamily.InterNetwork))?.ToString();
			}
			catch
			{
				return string.Empty;
			}
		}

		public static string ResolveHostName()
		{
			string result = string.Empty;
			try
			{
				result = Dns.GetHostName();
				if (!string.IsNullOrEmpty(result))
				{
					var response = Dns.GetHostEntry(result);
					result = response?.HostName ?? string.Empty;
				}
			}
			catch
			{
			}

			return result;
		}
		public static bool IsSiteLocalAddress(IPAddress address)
		{
			var addr = address.ToString();
			return addr.StartsWith("10.") || addr.StartsWith("172.16.") || addr.StartsWith("192.168.");
		}

	}
}
