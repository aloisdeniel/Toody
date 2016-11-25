namespace Toody
{
	using System.Collections.Generic;

	public interface IRenderer : IResource
	{
		Camera Camera { get; }

		void Begin();

		void Draw(IEnumerable<ISprite> sprites);

		void End();
	}
}
