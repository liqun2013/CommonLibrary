using CommonLibrary;

namespace CommonLibraryWeb
{
	public class BaseResponseModel
	{
		public string Title { get; set; }
		public bool Error { get; set; }
		public string ErrorMsg { get; set; }

		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public string RedirectUrl { get; set; }
		public ResponseCodes ResponseCode { get; set; }
		public PaginateList PagedListData { get; set; }
	}

	public enum ResponseCodes
	{
		Error = 0,
		Ok = 1,
		NoData = 2,
	}
}