namespace Toody.Sample
{
	using System;

	public class SampleGame : IGame
	{
		public SampleGame()
		{
		}

		private ITexture texture;

		private ISprite sprite;

		public void Load(IContent content)
		{
			this.texture = content.Get<ITexture>("bundle://assets.png");
			this.sprite = this.texture.CreateSprite();
		}

		public void Draw(IRenderer renderer)
		{
			renderer.Draw(new[] { this.sprite });
		}

		public void Update(Camera camera, double delta)
		{
			//camera.Rotation += (float)delta;
		}
	}
}
