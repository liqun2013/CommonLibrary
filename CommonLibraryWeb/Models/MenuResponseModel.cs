using System.Collections.Generic;

namespace CommonLibraryWeb.Models
{
	public class MenuResponseModel : BaseResponseModel
	{
		public MenuResponseModel()
		{
			ResponseCode = ResponseCodes.Ok;
		}
		public List<MenuItemDto> Menus { get; set; }
		public string ActiveMenu { get; set; }
	}
}