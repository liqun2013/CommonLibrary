using System;
using System.Collections.Generic;

namespace CommonLibrary
{
	internal class EnglishNumberToWordsConverter
	{
		private static readonly string[] unitsMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
		private static readonly string[] tensMap = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

		private static readonly Dictionary<long, string> ordinalExceptions = new Dictionary<long, string> {
						{1, "first"},
						{2, "second"},
						{3, "third"},
						{4, "fourth"},
						{5, "fifth"},
						{8, "eighth"},
						{9, "ninth"},
						{12, "twelfth"},
				};

		public string Convert(long number)
		{
			return Convert(number, false);
		}

		public string Convert(long number, bool addAnd = true)
		{
			return Convert(number, false, addAnd);
		}

		public string ConvertToOrdinal(int number)
		{
			return Convert(number, true);
		}

		private string Convert(long number, bool isOrdinal, bool addAnd = true)
		{
			if (number == 0)
			{
				return GetUnitValue(0, isOrdinal);
			}

			if (number < 0)
			{
				return string.Format("minus {0}", Convert(-number));
			}

			var parts = new List<string>();

			if ((number / 1000000000000000000) > 0)
			{
				parts.Add(string.Format("{0} quintillion", Convert(number / 1000000000000000000)));
				number %= 1000000000000000000;
			}

			if ((number / 1000000000000000) > 0)
			{
				parts.Add(string.Format("{0} quadrillion", Convert(number / 1000000000000000)));
				number %= 1000000000000000;
			}

			if ((number / 1000000000000) > 0)
			{
				parts.Add(string.Format("{0} trillion", Convert(number / 1000000000000)));
				number %= 1000000000000;
			}

			if ((number / 1000000000) > 0)
			{
				parts.Add(string.Format("{0} billion", Convert(number / 1000000000)));
				number %= 1000000000;
			}

			if ((number / 1000000) > 0)
			{
				parts.Add(string.Format("{0} million", Convert(number / 1000000)));
				number %= 1000000;
			}

			if ((number / 1000) > 0)
			{
				parts.Add(string.Format("{0} thousand", Convert(number / 1000)));
				number %= 1000;
			}

			if ((number / 100) > 0)
			{
				parts.Add(string.Format("{0} hundred", Convert(number / 100)));
				number %= 100;
			}

			if (number > 0)
			{
				if (parts.Count != 0 && addAnd)
				{
					parts.Add("and");
				}

				if (number < 20)
				{
					parts.Add(GetUnitValue(number, isOrdinal));
				}
				else
				{
					var lastPart = tensMap[number / 10];
					if ((number % 10) > 0)
					{
						lastPart += string.Format("-{0}", GetUnitValue(number % 10, isOrdinal));
					}
					else if (isOrdinal)
					{
						lastPart = lastPart.TrimEnd('y') + "ieth";
					}

					parts.Add(lastPart);
				}
			}
			else if (isOrdinal)
			{
				parts[parts.Count - 1] += "th";
			}

			var toWords = string.Join(" ", parts.ToArray());

			if (isOrdinal)
			{
				toWords = RemoveOnePrefix(toWords);
			}

			return toWords;
		}

		private string GetUnitValue(long number, bool isOrdinal)
		{
			if (isOrdinal)
			{
				if (ExceptionNumbersToWords(number, out var exceptionString))
				{
					return exceptionString;
				}
				else
				{
					return unitsMap[number] + "th";
				}
			}
			else
			{
				return unitsMap[number];
			}
		}

		private string RemoveOnePrefix(string toWords)
		{
			// one hundred => hundredth
			if (toWords.StartsWith("one", StringComparison.Ordinal))
			{
				toWords = toWords.Remove(0, 4);
			}

			return toWords;
		}

		private bool ExceptionNumbersToWords(long number, out string words)
		{
			return ordinalExceptions.TryGetValue(number, out words);
		}
	}
}
