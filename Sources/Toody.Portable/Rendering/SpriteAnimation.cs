namespace Toody.Portable
{
	public class SpriteAnimation : ISpriteAnimation
	{
		public SpriteAnimation(ISprite sprite, float interval, Rectangle[] frames)
		{
			this.frames = frames;
		}

		private double time;

		private Repeat.Mode repeat;

		private readonly Rectangle[] frames;

		public float Interval { get; private set; }

		public bool IsStarted { get; private set; }

		public ISprite Sprite { get; private set; }

		public void Start(Repeat.Mode repeat)
		{
			time = 0;
			this.repeat = repeat;
			this.IsStarted = true;
		}

		public void Stop()
		{
			this.IsStarted = false;
		}

		public void Update(double delta)
		{
			if (this.IsStarted)
			{
				time += delta;

				var amount = this.UpdateSource();

				if (Repeat.IsFinished(repeat, amount))
					this.Stop();
			}
		}

		private float UpdateSource()
		{
			var amount = (float)(time / (this.Interval * this.frames.Length));
			var value = Repeat.Calculate(this.repeat, amount);
			var i = (int)(value * frames.Length);
			this.Sprite.Source = frames[i];
			return amount;
		}

		public void Reset()
		{
			time = 0;
			this.repeat = Repeat.Mode.Once;
			this.UpdateSource();
		}
	}
}
