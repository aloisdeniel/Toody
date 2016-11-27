namespace Toody
{
	public interface ISprite
	{
		ITexture Texture { get; }

		Point Destination { get; set; }

		Rectangle Source { get; set; }

		Point Origin { get; set; }

		float Scale { get; set; }

		float Rotation { get; set; }

		Color Color { get; set; }

		float[] Vertices { get; }

		ISpriteAnimation CreateAnimation(float interval, Rectangle startFrame, params Point[] frameIndexes);

		ISpriteAnimation CreateAnimation(float interval, params Rectangle[] frames);
	}
}
