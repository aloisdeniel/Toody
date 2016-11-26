namespace Toody
{
	using System;

	public class Camera
	{
		public Camera()
		{
			this.Zoom = 1;
			this.Rotation = 0;
		}

		public Point Position { get; set; }

		public float Zoom { get; set; }

		public float Rotation { get; set; }

		public float Width { get; set; }

		public float Height { get; set; }

		public Matrix CreateTransform()
		{
			return Matrix.Rotation(this.Rotation).Multiply(Matrix.Scale(this.Zoom, this.Zoom));
		}

		public Point ToScreen(float x, float y)
		{
			throw new NotImplementedException();
		}

		public Point ToScreen(Point worldPosition)
		{
			throw new NotImplementedException();
		}

		public Point ToWorld(float x, float y)
		{
			throw new NotImplementedException();
		}

		public Point ToWorld(Point screenPosition)
		{
			throw new NotImplementedException();
		}
	}
}
