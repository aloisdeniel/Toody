namespace Toody.Sample
{
	using System;

	public class SampleGame : IGame
	{
		public SampleGame()
		{
		}

		private ITexture texture;

		private ISprite sprite, sprite2;

		public void Load(IContent content)
		{
			this.texture = content.Get<ITexture>("bundle://assets.png");
			this.sprite = this.texture.CreateSprite();
			this.sprite2 = this.texture.CreateSprite();
		}

		public void Draw(IRenderer renderer)
		{
			renderer.Draw(new[] { this.sprite,  this.sprite2 });
		}

		public void Update(Camera camera, double delta)
		{
			this.sprite2.Destination = new Rectangle(new Point(50, 50), this.sprite2.Destination.Size);
			camera.Rotation += (float)delta;
		}
	}
}
