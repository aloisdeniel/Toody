namespace Toody
{
	public interface ISprite
	{
		ITexture Texture { get; }

		Rectangle Destination { get; set; }

		Rectangle Source { get; set; }

		float Rotation { get; set; }

		Color Color { get; set; }

		float[] Vertices { get; }

		ISpriteAnimation CreateAnimation(float interval, Rectangle startFrame, params Point[] frameIndexes);

		ISpriteAnimation CreateAnimation(float interval, params Rectangle[] frames);
	}
}
