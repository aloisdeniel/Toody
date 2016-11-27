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

		public float Red { get; set; } // TODO clamp

		public float Green { get; set; } // TODO clamp

		public float Blue { get; set; } // TODO clamp

		public float Alpha { get; set; } // TODO clamp

		#region Operators

		public static Color operator +(Color value1, Color value2)
		{
			value1.Red += value2.Red;
			value1.Green += value2.Green;
			value1.Blue += value2.Blue;
			value1.Alpha += value2.Alpha;
			return value1;
		}

		public static Color operator -(Color value1, Color value2)
		{
			value1.Red -= value2.Red;
			value1.Green -= value2.Green;
			value1.Blue -= value2.Blue;
			value1.Alpha -= value2.Alpha;
			return value1;
		}

		public static Color operator *(Color value1, Color value2)
		{
			value1.Red *= value2.Red;
			value1.Green *= value2.Green;
			value1.Blue *= value2.Blue;
			value1.Alpha *= value2.Alpha;
			return value1;
		}

		public static Color operator *(Color value1, float amount)
		{
			value1.Red *= amount;
			value1.Green *= amount;
			value1.Blue *= amount;
			value1.Alpha *= amount;
			return value1;
		}

		public static explicit operator Color(uint hex) => new Color(hex);

		#endregion
	}
}
