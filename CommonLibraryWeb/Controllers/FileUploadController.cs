using CommonLibrary;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonLibraryWeb.Controllers
{
	public class FileUploadController : BaseController
	{
		private Lazy<IFileUploadHandler> lazyFileUploadHandler;
		protected IFileUploadHandler fileUploadHandler
		{
			get
			{
				return lazyFileUploadHandler.Value;
			}
		}

		public FileUploadController() : base()
		{
			lazyFileUploadHandler = new Lazy<IFileUploadHandler>(() => new FileUploadHandler(), true);
		}

		[ChildActionOnly]
		public PartialViewResult FileUploadPart()
		{
			return PartialView("_FileUploadPart");
		}

		[HttpPost]
		public async Task<JsonResult> ProcessUpload()
		{
			var fileRelativePath = "~/App_Data/UploadFiles" + DateTime.Now.ToString("yyyyMM");
			fileUploadHandler.Init(fileRelativePath, Server.MapPath(fileRelativePath), Session.SessionID);
			var result = await fileUploadHandler.ProcessUpload(Request).ConfigureAwait(false);

			return Json(result);
		}
	}
}