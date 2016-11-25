using System;
namespace Toody
{
	public static class Assert
	{
		public static void ThrowUnsupported()
		{
			throw new InvalidOperationException("Platform dll should be referenced instead of portable one");
		}

		public static T ThrowUnsupported<T>()
		{
			ThrowUnsupported();
			return default(T);
		}
	}
}
