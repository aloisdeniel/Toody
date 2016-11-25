namespace Toody
{
	public class Touch
	{
		public Touch(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public int ID { get; private set; }

		public InputState State { get; private set; }
	}
}
