using System.Web.Mvc;

namespace CommonLibraryWeb.Controllers
{
	public class BaseController : Controller
	{
		// GET: Base
		protected static log4net.ILog Logger = log4net.LogManager.GetLogger("commonlogger");

		protected override void OnException(ExceptionContext excContext)
		{
			Logger.Error(excContext.Exception);
			base.OnException(excContext);
		}
	}
}