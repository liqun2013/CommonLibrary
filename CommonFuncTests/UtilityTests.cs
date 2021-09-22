using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Tests
{
	[TestClass()]
	public class UtilityTests
	{
		[TestMethod()]
		public void ResolveHostAddressTest()
		{
			string v = Utility.ResolveHostName();
			string expected = "o-a869.Qgroup.corp.com";
			Assert.AreEqual(expected, v);

			v = Utility.ResolveHostAddress(expected);
			expected = "10.85.220.77";
			Assert.AreEqual(expected, v);
		}
	}
}
