using System;
using System.IO;
using MoonSharp.Interpreter;

namespace Toody.Interpreter
{
	public class InterpretedGame : IGame, IDisposable
	{
		private Script script;

		private DynValue load, draw, update;

		private static Type[] DynamicTypes = 
		{ 
			typeof(Camera),
			typeof(Point),
			typeof(Rectangle),
			typeof(Color),
			typeof(IContent),
			typeof(IRenderer),
			typeof(ITexture),
			typeof(ISprite),
			typeof(Tween),
			typeof(Tween<float>),
			typeof(Tween<Color>),
			typeof(Tween<Point>),
			typeof(Tween<Rectangle>),
			typeof(Repeat.Mode),
			typeof(Easing.Mode),
		};

		private static Type[] DynamicExtensions =
		{
			typeof(Extensions),
		};

		private const string MainScript = "Game.lua";

		public InterpretedGame(string scriptFolder)
		{
			foreach (var type in DynamicTypes)
			{
				UserData.RegisterType(type);
			}

			foreach (var type in DynamicExtensions)
			{
				UserData.RegisterExtensionType(type);
			}

			this.script = new Script();

			this.script.Options.ScriptLoader = new ScriptLoader(scriptFolder);

			this.script.Globals["Point"] = typeof(Point);
			this.script.Globals["Tween"] = typeof(Tween);
			this.script.Globals["Repeat"] = typeof(Repeat.Mode);
			this.script.Globals["Easing"] = typeof(Easing.Mode);

			script.DoFile(MainScript);

			this.load = script.Globals.Get(nameof(load));
			this.update = script.Globals.Get(nameof(update));
			this.draw = script.Globals.Get(nameof(draw));
		}

		public void Load(IContent content)
		{
			script.Call(this.load, content);
		}

		public void Update(Camera camera, double delta)
		{
			script.Call(this.update, camera, delta);
		}

		public void Draw(IRenderer renderer)
		{
			script.Call(this.draw, renderer);
		}

		public void Dispose()
		{
			foreach (var type in DynamicTypes)
			{
				UserData.UnregisterType(type);
			}

			this.script = null;
		}
	}
}
