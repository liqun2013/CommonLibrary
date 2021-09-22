using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CommonLibraryWeb.Infrastracture
{
	public static class LocalizationHelper
	{
		public static string[] LanguageCodes => new string[] { "zh-CN", "en-US", "zh-TW" };
		//{
		//	get { return new string[] { "zh-CN", "en-US", "zh-TW" }; }
		//}

		/// <summary>
		/// 清理缓存
		/// </summary>
		public static void ClearCache()
		{
			string language = SessionStateHelper.Get(SessionStateKeys.CurrentLanguage) == null ? "zh-CN" : SessionStateHelper.Get(SessionStateKeys.CurrentLanguage).ToString();
			WebCache.Remove(language);
		}

		/// <summary>
		/// 翻译
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetTranslatedText(string key)
		{
			string text = string.Empty;
			string language = SessionStateHelper.Get(SessionStateKeys.CurrentLanguage) == null ? "zh-CN" : SessionStateHelper.Get(SessionStateKeys.CurrentLanguage).ToString();

			Dictionary<string, string> diclans = WebCache.Get(language) as Dictionary<string, string>;

			if (diclans == null)
			{
				diclans = GetSysLanguage(language);
				if (diclans == null)
					diclans = GetSysLanguage("zh-CN");
				WebCache.Set(language, diclans, 15);
			}

			if (diclans != null && diclans.ContainsKey(key))
				text = diclans[key];
			else
				text = key;

			//为中文转化为gb2312格式
			if ("zh-CN".Equals(language))
				text = utf8_gb2312(text);

			return text;
		}

		/// <summary>
		/// 翻译
		/// </summary>
		/// <param name="language">语言</param>
		/// <returns></returns>
		public static Dictionary<string, string> GetSysLanguage(string language = "zh-CN")
		{
			var fn = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/lans"), language + ".json");
			if (File.Exists(fn))
			{
				using (StreamReader file = File.OpenText(fn))
				using (JsonTextReader reader = new JsonTextReader(file))
				{
					JObject o2 = (JObject)JToken.ReadFrom(reader);
					return o2.ToObject<Dictionary<string, string>>();
				}
			}
			else
				return null;
		}

		/// <summary>
		/// 翻译
		/// </summary>
		/// <param name="key">语言键</param>
		/// <param name="language">语言</param>
		/// <returns></returns>
		public static string GetTranslatedText(string key, string language = "zh-CN")
		{
			string text = string.Empty;

			Dictionary<string, string> diclans = WebCache.Get(language) as Dictionary<string, string>;

			if (diclans == null)
			{
				diclans = GetSysLanguage(language);
				WebCache.Set(language, diclans, 15);
			}

			if (diclans != null && diclans.ContainsKey(key))
				text = diclans[key];

			//为中文转化为gb2312格式
			//if ("zh-CN".Equals(language))
			//	text = utf8_gb2312(text);

			return text;
		}
		/// <summary>
		/// 翻译
		/// </summary>
		/// <param name="html"></param>
		/// <param name="key">语言键</param>
		/// <returns></returns>
		public static string Translate(this HtmlHelper html, string key)
		{
			return GetTranslatedText(key);
		}

		/// <summary>
		/// UTF8转换成GB2312
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string utf8_gb2312(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
				return string.Empty;
			//声明字符集   
			//utf8   
			System.Text.Encoding utf8 = System.Text.Encoding.GetEncoding("utf-8");
			//gb2312   
			System.Text.Encoding gb2312 = System.Text.Encoding.GetEncoding("gb2312");
			byte[] utf;
			utf = utf8.GetBytes(text);
			utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
			//返回转换后的字符   
			return gb2312.GetString(utf);
		}

		private static string HTMLDecode(string str)
		{
			if (!string.IsNullOrWhiteSpace(str))
				return str.Replace("&amp;", @"&").Replace("&lt;", @"<").Replace("&gt;", @">").Replace("&nbsp;", " ").Replace("&#39;", "\'").Replace("&quot;", "\"").Replace(" <br>", "\n")
					.Replace(@"//", @"/").Replace(@"https:/", @"https://");
			else
				return string.Empty;
		}
	}
}