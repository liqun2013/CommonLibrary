using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonLibrary
{
	/// <summary>
	/// 调用asmx服务时传的参数基类，具体参数可继承该类
	/// </summary>
	public abstract class ASMXWebRequestParamsBase
	{
	}
	public abstract class ASMXWebResponceBase
	{
		public abstract ASMXWebResponceBase DeserializeToObject(string xml);
	}

	public class ASMXWebServiceRepository
	{
		private static log4net.ILog Logger = log4net.LogManager.GetLogger("commonlogger");
		private ASMXWebServiceRepository()
		{
		}
		public ASMXWebServiceRepository(ASMXWebRequestParamsBase req, ASMXWebResponceBase resp)
		{
			RequestParams = req;
			ResponceObject = resp;
		}
		public ASMXWebRequestParamsBase RequestParams { get; set; }
		public ASMXWebResponceBase ResponceObject { get; set; }
		public async Task Invoke(string serviceUrl, string method)
		{
			if (RequestParams == null)
				throw new NullReferenceException("RequestParams is null.");
			if (ResponceObject == null)
				throw new NullReferenceException("ResponceObject is null.");

			try
			{
				string soapStr = @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
					<soap:Body><{0} xmlns=""http://tempuri.org/""><xml>{1}</xml></{0}></soap:Body>
					</soap:Envelope>";

				var content = HttpUtility.HtmlEncode(Utility.SerializeToXml(RequestParams).Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", string.Empty));
				string requestXML = string.Format(soapStr, method, content);

				HttpWebRequest webRequest = CreateWebRequest(serviceUrl, method);
				using (Stream s = await webRequest.GetRequestStreamAsync().ConfigureAwait(false))
				using (StreamWriter stmw = new StreamWriter(s, Encoding.UTF8))
				{
					var req = requestXML.Replace("\r\n", string.Empty);
					Logger.InfoFormat("request params: {0}{1}", Environment.NewLine, req);
					await stmw.WriteAsync(req).ConfigureAwait(false);
				}

				using (WebResponse response = await webRequest.GetResponseAsync().ConfigureAwait(false))
				using (StreamReader rd = new StreamReader(response.GetResponseStream()))
				{
					string soapResult = HttpUtility.HtmlDecode(rd.ReadToEnd());
					Logger.InfoFormat("responce: {0}{1}", Environment.NewLine, soapResult);
					ResponceObject.DeserializeToObject(soapResult);
					//xmlResponce = XDocument.Parse(HttpUtility.HtmlDecode(soapResult));
				}
			}
			catch (Exception exc)
			{
				Logger.Error("ASMXWebServiceRepository->Invoke", exc);
				if(exc.InnerException != null)
					Logger.Error("ASMXWebServiceRepository->Invoke", exc.InnerException);
			}
		}

		public HttpWebRequest CreateWebRequest(string url, string soapAction)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
			webRequest.Headers.Add(@"SOAPAction", "http://tempuri.org/" + soapAction);
			webRequest.ContentType = "text/xml;charset=\"utf-8\"";
			//webRequest.ContentType = "application/soap+xml; charset=utf-8";
			webRequest.Accept = "text/xml";
			webRequest.Method = "POST";
			return webRequest;
		}

	}
}
