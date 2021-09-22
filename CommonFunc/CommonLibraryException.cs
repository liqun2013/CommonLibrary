using System;

namespace CommonLibrary
{
	public class CommonLibraryException : Exception
	{
		public CommonLibraryException(string msg) : base(msg)
		{
		}
	}
}
