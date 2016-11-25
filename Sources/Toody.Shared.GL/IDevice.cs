namespace Toody
{
	public interface IDevice
	{
		Rectangle Viewport { get; }

		byte[] LoadTexture(string path, out int width, out int height);
	}
}
