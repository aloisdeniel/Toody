namespace Toody.Shared.GL
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class Content : IContent
	{
		class ContentResource
		{
			public string Path { get; set; }

			public Type ResourceType { get; set; }

			public IResource Resource { get; set; }
		}

		public Content(IDevice device)
		{
			this.Device = device;
		}

		private IList<ContentResource> resources = new List<ContentResource>();

		public IDevice Device { get; private set; }

		public void Dispose()
		{
			foreach (var resource in resources)
			{
				resource.Resource.Dispose();
			}

			resources.Clear();
		}

		public T Get<T>(string path) where T : IResource
		{
			var result = this.resources.FirstOrDefault(r => r.Path == path && r.ResourceType == typeof(T))?.Resource;

			if (result != null)
				return (T)result;

			if (typeof(T) == typeof(ITexture))
			{
				result = new Texture(path);
			}
			else if (typeof(T) == typeof(ShaderProgram))
			{
				result = new ShaderProgram();
			}
			else if (typeof(T) == typeof(IRenderer))
			{
				result = new Renderer();
			}

			if (result != null)
			{
				this.resources.Add(new ContentResource()
				{
					Path = path,
					Resource = result,
					ResourceType = typeof(T),
				});

				return (T)result;
			}

			throw new InvalidOperationException($"Can't create ressource of type \"{typeof(T)}\" with given path \"{path}\"");
		}

		public void Load(IContent content)
		{
			for (int i = 0; i < resources.Count(); i++)
			{
				resources[i].Resource.Load(this);
			}
		}

		public static Content PlatformCast(IContent content)
		{
			var glContent = content as Content;
			if (glContent == null)
				throw new InvalidOperationException($"The content should be of type {typeof(Content).FullName}");

			return glContent;
		}
	}
}