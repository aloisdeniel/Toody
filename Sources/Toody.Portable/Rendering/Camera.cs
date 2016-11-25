namespace Toody
{
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
	}
}
