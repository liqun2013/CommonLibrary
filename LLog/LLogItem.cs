using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLog
{
	public enum LLogType
	{
		Info, Error,
	}
	public class LLogItem
	{
		public LLogType LogType { get; set; }
		public string Msg { get; set; }
		public Exception TheException { get; set; }
	 }
}
