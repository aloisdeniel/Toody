namespace Toody.Sample
{
	using System;

	public class SampleGame : IGame
	{
		public SampleGame()
		{
		}

		private Tween<Point> tween;
		private Tween<Color> colortween;
		private Tween<float> rotationtween;
		private Tween<float> scaletween;

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
			if (this.tween == null)
			{
				this.tween = Tween.Point(this.sprite2.Destination, new Point(200, 200), 1, Easing.Mode.EaseBoth, Repeat.Mode.LoopWithReverse);
				this.colortween = Tween.Color(Color.White, new Color(1, 0, 0, 1), 2, Easing.Mode.EaseBoth, Repeat.Mode.LoopWithReverse);
				this.rotationtween = Tween.Float(0, (float)(Math.PI * 2), 3, Easing.Mode.EaseBoth, Repeat.Mode.LoopWithReverse);
				this.scaletween = Tween.Float(1, 0.5f, 4, Easing.Mode.EaseBoth, Repeat.Mode.LoopWithReverse);
			}

			this.tween.Update(delta);
			this.colortween.Update(delta);
			this.rotationtween.Update(delta);
			this.scaletween.Update(delta);
			this.sprite2.Color = this.colortween.Value;;
			this.sprite2.Destination = this.tween.Value;
			this.sprite2.Rotation = this.rotationtween.Value;
			this.sprite2.Scale = this.scaletween.Value;
			//this.sprite2.Rotation += (float)delta;
			//camera.Rotation += (float)delta;
		}
	}
}
