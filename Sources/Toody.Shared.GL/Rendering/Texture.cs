namespace Toody
{
	using System;
	using OpenTK.Graphics.ES30;
	using Toody.Shared.GL;

	public class Texture : ITexture
	{
		public string Filename { get; private set; }

		public int ID { get; private set; }

		public DrawMode Mode { get; private set; }

		public int Width { get; private set; }

		public int Height { get; private set; }

		public Texture(string filename, DrawMode drawMode = DrawMode.Nearest)
		{
			this.Filename = filename;
			this.Mode = drawMode;
		}

		public void Dispose()
		{
			GL.DeleteTexture(this.ID);
		}

		public ISprite CreateSprite() => new Sprite(this);

		public void Load(IContent content)
		{
			var glContent = Content.PlatformCast(content);
			                       
			int width, height;

			var data = glContent.Device.LoadTexture(this.Filename, out width, out height);

			this.Width = width;
			this.Height = height;

			this.ID = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, this.ID);

			//Loading data
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);

			switch (this.Mode)
			{
				case DrawMode.Nearest:
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
					break;
				default:
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
					break;
			}

			GL.BindTexture(TextureTarget.Texture2D, 0);
		}
	}
}
