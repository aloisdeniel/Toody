namespace Toody
{
	using System;

	public interface IContent : ILoadable, IDisposable
	{
		T Get<T>(string path) where T : IResource;
	}
}
