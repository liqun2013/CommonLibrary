using System;

namespace CommonLibrary
{
	public sealed class Singleton
	{
		private static readonly Lazy<Singleton> lazy = new Lazy<Singleton>(() => new Singleton());

		public static Singleton Instance => lazy.Value;//{ get { return lazy.Value; } }

		private Singleton()
		{
		}
	}
}
