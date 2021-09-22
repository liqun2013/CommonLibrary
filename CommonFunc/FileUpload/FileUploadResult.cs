namespace CommonLibrary
{
	public sealed class FileUploadResult : BaseDto
	{
		public bool Success { get; set; }
		public string FileUrl { get; set; }
		public long UploadedRange { get; set; }
		public string Error { get; set; }
	}
}
