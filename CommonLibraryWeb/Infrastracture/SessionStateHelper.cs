using System;
using System.Web;

namespace CommonLibraryWeb.Infrastracture
{
	public static class SessionStateHelper
	{
		public static object Get(SessionStateKeys key)
		{
			if (HttpContext.Current == null)
				return null;

			string keyString = Enum.GetName(typeof(SessionStateKeys), key);
			return HttpContext.Current.Session[keyString];
		}
		public static object Set(SessionStateKeys key, object value)
		{
			if (HttpContext.Current == null)
				return null;

			string keyString = Enum.GetName(typeof(SessionStateKeys), key);
			return HttpContext.Current.Session[keyString] = value;
		}

		public static void ClearAll()
		{
			//Set(SessionStateKeys.CurrentLoginUser, null);
			//Set(SessionStateKeys.CurrentLanguage, null);
			//Set(SessionStateKeys.CurrentMenu, null);
			HttpContext.Current.Session.Clear();
		}
	}
}