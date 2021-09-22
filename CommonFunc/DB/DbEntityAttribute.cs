using System;

namespace CommonLibrary.DB
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class DbEntityAttribute : Attribute
	{
		public string ColName { get; set; }
	}
}
