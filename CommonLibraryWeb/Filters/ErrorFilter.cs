using CommonLibrary;
using System.Web.Mvc;

namespace CommonLibraryWeb.Filters
{
	public class ErrorFilter : FilterAttribute, IExceptionFilter
	{
		public void OnException(ExceptionContext filterContext)
		{
			if (!filterContext.ExceptionHandled && filterContext.Exception is CommonLibraryException)
			{
				filterContext.Result = new RedirectResult("~/Layout/Error?ErrorMsg=" + filterContext.Exception.Message);
				filterContext.ExceptionHandled = true;
			}
		}
	}
}