namespace Toody
{
	public interface IGame : ILoadable
	{
		void Draw(IRenderer renderer);

		void Update(Camera camera, double delta);
	}
}
