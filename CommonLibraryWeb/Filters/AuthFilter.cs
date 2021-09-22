using CommonLibraryWeb.Infrastracture;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace CommonLibraryWeb.Filters
{
	public class AuthFilter : AuthorizeAttribute, IAuthenticationFilter
	{
		public void OnAuthentication(AuthenticationContext filterContext)
		{
			if (filterContext.HttpContext.Request.IsAjaxRequest())
			{
				if (SessionStateHelper.Get(SessionStateKeys.CurrentLoginUserName) == null)
				{
					filterContext.Result = new JsonResult()
					{

						Data = new
						{
							NeedLogin = true,
							LoginUrl = UrlHelper.GenerateUrl("login", "login", "account", new RouteValueDictionary
							{
								{ "ReturnUrl",  filterContext.HttpContext.Request.UrlReferrer?.AbsolutePath}
							},
							RouteTable.Routes, HttpContext.Current.Request.RequestContext, false)
						}
					};
				}
			}
			else
			{
				if (SessionStateHelper.Get(SessionStateKeys.CurrentLoginUserName) == null || HttpContext.Current.Request.Cookies.Get(CookieKeys.LastLoginUsername.ToString()) == null)
				{
					filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
						{ "controller", "account"},
						{ "action", "Login"},
						{ "Area", ""},
						{ "ReturnUrl", filterContext.HttpContext.Request.RawUrl}
					});
				}
			}
		}

		public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
		{
			if (filterContext.Result == null && SessionStateHelper.Get(SessionStateKeys.CurrentLoginUserName) == null)
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
						{ "controller", "account"},
						{ "action", "Login"},
						{ "Area", ""},
						{ "ReturnUrl", filterContext.HttpContext.Request.RawUrl}
					});
			}
		}
	}
}