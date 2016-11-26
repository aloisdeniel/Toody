namespace Toody
{
	public struct Color
	{
		#region Constants

		public static Color White = new Color(1, 1, 1, 1);

		#endregion

		public Color(uint hex)
		{
			this.Alpha = (byte)(hex >> 24) / 255.0f;
			this.Red = (byte)(hex >> 16) / 255.0f;
			this.Green = (byte)(hex >> 8) / 255.0f;
			this.Blue = (byte)(hex >> 0) / 255.0f;
		}

		public Color(float r, float g, float b, float a)
		{
			this.Red = r;
			this.Green = g;
			this.Blue = b;
			this.Alpha = a;
		}

		public float Red { get; set; }

		public float Green { get; set; }

		public float Blue { get; set; }

		public float Alpha { get; set; }

		public static explicit operator Color(uint hex) => new Color(hex);
	}
}
