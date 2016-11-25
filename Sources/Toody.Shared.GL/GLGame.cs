namespace Toody
{
	using System;
	using Toody.Shared.GL;

	public class GLGame : IGame
	{
		public GLGame(IGame portableGame)
		{
			this.portableGame = portableGame;
		}

		#region Fields

		private IContent content;

		private IDevice device;

		private bool isLoaded;

		private Renderer renderer;

		private readonly IGame portableGame;

		#endregion

		#region Properties

		public IDevice Device { get; private set; }

		public IRenderer Renderer => this.renderer;

		#endregion

		public void Load(IContent content)
		{
			if (!isLoaded)
			{
				this.content = content;
				this.device = Content.PlatformCast(this.content).Device;

				this.renderer = content.Get<IRenderer>(null) as Renderer;

				this.portableGame.Load(content);

				content.Load(content);

				this.isLoaded = true;
			}
			else throw new InvalidOperationException("Already loaded");
		}

		public void Draw()
		{
			this.Draw(this.Renderer);
		}

		public void Draw(IRenderer renderer)
		{
			this.Renderer.Begin();
			this.Renderer.Camera.Width = this.device.Viewport.Width;
			this.Renderer.Camera.Height = this.device.Viewport.Height;

			this.portableGame.Draw(renderer);

			this.Renderer.End();
		}

		public void Update(double delta)
		{
			this.Update(this.Renderer.Camera, delta);
		}

		public void Update(Camera camera, double delta)
		{
			this.portableGame.Update(camera, delta);
		}

		public void Dispose()
		{
			if (this.isLoaded)
			{
				this.content.Dispose();

				this.Device = null;
				this.renderer = null;
				this.content = null;
				this.isLoaded = false;
			}
		}
	}
}
