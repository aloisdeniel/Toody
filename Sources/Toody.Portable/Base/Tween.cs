namespace Toody
{
	using System;

	public static class Tween
	{
		public static Tween<Rectangle> Move(Rectangle from, Rectangle to, double duration, Easing.Mode easing, Repeat.Mode repeat = Repeat.Mode.Once) => new Tween<Rectangle>(from, to, duration, easing,repeat, (amount, start, end) => new Rectangle((start.Location + (end.Location - start.Location) * (float)amount), (start.Size + (end.Size - start.Size) * (float)amount)));

		public static Tween<Point> Move(Point from, Point to, double duration, Easing.Mode easing, Repeat.Mode repeat = Repeat.Mode.Once) => new Tween<Point>(from,to,duration,easing,repeat,(amount, start, end) => (start + (end - start) * (float)amount));

		public static Tween<float> Rotate(float from, float to, double duration, Easing.Mode easing, Repeat.Mode repeat = Repeat.Mode.Once) => new Tween<float>(from, to, duration, easing,repeat, (amount, start, end) => (start + (end - start) * (float)amount));
	}

	public class Tween<T> : IUpdatable
	{
		internal Tween(T start, T end, double duration, Easing.Mode easing, Repeat.Mode repeat, Func<double, T, T, T> update)
		{
			this.Start = start;
			this.End = end;
			this.Duration = duration;
			this.Easing = easing;
			this.Repeat = repeat;
			this.update = update;
		}

		private Func<double, T, T, T> update;

		public T Start { get; private set; }

		public T End { get; private set; }

		public T Value { get; private set; }

		public bool IsFinished { get; private set; }

		public Easing.Mode Easing { get; private set; }

		public Repeat.Mode Repeat { get; private set; }

		public double Duration { get; private set; }

		public double Time { get; private set; }

		public void Update(double delta)
		{
			if (!this.IsFinished)
			{
				this.Time += delta;

				var amount = (1.0 * this.Time) / this.Duration;
				this.IsFinished = Toody.Repeat.IsFinished(this.Repeat, amount);
				amount = Toody.Repeat.Calculate(this.Repeat, amount);
				amount = Toody.Easing.Calculate(this.Easing, amount);
				this.Value = this.update(amount, this.Start, this.End);
			}
		}
	}
}
