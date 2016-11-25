namespace Toody
{
	using System;

	public interface ITexture : IResource
	{
		string Filename { get; }

		int ID { get; }

		DrawMode Mode { get; }

		int Width { get; }

		int Height { get; }

		ISprite CreateSprite();
	}
}
