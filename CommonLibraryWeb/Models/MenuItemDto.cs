using CommonLibrary;
using System.Collections.Generic;

namespace CommonLibraryWeb.Models
{
	public class MenuItemDto : BaseDto
	{
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int Order { get; set; }
		/// <summary>
		/// 显示文字
		/// </summary>
		public string MenuText { get; set; }
		/// <summary>
		/// 菜单地址
		/// </summary>
		public string MenuUrl { get; set; }

		public List<MenuItemDto> SubMenus { get; set; }
		public bool HasActiveSubMenu { get; set; }
	}
}