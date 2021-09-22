using CommonLibraryWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CommonLibraryWeb.Controllers
{
	public class LayoutController : BaseController
	{
		public ActionResult Error(string errorMsg)
		{
			return View(new BaseResponseModel { ErrorMsg = errorMsg });
		}

		[ChildActionOnly]
		public PartialViewResult SideBarMenu()
		{
			var actMenu = System.Web.HttpContext.Current.Request.RawUrl;
			var responseModel = new MenuResponseModel { Menus = new List<MenuItemDto>(), ActiveMenu = actMenu };
			return PartialView("_SideBarMenu", responseModel);
		}

		private bool FindSubMenu(MenuItemDto m, string url)
		{
			var result = (m?.SubMenus?.Any(x => x.MenuUrl.Equals(url)) == true);

			if (!result && m.SubMenus?.Any() == true)
			{
				foreach (var itm in m.SubMenus)
				{
					result = FindSubMenu(itm, url);
					itm.HasActiveSubMenu = result;
					if (itm.HasActiveSubMenu)
						break;
				}
			}

			return result;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}