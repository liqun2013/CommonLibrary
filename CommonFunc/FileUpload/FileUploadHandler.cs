using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace CommonLibrary
{
	public class FileUploadHandler : IFileUploadHandler
	{
		private static log4net.ILog logger = log4net.LogManager.GetLogger("commonlogger");
		private string fileRelativePath;
		private string tempExtension;
		private string fileName;
		private string fileType;
		private long fileSize;
		private long startRange;
		private long endRange;
		private string fileAbsolutePath;
		private string fileFullPath;
		private string fileTempFullPath;
		private string uploaderId;

		public FileUploadHandler()
		{
			tempExtension = "_temp";
		}
		/// <param name="saveToRelativePath">相对路径(如:"~/App_Data/UploadFiles")</param>
		/// <param name="saveToAbsolutePath">绝对路径(如:"C:/App/App_Data/UploadFiles")</param>
		public void Init(string saveToRelativePath, string saveToAbsolutePath, string sessionId)
		{
			fileRelativePath = saveToRelativePath;//"~/App_Data/UploadFiles" + DateTime.Now.ToString("yyyyMM");
			fileAbsolutePath = saveToAbsolutePath;
			uploaderId = sessionId;
		}

		protected string FileFolder
		{
			get
			{
				if (string.IsNullOrWhiteSpace(fileAbsolutePath))
					throw new CommonLibraryException("AbsolutePath is null or empty!");

				if (!Directory.Exists(fileAbsolutePath))
					Directory.CreateDirectory(fileAbsolutePath);

				return fileAbsolutePath;
			}
		}

		protected string FileFullPath
		{
			get
			{
				if (string.IsNullOrWhiteSpace(fileFullPath) && !string.IsNullOrWhiteSpace(FileFolder) && !string.IsNullOrWhiteSpace(fileName))
					fileFullPath = Path.Combine(FileFolder, fileName);

				return fileFullPath;
			}
		}

		protected string FileTempFullPath
		{
			get
			{
				if (string.IsNullOrWhiteSpace(fileTempFullPath) && !string.IsNullOrWhiteSpace(FileFullPath))
					fileTempFullPath = FileFullPath + tempExtension;

				return fileTempFullPath;
			}
		}

		public async Task<FileUploadResult> ProcessUpload(HttpRequestBase request)
		{
			FileUploadResult result = new FileUploadResult();
			//System.Text.StringBuilder sbLog = new System.Text.StringBuilder();
			if (request.InputStream.Length > 0)
			{
				fileName = HttpUtility.UrlDecode(request.Headers["Content-FileName"]);
				fileType = request.Headers["Content-FileType"];
				fileSize = request.Headers["Content-FileSize"].ToInt64(true);
				startRange = request.Headers["Content-RangeStart"].ToInt64(true);
				endRange = request.Headers["Content-RangeEnd"].ToInt64(true);

				//sbLog.AppendFormat("Headers->FileName:{0}  FileType:{1}  FileSize:{2}  Range{3}-{4}{5}", fileName, fileType, fileSize.ToString(), startRange.ToString(), endRange.ToString(), Environment.NewLine);

				if (File.Exists(FileFullPath))
				{
					fileName = string.Format("{0}{1}{2}{3}{4}", Path.GetFileNameWithoutExtension(fileName), DateTime.Now.ToString("yyMMdd"), request.Headers["Content-FileSize"], uploaderId, Path.GetExtension(fileName));
					fileFullPath = string.Empty;
				}
				try
				{
					if (!(startRange == 0 && File.Exists(FileTempFullPath)))
					{
						await SaveFile(request.InputStream, FileTempFullPath).ConfigureAwait(false);

						if (endRange == fileSize)
						{
							if (!File.Exists(FileFullPath))
								File.Move(FileTempFullPath, FileFullPath);

							File.Delete(FileTempFullPath);

							result.FileUrl = fileRelativePath + "/" + fileName;
						}
						else
							result.UploadedRange = endRange;
					}

					//sbLog.AppendFormat("FileTempFullPath: {0}{2}FileFullPath:{1}",FileTempFullPath, FileFullPath, Environment.NewLine);
					result.Success = true;
				}
				catch (AggregateException exc)
				{
					logger.Error(exc);
					if(exc.InnerException != null)
						result.Error = exc.InnerException.Message;
				}
				catch (IOException ioExc)
				{
					logger.Error(ioExc);
					result.Error = ioExc.Message;
				}
				catch (Exception exc)
				{
					logger.Error(exc);
					result.Error = exc.Message;
				}
				finally
				{
					//logger.Info(sbLog.ToString());
				}
			}
			else
				result.Error = "no file";

			return result;
		}

		/// <summary>
		/// Save the contents of the Stream to a file
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="f"></param>
		private async Task SaveFile(Stream stream)
		{
			using (var f = File.Open(FileTempFullPath, FileMode.Append))
			{
				byte[] buffer = new byte[2048];
				int bytesRead;
				while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
					await f.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
			}
		}
	}
}
