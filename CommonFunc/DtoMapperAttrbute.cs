using System;

namespace CommonLibrary
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class DtoMapperAttrbute : Attribute
	{
		public string Mapto { get; set; }
	}
}
