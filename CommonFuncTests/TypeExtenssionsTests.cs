using CommonLibrary;
using CommonLibrary.OpenXMLExtend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonLibrary.Tests
{
	[TestClass()]
	public class TypeExtenssionsTests
	{
		[TestMethod()]
		public void ToBooleanTest()
		{
			bool v = false;
			string s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToBoolean(false); });
			s = "True";
			v = s.ToBoolean(false);
			Assert.AreEqual(true, v);
			s = "0";
			v = s.ToBoolean(false);
			Assert.AreEqual(false, v);
			s = "tr";
			Assert.ThrowsException<FormatException>(() => { v = s.ToBoolean(false); });
			s = null;
			v = s.ToBoolean(true, true);
			Assert.AreEqual(true, v);
			s = "True";
			v = s.ToBoolean(true, false);
			Assert.AreEqual(true, v);
			s = "0";
			v = s.ToBoolean(true, true);
			Assert.AreEqual(false, v);
			s = "tr";
			v = s.ToBoolean(true, true);
			Assert.AreEqual(true, v);
		}

		[TestMethod()]
		public void ToNullableBooleanTest()
		{
			bool? v = null;
			string s = null;
			v = s.ToNullableBoolean(false);
			Assert.AreEqual(null, v);
			s = "true";
			v = s.ToNullableBoolean(false);
			bool expected = true;
			Assert.AreEqual(expected, v);
			s = "0";
			v = s.ToNullableBoolean(false);
			expected = false;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableBoolean(false); });

			s = null;
			v = s.ToNullableBoolean(true);
			Assert.AreEqual(null, v);
			s = "true";
			v = s.ToNullableBoolean(true, false);
			expected = true;
			Assert.AreEqual(expected, v);
			s = "0";
			v = s.ToNullableBoolean(true, true);
			expected = false;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableBoolean(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToInt16Test()
		{
			short v = 0;
			string s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToInt16(false); });
			s = "21";
			v = s.ToInt16(false);
			Assert.AreEqual(21, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt16(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt16(false); });
			s = "10E+2";
			v = s.ToInt16(false);
			Assert.AreEqual(1000, v);

			s = null;
			v = s.ToInt16(true, 0);
			Assert.AreEqual(0, v);
			s = "21";
			v = s.ToInt16(true);
			Assert.AreEqual(21, v);
			s = "21ds";
			v = s.ToInt16(true, 0);
			Assert.AreEqual(0, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToInt16(true);
			Assert.AreEqual(0, v);
			s = "10E+2";
			v = s.ToInt16(true);
			Assert.AreEqual(1000, v);
		}

		[TestMethod()]
		public void ToNullableInt16Test()
		{
			short? v = null;
			string s = null;
			v = s.ToNullableInt16(false);
			Assert.AreEqual(null, v);
			s = "21";
			v = s.ToNullableInt16(false);
			short expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt16(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt16(false); });
			s = "10E+2";
			v = s.ToNullableInt16(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = 0;
			v = s.ToNullableInt16(true, 0);
			Assert.AreEqual(expected, v);
			s = "21";
			v = s.ToNullableInt16(true);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableInt16(true, 0);
			expected = 0;
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableInt16(true, null);
			Assert.AreEqual(null, v);
			s = "10E+2";
			v = s.ToNullableInt16(true);
			expected = 1000;
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToUInt16Test()
		{
			ushort v = 0;
			string s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToUInt16(false); });
			s = "21";
			v = s.ToUInt16(false);
			ushort expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt16(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt16(false); });
			s = "10E+2";
			v = s.ToUInt16(false);
			expected = 1000;
			Assert.AreEqual(expected, v);
			s = "-20";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt16(false); });
			s = "10E-2";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt16(false); });

			s = null;
			expected = 0;
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			v = s.ToUInt16(true);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			expected = 0;
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToUInt16(true);
			Assert.AreEqual(expected, v);
			s = "-20";
			expected = 0;
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-2";
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToNullableUInt16Test()
		{
			ushort? v = null;
			string s = null;
			v = s.ToNullableUInt16(false);
			Assert.AreEqual(null, v);
			s = "21";
			v = s.ToNullableUInt16(false);
			ushort? expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt16(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt16(false); });
			s = "10E+2";
			v = s.ToNullableUInt16(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			v = s.ToNullableUInt16(true, expected);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			v = s.ToNullableUInt16(true, expected);
			expected = 1000;
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToInt32Test()
		{
			int v = 0;
			string s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToInt32(false); });
			s = "21";
			v = s.ToInt32(false);
			Assert.AreEqual(21, v);
			s = "-21";
			v = s.ToInt32(false);
			Assert.AreEqual(-21, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt32(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt32(false); });
			s = "10E+2";
			v = s.ToInt32(false);
			Assert.AreEqual(1000, v);

			s = null;
			v = s.ToInt32(true, 0);
			Assert.AreEqual(0, v);
			s = "21";
			v = s.ToInt32(true);
			Assert.AreEqual(21, v);
			s = "-21";
			v = s.ToInt32(true);
			Assert.AreEqual(-21, v);
			s = "21ds";
			v = s.ToInt32(true, 0);
			Assert.AreEqual(0, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToInt32(true, 0);
			Assert.AreEqual(0, v);
			s = "10E+2";
			v = s.ToInt32(true);
			Assert.AreEqual(1000, v);
		}

		[TestMethod()]
		public void ToUInt32Test()
		{
			uint v = 0;
			string s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToUInt32(false); });
			s = "21";
			v = s.ToUInt32(false);
			uint expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt32(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt32(false); });
			s = "10E+2";
			v = s.ToUInt32(false);
			expected = 1000;
			Assert.AreEqual(expected, v);
			s = "-20";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt32(false); });
			s = "10E-2";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt32(false); });

			s = null;
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			v = s.ToUInt32(true, expected);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			v = s.ToUInt32(true, expected);
			expected = 1000;
			Assert.AreEqual(expected, v);
			s = "-20";
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-2";
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToNullableInt32Test()
		{
			int? v = null;
			string s = null;
			v = s.ToNullableInt32(false);
			Assert.AreEqual(null, v);
			s = "21";
			v = s.ToNullableInt32(false);
			int? expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt32(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt32(false); });
			s = "10E+2";
			v = s.ToNullableInt32(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableInt32(true, expected);
			Assert.AreEqual(null, v);
			s = "21";
			v = s.ToNullableInt32(true, expected);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			v = s.ToNullableInt32(true, expected);
			expected = 1000;
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToNullableUInt32Test()
		{
			uint? v = null;
			string s = null;
			v = s.ToNullableUInt32(false);
			Assert.AreEqual(null, v);
			s = "21";
			v = s.ToNullableUInt32(false);
			uint? expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt32(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt32(false); });
			s = "10E+2";
			v = s.ToNullableUInt32(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			v = s.ToNullableUInt32(true, expected);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			v = s.ToNullableUInt32(true, expected);
			expected = 1000;
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToInt64Test()
		{
			long v = 0;
			string s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToInt64(false); });
			s = "9223372036854775807";
			v = s.ToInt64(false);
			Assert.AreEqual(9223372036854775807, v);
			s = "-9223372036854775807";
			v = s.ToInt64(false);
			Assert.AreEqual(-9223372036854775807, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt64(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt64(false); });
			s = "10E+2";
			v = s.ToInt64(false);
			Assert.AreEqual(1000, v);

			long expected = 0;
			s = null;
			v = s.ToInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807";
			v = s.ToInt64(true, expected);
			expected = 9223372036854775807;
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807";
			v = s.ToInt64(true, expected);
			expected = -9223372036854775807;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			v = s.ToInt64(true, expected);
			Assert.AreEqual(1000, v);
		}

		[TestMethod()]
		public void ToNullableInt64Test()
		{
			long? v = null;
			string s = null;
			v = s.ToNullableInt64(false);
			Assert.AreEqual(null, v);
			s = "9223372036854775807";
			v = s.ToNullableInt64(false);
			long? expected = 9223372036854775807;
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807";
			v = s.ToNullableInt64(false);
			Assert.AreEqual(-9223372036854775807, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt64(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt64(false); });
			s = "10E+2";
			v = s.ToNullableInt64(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807";
			v = s.ToNullableInt64(true, expected);
			expected = 9223372036854775807;
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807";
			expected = -9223372036854775807;
			v = s.ToNullableInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			v = s.ToNullableInt64(true, expected);
			expected = 1000;
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToUInt64Test()
		{
			ulong v = 0;
			string s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToUInt64(false); });
			s = "9223372036854775807";
			v = s.ToUInt64(false);
			ulong expected = 9223372036854775807;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt64(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt64(false); });
			s = "10E+2";
			v = s.ToUInt64(false);
			expected = 1000;
			Assert.AreEqual(expected, v);
			s = "-20";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt64(false); });
			s = "10E-2";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt64(false); });

			s = null;
			expected = 0;
			v = s.ToUInt64(true, expected);
			s = "9223372036854775807";
			expected = 9223372036854775807;
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			v = s.ToUInt64(true, expected);
			expected = 1000;
			Assert.AreEqual(expected, v);
			s = "-20";
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-2";
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToNullableUInt64Test()
		{
			ulong? v = null;
			string s = null;
			v = s.ToNullableUInt64(false);
			Assert.AreEqual(null, v);
			s = "9223372036854775807";
			v = s.ToNullableUInt64(false);
			ulong? expected = 9223372036854775807;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt64(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt64(false); });
			s = "10E+2";
			v = s.ToNullableUInt64(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableUInt64(true, expected);
			Assert.AreEqual(null, v);
			s = "9223372036854775807";
			v = s.ToNullableUInt64(true, expected);
			expected = 9223372036854775807;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			v = s.ToNullableUInt64(true, expected);
			expected = 1000;
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToFloatTest()
		{
			float v = 0;
			string s = null;
			float expected = 9223372036854775807.9098F;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToFloat(false); });
			s = "9223372036854775807.9098";
			v = s.ToFloat(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToFloat(false);
			expected = -9223372036854775807.9098F;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToFloat(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToFloat(false); });
			s = "10E-5";
			expected = 0.0001F;
			v = s.ToFloat(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = 9223372036854775807.9098F;
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807.9098";
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToFloat(true, expected);
			expected = -9223372036854775807.9098F;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001F;
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToNullableFloatTest()
		{
			float? v = null;
			string s = null;
			float expected = 9223372036854775807.9098F;
			v = s.ToNullableFloat(false);
			Assert.AreEqual(null, v);
			s = "9223372036854775807.9098";
			v = s.ToNullableFloat(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToNullableFloat(false);
			expected = -9223372036854775807.9098F;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableFloat(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableFloat(false); });
			s = "10E-5";
			expected = 0.0001F;
			v = s.ToNullableFloat(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = 9223372036854775807.9098F;
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807.9098";
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToNullableFloat(true, expected);
			expected = -9223372036854775807.9098F;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001F;
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToDecimalTest()
		{
			decimal v = 0;
			string s = null;
			decimal expected = 9223372036854775807.9098M;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToDecimal(false); });
			s = "9223372036854775807.9098";
			v = s.ToDecimal(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToDecimal(false);
			expected = -9223372036854775807.9098M;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDecimal(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDecimal(false); });
			s = "10E-5";
			expected = 0.0001M;
			v = s.ToDecimal(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = 9223372036854775807.9098M;
			v = s.ToDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807.9098";
			v = s.ToDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToDecimal(true, expected);
			expected = -9223372036854775807.9098M;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001M;
			v = s.ToDecimal(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToNullableDecimalTest()
		{
			decimal? v = null;
			string s = null;
			decimal? expected = 9223372036854775807.9098M;
			v = s.ToNullableDecimal(false);
			Assert.AreEqual(null, v);
			s = "9223372036854775807.9098";
			v = s.ToNullableDecimal(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToNullableDecimal(false);
			expected = -9223372036854775807.9098M;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDecimal(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDecimal(false); });
			s = "10E-5";
			expected = 0.0001M;
			v = s.ToNullableDecimal(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807.9098";
			v = s.ToNullableDecimal(true, expected);
			expected = 9223372036854775807.9098M;
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToNullableDecimal(true, expected);
			expected = -9223372036854775807.9098M;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001M;
			v = s.ToNullableDecimal(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToDoubleTest()
		{
			double v = 0;
			string s = null;
			double expected = 9223372036854775807.9098;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToDouble(false); });
			s = "9223372036854775807.9098";
			v = s.ToDouble(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToDouble(false);
			expected = -9223372036854775807.9098;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDouble(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDouble(false); });
			s = "10E-5";
			expected = 0.0001;
			v = s.ToDouble(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = 9223372036854775807.9098;
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807.9098";
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToDouble(true, expected);
			expected = -9223372036854775807.9098;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001;
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToNullableDoubleTest()
		{
			double? v = null;
			string s = null;
			double? expected = 9223372036854775807.9098;
			v = s.ToNullableDouble(false);
			Assert.AreEqual(null, v);
			s = "9223372036854775807.9098";
			v = s.ToNullableDouble(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToNullableDouble(false);
			expected = -9223372036854775807.9098;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDouble(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDouble(false); });
			s = "10E-5";
			expected = 0.0001;
			v = s.ToNullableDouble(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableDouble(true, expected);
			Assert.AreEqual(null, v);
			s = "9223372036854775807.9098";
			v = s.ToNullableDouble(true, expected);
			expected = 9223372036854775807.9098;
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToNullableDouble(true, expected);
			expected = -9223372036854775807.9098;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001;
			v = s.ToNullableDouble(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToDateTimeTest()
		{
			DateTime v = DateTime.MinValue;
			string s = null;
			DateTime expected = DateTime.MinValue;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToDateTime(false); });
			s = "2020/10/10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToDateTime(false);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			v = s.ToDateTime(false);
			expected = DateTime.Parse(s);
			Assert.AreEqual(expected, v);
			s = "2020/10/1023:20:50";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDateTime(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDateTime(false); });
			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToDateTime("yyyy-MM-dd HH:mm:ss", false);
			Assert.AreEqual(expected, v);
			s = "20-9-10 23:2:50";
			expected = DateTime.Parse(s);
			v = s.ToDateTime("yy-M-dd HH:m:ss", false);
			Assert.AreEqual(expected, v);
			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToDateTime(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = DateTime.MinValue;
			v = s.ToDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			v = s.ToDateTime(true, expected);
			expected = DateTime.Parse(s);
			Assert.AreEqual(expected, v);
			s = "2020/10/1023:20:50";
			v = s.ToDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToDateTime("yyyy-MM-dd HH:mm:ss", true, expected);
			Assert.AreEqual(expected, v);
			s = "20-9-10 23:2:50";
			expected = DateTime.Parse(s);
			v = s.ToDateTime("yy-M-dd HH:m:ss", true, expected);
			Assert.AreEqual(expected, v);
			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToDateTime(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToNullableDateTimeTest()
		{
			DateTime? v = null;
			string s = null;
			DateTime? expected = DateTime.MinValue;
			v = s.ToNullableDateTime(false);
			Assert.AreEqual(null, v);
			s = "2020/10/10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToNullableDateTime(false);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			v = s.ToNullableDateTime(false);
			expected = DateTime.Parse(s);
			Assert.AreEqual(expected, v);
			s = "2020/10/1023:20:50";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDateTime(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDateTime(false); });
			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToNullableDateTime("yyyy-MM-dd HH:mm:ss", false);
			Assert.AreEqual(expected, v);
			s = "20-9-10 23:2:50";
			expected = DateTime.Parse(s);
			v = s.ToNullableDateTime("yy-M-dd HH:m:ss", false);
			Assert.AreEqual(expected, v);
			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToNullableDateTime(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToNullableDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			v = s.ToNullableDateTime(true, expected);
			expected = DateTime.Parse(s);
			Assert.AreEqual(expected, v);
			s = "2020/10/1023:20:50";
			v = s.ToNullableDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToNullableDateTime("yyyy-MM-dd HH:mm:ss", true, expected);
			Assert.AreEqual(expected, v);
			s = "20-9-10 23:2:50";
			expected = DateTime.Parse(s);
			v = s.ToNullableDateTime("yy-M-dd HH:m:ss", true, expected);
			Assert.AreEqual(expected, v);
			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse(s);
			v = s.ToNullableDateTime(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToInt16Test()
		{
			short v = 0;
			object s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToInt16(false); });
			s = "21";
			v = s.ToInt16(false);
			Assert.AreEqual(21, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt16(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt16(false); });
			s = "10E+2";
			v = s.ToInt16(false);
			Assert.AreEqual(1000, v);

			short expected = 0;
			s = null;
			v = s.ToInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			expected = 21;
			v = s.ToInt16(true, expected);
			Assert.AreEqual(21, v);
			s = "21ds";
			v = s.ToInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToInt16(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToNullableInt16Test()
		{
			short? v = null;
			object s = null;
			v = s.ToNullableInt16(false);
			Assert.AreEqual(null, v);
			s = "21";
			v = s.ToNullableInt16(false);
			short? expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt16(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt16(false); });
			s = "10E+2";
			v = s.ToNullableInt16(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableInt16(true, expected);
			Assert.AreEqual(null, v);
			s = "21";
			expected = 21;
			v = s.ToNullableInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			v = s.ToNullableInt16(true, expected);
			expected = 1000;
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToUInt16Test()
		{
			ushort v = 0;
			object s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToUInt16(false); });
			s = "21";
			v = s.ToUInt16(false);
			ushort expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt16(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt16(false); });
			s = "10E+2";
			v = s.ToUInt16(false);
			expected = 1000;
			Assert.AreEqual(expected, v);
			s = "-20";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt16(false); });
			s = "10E-2";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt16(false); });

			s = null;
			expected = 0;
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			v = s.ToUInt16(true, expected);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "-20";
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-2";
			v = s.ToUInt16(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToNullableUInt16Test()
		{
			ushort? v = null;
			object s = null;
			v = s.ToNullableUInt16(false);
			Assert.AreEqual(null, v);
			s = "21";
			v = s.ToNullableUInt16(false);
			ushort? expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt16(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt16(false); });
			s = "10E+2";
			v = s.ToNullableUInt16(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			v = s.ToNullableUInt16(true, expected);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			s.ToNullableUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableUInt16(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			v = s.ToNullableUInt16(true, expected);
			expected = 1000;
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToInt32Test()
		{
			int v = 0;
			object s = null;
			int expected = 0;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToInt32(false); });
			s = "21";
			v = s.ToInt32(false);
			Assert.AreEqual(21, v);
			s = "-21";
			v = s.ToInt32(false);
			Assert.AreEqual(-21, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt32(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt32(false); });
			s = "10E+2";
			v = s.ToInt32(false);
			Assert.AreEqual(1000, v);

			s = null;
			expected = 0;
			v = s.ToInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			expected = 21;
			v = s.ToInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "-21";
			expected = -21;
			v = s.ToInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			expected = 0;
			v = s.ToInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			expected = 0;
			v = s.ToInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToInt32(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToUInt32Test()
		{
			uint v = 0;
			object s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToUInt32(false); });
			s = "21";
			v = s.ToUInt32(false);
			uint expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt32(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt32(false); });
			s = "10E+2";
			v = s.ToUInt32(false);
			expected = 1000;
			Assert.AreEqual(expected, v);
			s = "-20";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt32(false); });
			s = "10E-2";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt32(false); });

			s = null;
			expected = 0;
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			expected = 21;
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			expected = 0;
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			expected = 0;
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "-20";
			expected = 0;
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-2";
			expected = 0;
			v = s.ToUInt32(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToNullableInt32Test()
		{
			int? v = null;
			int? expected = null;
			object s = null;
			v = s.ToNullableInt32(false);
			Assert.AreEqual(null, v);
			s = "21";
			expected = 21;
			v = s.ToNullableInt32(false);
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt32(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt32(false); });
			s = "10E+2";
			v = s.ToNullableInt32(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			expected = 21;
			v = s.ToNullableInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			expected = null;
			v = s.ToNullableInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			expected = null;
			v = s.ToNullableInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToNullableInt32(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToNullableUInt32Test()
		{
			uint? v = null;
			uint? expected = null;
			object s = null;
			v = s.ToNullableUInt32(false);
			Assert.AreEqual(null, v);
			s = "21";
			expected = 21;
			v = s.ToNullableUInt32(false);
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt32(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt32(false); });
			s = "10E+2";
			v = s.ToNullableUInt32(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			v = s.ToNullableUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			expected = 21;
			v = s.ToNullableUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			expected = 21;
			v = s.ToNullableUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			expected = 21;
			v = s.ToNullableUInt32(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToNullableUInt32(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToInt64Test()
		{
			long v = 0;
			long expected = 0;
			object s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToInt64(false); });
			s = "9223372036854775807";
			v = s.ToInt64(false);
			Assert.AreEqual(9223372036854775807, v);
			s = "-9223372036854775807";
			v = s.ToInt64(false);
			Assert.AreEqual(-9223372036854775807, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt64(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToInt64(false); });
			s = "10E+2";
			v = s.ToInt64(false);
			Assert.AreEqual(1000, v);

			s = null;
			expected = 0;
			v = s.ToInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807";
			expected = 9223372036854775807;
			v = s.ToInt64(true, expected);
			Assert.AreEqual(9223372036854775807, v);
			s = "-9223372036854775807";
			expected = -9223372036854775807;
			v = s.ToInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToInt64(false);
			Assert.AreEqual(1000, v);
		}

		[TestMethod()]
		public void ObjToNullableInt64Test()
		{
			long? v = null;
			object s = null;
			long? expected = 9223372036854775807;
			v = s.ToNullableInt64(false);
			Assert.AreEqual(null, v);
			s = "9223372036854775807";
			v = s.ToNullableInt64(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807";
			v = s.ToNullableInt64(false);
			Assert.AreEqual(-9223372036854775807, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt64(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableInt64(false); });
			s = "10E+2";
			v = s.ToNullableInt64(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807";
			expected = 9223372036854775807;
			v = s.ToNullableInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807";
			expected = -9223372036854775807;
			v = s.ToNullableInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			expected = 99;
			v = s.ToNullableInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToNullableInt64(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToUInt64Test()
		{
			ulong v = 0;
			object s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToUInt64(false); });
			s = "9223372036854775807";
			v = s.ToUInt64(false);
			ulong expected = 9223372036854775807;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt64(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt64(false); });
			s = "10E+2";
			v = s.ToUInt64(false);
			expected = 1000;
			Assert.AreEqual(expected, v);
			s = "-20";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt64(false); });
			s = "10E-2";
			Assert.ThrowsException<FormatException>(() => { v = s.ToUInt64(false); });

			s = null;
			expected = 0;
			v = s.ToUInt64(true, expected);
			s = "9223372036854775807";
			expected = 9223372036854775807;
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "-20";
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-2";
			v = s.ToUInt64(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToNullableUInt64Test()
		{
			ulong? v = null;
			object s = null;
			v = s.ToNullableUInt64(false);
			Assert.AreEqual(null, v);
			s = "9223372036854775807";
			v = s.ToNullableUInt64(false);
			ulong? expected = 9223372036854775807;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt64(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableUInt64(false); });
			s = "10E+2";
			v = s.ToNullableUInt64(false);
			expected = 1000;
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807";
			v = s.ToNullableUInt64(true, expected);
			expected = 9223372036854775807;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableUInt64(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E+2";
			expected = 1000;
			v = s.ToNullableUInt64(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToFloatTest()
		{
			float v = 0;
			object s = null;
			float expected = 9223372036854775807.9098F;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToFloat(false); });
			s = "9223372036854775807.9098";
			v = s.ToFloat(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToFloat(false);
			expected = -9223372036854775807.9098F;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToFloat(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToFloat(false); });
			s = "10E-5";
			expected = 0.0001F;
			v = s.ToFloat(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = 9223372036854775807.9098F;
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807.9098";
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			expected = -9223372036854775807.9098F;
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001F;
			v = s.ToFloat(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToNullableFloatTest()
		{
			float? v = null;
			object s = null;
			float? expected = 9223372036854775807.9098F;
			Assert.AreEqual(null, v);
			s = "9223372036854775807.9098";
			v = s.ToNullableFloat(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToNullableFloat(false);
			expected = -9223372036854775807.9098F;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableFloat(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableFloat(false); });
			s = "10E-5";
			expected = 0.0001F;
			v = s.ToNullableFloat(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(null, v);
			s = "9223372036854775807.9098";
			expected = 9223372036854775807.9098F;
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			expected = -9223372036854775807.9098F;
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001F;
			v = s.ToNullableFloat(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToDecimalTest()
		{
			decimal v = 0;
			object s = null;
			decimal expected = 9223372036854775807.9098M;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToDecimal(false); });
			s = "9223372036854775807.9098";
			v = s.ToDecimal(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToDecimal(false);
			expected = -9223372036854775807.9098M;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDecimal(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDecimal(false); });
			s = "10E-5";
			expected = 0.0001M;
			v = s.ToDecimal(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = 9223372036854775807.9098M;
			v = s.ToDecimal(true, expected);
			s = "9223372036854775807.9098";
			v = s.ToDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			expected = -9223372036854775807.9098M;
			v = s.ToDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001M;
			v = s.ToDecimal(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToNullableDecimalTest()
		{
			decimal? v = null;
			object s = null;
			decimal? expected = 9223372036854775807.9098M;
			Assert.AreEqual(null, v);
			s = "9223372036854775807.9098";
			v = s.ToNullableDecimal(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToNullableDecimal(false);
			expected = -9223372036854775807.9098M;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDecimal(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDecimal(false); });
			s = "10E-5";
			expected = 0.0001M;
			v = s.ToNullableDecimal(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807.9098";
			expected = 9223372036854775807.9098M;
			v = s.ToNullableDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			expected = -9223372036854775807.9098M;
			v = s.ToNullableDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableDecimal(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001M;
			v = s.ToNullableDecimal(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToDoubleTest()
		{
			double v = 0;
			object s = null;
			double expected = 9223372036854775807.9098;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToDouble(false); });
			s = "9223372036854775807.9098";
			v = s.ToDouble(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToDouble(false);
			expected = -9223372036854775807.9098;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDouble(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDouble(false); });
			s = "10E-5";
			expected = 0.0001;
			v = s.ToDouble(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = 0;
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "9223372036854775807.9098";
			expected = 9223372036854775807.9098;
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			expected = -9223372036854775807.9098;
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "10E-5";
			expected = 0.0001;
			v = s.ToDouble(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToNullableDoubleTest()
		{
			double? v = null;
			object s = null;
			double? expected = 9223372036854775807.9098;
			v = s.ToNullableDouble(false);
			Assert.AreEqual(null, v);
			s = "9223372036854775807.9098";
			v = s.ToNullableDouble(false);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			v = s.ToNullableDouble(false);
			expected = -9223372036854775807.9098;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDouble(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDouble(false); });
			s = "10E-5";
			expected = 0.0001;
			v = s.ToNullableDouble(false);
			Assert.AreEqual(expected, v);

			s = null;
			expected = null;
			v = s.ToNullableDouble(true, expected);
			Assert.AreEqual(null, v);
			s = "9223372036854775807.9098";
			expected = 9223372036854775807.9098;
			v = s.ToNullableDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "-9223372036854775807.9098";
			expected = -9223372036854775807.9098;
			v = s.ToNullableDouble(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableDouble(true, expected);
			s = "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableDouble(true, expected);
			s = "10E-5";
			expected = 0.0001;
			v = s.ToNullableDouble(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToDateTimeTest()
		{
			DateTime v = DateTime.MinValue;
			object s = null;
			DateTime expected = DateTime.MinValue;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToDateTime(false); });
			s = "2020/10/10 23:20:50";
			expected = DateTime.Parse("2020/10/10 23:20:50");
			v = s.ToDateTime(false);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			v = s.ToDateTime(false);
			expected = DateTime.Parse("2020/10/10 23:20:50");
			Assert.AreEqual(expected, v);
			s = "2020/10/1023:20:50";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDateTime(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToDateTime(false); });

			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse("2020-10-10 23:20:50");
			v = s.ToDateTime("yyyy-MM-dd HH:mm:ss", false);
			Assert.AreEqual(expected, v);

			s = "20-9-10 23:2:50";
			expected = DateTime.Parse("20-9-10 23:2:50");
			v = s.ToDateTime("yy-M-dd HH:m:ss", false);
			Assert.AreEqual(expected, v);


			s = null;
			expected = DateTime.MinValue;
			v = s.ToDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			expected = DateTime.Parse("2020/10/10 23:20:50");
			v = s.ToDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			v = s.ToDateTime(true, expected);
			expected = DateTime.Parse("2020/10/10 23:20:50");
			Assert.AreEqual(expected, v);
			s = "2020/10/1023:20:50";
			v = s.ToDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToDateTime(true, expected);
			Assert.AreEqual(expected, v);

			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse("2020-10-10 23:20:50");
			v = s.ToDateTime("yyyy-MM-dd HH:mm:ss", true, expected);
			Assert.AreEqual(expected, v);

			s = "20-9-10 23:2:50";
			expected = DateTime.Parse("20-9-10 23:2:50");
			v = s.ToDateTime("yy-M-dd HH:m:ss", true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToNullableDateTimeTest()
		{
			DateTime? v = null;
			object s = null;
			DateTime? expected = DateTime.MinValue;
			s.ToNullableDateTime(false);
			Assert.AreEqual(null, v);
			s = "2020/10/10 23:20:50";
			expected = DateTime.Parse("2020/10/10 23:20:50");
			v = s.ToNullableDateTime(false);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			v = s.ToNullableDateTime(false);
			expected = DateTime.Parse("2020/10/10 23:20:50");
			Assert.AreEqual(expected, v);
			s = "2020/10/1023:20:50";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDateTime(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableDateTime(false); });

			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse("2020-10-10 23:20:50");
			v = s.ToNullableDateTime("yyyy-MM-dd HH:mm:ss", false);
			Assert.AreEqual(expected, v);

			s = "20-9-10 23:2:50";
			expected = DateTime.Parse("20-9-10 23:2:50");
			v = s.ToNullableDateTime("yy-M-dd HH:m:ss", false);
			Assert.AreEqual(expected, v);


			s = null;
			expected = null;
			v = s.ToNullableDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			expected = DateTime.Parse("2020/10/10 23:20:50");
			v = s.ToNullableDateTime(true, expected);
			Assert.AreEqual(expected, v);
			s = "2020/10/10 23:20:50";
			v = s.ToNullableDateTime(true, expected);
			expected = DateTime.Parse("2020/10/10 23:20:50");
			Assert.AreEqual(expected, v);
			s = "2020/10/1023:20:50";
			v = s.ToNullableDateTime(true, expected);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableDateTime(true, expected);

			s = "2020-10-10 23:20:50";
			expected = DateTime.Parse("2020-10-10 23:20:50");
			v = s.ToNullableDateTime("yyyy-MM-dd HH:mm:ss", true, expected);
			Assert.AreEqual(expected, v);

			s = "20-9-10 23:2:50";
			expected = DateTime.Parse("20-9-10 23:2:50");
			v = s.ToNullableDateTime("yy-M-dd HH:m:ss", true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ObjToBooleanTest()
		{
			bool v = false;
			object s = null;
			bool expected = false;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToBoolean(false); });
			s = "True";
			v = s.ToBoolean(false);
			Assert.AreEqual(true, v);
			s = "0";
			v = s.ToBoolean(false);
			Assert.AreEqual(false, v);
			s = "tr";
			Assert.ThrowsException<FormatException>(() => { v = s.ToBoolean(false); });

			s = null;
			expected = false;
			v = s.ToBoolean(true, expected);
			s = "True";
			expected = true;
			v = s.ToBoolean(true, expected);
			Assert.AreEqual(true, v);
			s = "0";
			expected = false;
			v = s.ToBoolean(true, expected);
			Assert.AreEqual(false, v);
			s = "tr";
			expected = false;
			v = s.ToBoolean(true, expected);
		}

		[TestMethod()]
		public void ObjToNullableBooleanTest()
		{
			bool? v = null;
			object s = null;
			v = s.ToNullableBoolean(false);
			Assert.AreEqual(null, v);
			s = "true";
			v = s.ToNullableBoolean(false);
			bool? expected = true;
			Assert.AreEqual(expected, v);
			s = "0";
			v = s.ToNullableBoolean(false);
			expected = false;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableBoolean(false); });

			s = null;
			expected = null;
			v = s.ToNullableBoolean(true, expected);
			Assert.AreEqual(expected, v);
			s = "true";
			v = s.ToNullableBoolean(true, expected);
			expected = true;
			Assert.AreEqual(expected, v);
			s = "0";
			expected = false;
			v = s.ToNullableBoolean(true, expected);
			Assert.AreEqual(expected, v);
			s = "21ds";
			expected = false;
			v = s.ToNullableBoolean(true, expected);
		}

		[TestMethod()]
		public void ToCharTest()
		{
			char v = char.MinValue;
			string s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToChar(false); });
			s = "T";
			v = s.ToChar(false);
			Assert.AreEqual('T', v);
			s = "";
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToChar(false); });
			s = "0";
			v = s.ToChar(false);
			Assert.AreEqual('0', v);
			s = "hjj";
			Assert.ThrowsException<FormatException>(() => { v = s.ToChar(false); });
			s = null;
			v = s.ToChar(true, ' ');
			Assert.AreEqual(' ', v);
			s = "Tu";
			v = s.ToChar(true, 'a');
			Assert.AreEqual('a', v);
			s = "";
			v = s.ToChar(true, ' ');
			Assert.AreEqual(' ', v);
			s = "0";
			v = s.ToChar(true);
			Assert.AreEqual('0', v);
			s = "hjj";
			v = s.ToChar(true, ' ');
			Assert.AreEqual(' ', v);
		}

		[TestMethod()]
		public void ToNullableCharTest()
		{
			char? v = null;
			string s = null;
			v = s.ToNullableChar(false);
			Assert.AreEqual(null, v);
			s = "T";
			v = s.ToNullableChar(false);
			Assert.AreEqual('T', v);
			s = "";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableChar(false); });
			//			Assert.AreEqual(null, v);
			s = "0";
			v = s.ToNullableChar(false);
			Assert.AreEqual('0', v);
			s = "hjj";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableChar(false); });
			s = null;
			v = s.ToNullableChar(true, null);
			Assert.AreEqual(null, v);
			s = "T";
			v = s.ToNullableChar(true, 'T');
			Assert.AreEqual('T', v);
			s = "";
			v = s.ToNullableChar(true, null);
			Assert.AreEqual(null, v);
			s = "0";
			v = s.ToNullableChar(true, '0');
			Assert.AreEqual('0', v);
			s = "hjj";
			v = s.ToNullableChar(true, ' ');
			Assert.AreEqual(' ', v);
		}

		[TestMethod()]
		public void ObjToCharTest()
		{
			char v = char.MinValue;
			object s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToChar(false); });
			s = "T";
			v = s.ToChar(false);
			Assert.AreEqual('T', v);
			s = "";
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToChar(false); });
			s = "0";
			v = s.ToChar(false);
			Assert.AreEqual('0', v);
			s = "hjj";
			Assert.ThrowsException<FormatException>(() => { v = s.ToChar(false); });

			s = null;
			v = s.ToChar(true, ' ');
			Assert.AreEqual(' ', v);
			s = "T";
			v = s.ToChar(true, 'T');
			Assert.AreEqual('T', v);
			s = "";
			v = s.ToChar(true, ' ');
			s = "0";
			v = s.ToChar(true, '0');
			Assert.AreEqual('0', v);
			s = "hjj";
			v = s.ToChar(true, ' ');
			Assert.AreEqual(' ', v);
		}

		[TestMethod()]
		public void ObjToNullableCharTest()
		{
			char? v = null;
			object s = null;
			v = s.ToNullableChar(false);
			Assert.AreEqual(null, v);
			s = "T";
			v = s.ToNullableChar(false);
			Assert.AreEqual('T', v);
			s = "";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableChar(false); });
			s = "0";
			v = s.ToNullableChar(false);
			Assert.AreEqual('0', v);
			s = "hjj";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableChar(false); });

			s = null;
			v = s.ToNullableChar(true, null);
			Assert.AreEqual(null, v);
			s = "T";
			v = s.ToNullableChar(true, 'T');
			Assert.AreEqual('T', v);
			s = "";
			v = s.ToNullableChar(true, null);
			Assert.AreEqual(null, v);
			s = "0";
			v = s.ToNullableChar(true, '0');
			Assert.AreEqual('0', v);
			s = "hjj";
			v = s.ToNullableChar(true, ' ');
			Assert.AreEqual(' ', v);
		}

		[TestMethod()]
		public void ToEnglishWordsTest()
		{
			int n = 2980;
			long n2 = 324523;
			string s = n.ToEnglishWords();
			Assert.AreEqual("two thousand nine hundred eighty", s);
			s = n2.ToEnglishWords();
			Assert.AreEqual("three hundred twenty-four thousand five hundred twenty-three", s);
		}

		[TestMethod()]
		public void IsNumericTest()
		{
			string s = "asd9";
			bool r = s.IsNumeric();
			Assert.IsFalse(r);
			s = "34234";
			r = s.IsNumeric();
			Assert.IsTrue(r);
			s = "45345.938";
			r = s.IsNumeric();
			Assert.IsTrue(r);
		}

		[TestMethod()]
		public void IsNumericTest1()
		{
			object s = "asd9";
			bool r = s.IsNumeric();
			Assert.IsFalse(r);
			s = "34234";
			r = s.IsNumeric();
			Assert.IsTrue(r);
			s = "45345.938";
			r = s.IsNumeric();
			Assert.IsTrue(r);

			object o = new OpenXMLExtend.ExcelWrapper();

			r = o.IsNumeric();
			Assert.IsFalse(r);
		}

		[TestMethod()]
		public void SubStringTest()
		{
			string s = "fihwiogrewfgjwepoj士大夫v发";
			string v = s.SubString(1, 10, false);
			Assert.AreEqual(10, v.Length);

			v = s.SubString(s.Length - 2, 3, false);
			Assert.AreEqual(2, v.Length);

			v = s.SubString(s.Length - 2, 2, false);
			Assert.AreEqual(2, v.Length);

			s = string.Empty;
			v = s.SubString(1, 3, true, "a");
			Assert.AreEqual("a", v);
		}

		[TestMethod()]
		public void ToEnumTest()
		{
			string s = "Middle";
			var v = s.ToEnum<VerticalAlignments>(false, false);
			VerticalAlignments expected = VerticalAlignments.Middle;
			Assert.AreEqual(expected.ToString(), v.ToString());

			s = "asd";
			Assert.ThrowsException<FormatException>(() => { v = s.ToEnum<VerticalAlignments>(false, false); });

			v = s.ToEnum<VerticalAlignments>(false, true, VerticalAlignments.Default);
			expected = VerticalAlignments.Default;
			Assert.AreEqual(expected, v);

			s = "";
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToEnum<VerticalAlignments>(false, false); });
		}

		[TestMethod()]
		public void ToByteTest()
		{
			byte v = 0;
			string s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToByte(false); });
			s = "21";
			v = s.ToByte(false);
			Assert.AreEqual(21, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToByte(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToByte(false); });
			s = "-21";
			Assert.ThrowsException<FormatException>(() => { v = s.ToByte(false); });

			s = null;
			v = s.ToByte(true, 0);
			Assert.AreEqual(0, v);
			s = "21";
			v = s.ToByte(true);
			Assert.AreEqual(21, v);
			s = "21ds";
			v = s.ToByte(true, 0);
			Assert.AreEqual(0, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToByte(true);
			Assert.AreEqual(0, v);
			s = "-21";
			v = s.ToByte(true, 0);
			Assert.AreEqual(0, v);
		}

		[TestMethod()]
		public void ToNullableByteTest()
		{
			byte? v = null;
			string s = null;
			v = s.ToNullableByte(false);
			Assert.AreEqual(null, v);
			s = "21";
			v = s.ToNullableByte(false);
			byte expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableByte(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableByte(false); });

			s = null;
			expected = 0;
			v = s.ToNullableByte(true, 0);
			Assert.AreEqual(expected, v);
			s = "21";
			v = s.ToNullableByte(true);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableByte(true, 0);
			expected = 0;
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableByte(true, null);
			Assert.AreEqual(null, v);
		}

		[TestMethod()]
		public void ToSbyteTest()
		{
			sbyte v = 0;
			string s = null;
			Assert.ThrowsException<ArgumentNullException>(() => { v = s.ToSbyte(false); });
			s = "21";
			v = s.ToSbyte(false);
			sbyte expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToSbyte(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToSbyte(false); });
			s = "-20";
			v = s.ToSbyte(false);
			expected = -20;
			Assert.AreEqual(expected, v);

			s = null;
			expected = 0;
			v = s.ToSbyte(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			v = s.ToSbyte(true);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			expected = 0;
			v = s.ToSbyte(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToSbyte(true, expected);
			Assert.AreEqual(expected, v);
			s = "-20";
			expected = -20;
			v = s.ToSbyte(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void ToNullableSbyteTest()
		{
			sbyte? v = null;
			string s = null;
			v = s.ToNullableSbyte(false);
			Assert.AreEqual(null, v);
			s = "21";
			v = s.ToNullableSbyte(false);
			sbyte? expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableSbyte(false); });
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			Assert.ThrowsException<FormatException>(() => { v = s.ToNullableSbyte(false); });

			s = null;
			expected = null;
			v = s.ToNullableSbyte(true, expected);
			Assert.AreEqual(expected, v);
			s = "21";
			v = s.ToNullableSbyte(true, expected);
			expected = 21;
			Assert.AreEqual(expected, v);
			s = "21ds";
			v = s.ToNullableSbyte(true, expected);
			Assert.AreEqual(expected, v);
			s = "999999999999999999999999999999999999999999999999999999999999999999";
			v = s.ToNullableSbyte(true, expected);
			Assert.AreEqual(expected, v);
		}

		[TestMethod()]
		public void CheckNotNullTest()
		{
			int a = 2;
			int? b = null;
			Assert.ThrowsException<ArgumentNullException>(() => { b.CheckNotNull(); });
			b = a.CheckNotNull();
			Assert.AreEqual(a, b.Value);
			object o = null;
			Assert.ThrowsException<ArgumentNullException>(() => { o.CheckNotNull(); });
			Email.EmailOptions emailOptions = null;
			Assert.ThrowsException<ArgumentNullException>(() => { emailOptions.CheckNotNull(); });
			SheetCellItem sheetCellItem = new SheetCellItem();
			var sh = sheetCellItem.CheckNotNull();
			Assert.AreSame(sheetCellItem, sh);
		}
	}
}