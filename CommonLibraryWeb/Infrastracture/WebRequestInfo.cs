using System;
using System.Web;

namespace CommonLibraryWeb.Infrastracture
{
	public class WebRequestInfo
	{
		public override string ToString()
		{
			if (HttpContext.Current != null && HttpContext.Current.Request != null)
			{
				var req = HttpContext.Current.Request;
				return (req.RawUrl ?? string.Empty) + "; "
					+ (req.UserAgent ?? string.Empty) + "; "
					+ (req.HttpMethod ?? string.Empty) + Environment.NewLine
					+ HttpUtility.UrlDecode(req.Params?.ToString() ?? string.Empty);
			}
			else
				return string.Empty;
		}
	}
}