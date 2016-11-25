namespace Toody.Droid
{
	using Android.Graphics;
	using Java.Nio;

	public class Device : IDevice
	{
		public Rectangle Viewport { get; internal set; }

		public void Load(Texture texture)
		{
			var b = BitmapFactory.DecodeFile(texture.Filename);
			var buffer = ByteBuffer.Allocate(b.ByteCount);
			b.CopyPixelsToBuffer(buffer);
			b.Recycle();
			var data = buffer.ToArray<byte>();
			texture.Load(data, b.Width, b.Height);
		}
	}
}
