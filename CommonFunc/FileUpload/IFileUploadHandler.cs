using System.Threading.Tasks;
using System.Web;

namespace CommonLibrary
{
	public interface IFileUploadHandler
	{
		Task<FileUploadResult> ProcessUpload(HttpRequestBase request);
		void Init(string saveToRelativePath, string saveToAbsolutePath, string sessionId);
	}
}
