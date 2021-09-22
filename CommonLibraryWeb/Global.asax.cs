using CommonLibraryWeb.Infrastracture;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CommonLibraryWeb
{
	public class MvcApplication : HttpApplication
	{
		private static log4net.ILog logger = log4net.LogManager.GetLogger("httpreqlogger");
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			log4net.Config.XmlConfigurator.Configure();
		}

		public override void Init()
		{
			base.Init();
			BeginRequest += MvcApplication_BeginRequest;
		}

		private void MvcApplication_BeginRequest(object sender, EventArgs e)
		{
			log4net.LogicalThreadContext.Properties["requestinfo"] = new WebRequestInfo();
			logger.Debug("BeginRequest");
		}
	}
}
