namespace Toody
{
	public interface ISprite
	{
		ITexture Texture { get;}

		Rectangle Destination { get; set; }

		Rectangle Source { get; set; }

		float[] CreateVertices();

		ISpriteAnimation CreateAnimation(float interval, params Rectangle[] frames);
	}
}
