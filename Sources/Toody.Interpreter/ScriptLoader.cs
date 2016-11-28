namespace Toody.Interpreter
{
	using System.IO;
	using MoonSharp.Interpreter;
	using MoonSharp.Interpreter.Loaders;

	public class ScriptLoader : ScriptLoaderBase
	{
		public string ScriptsFolder { get; set; }

		public ScriptLoader(string scriptsFolder)
		{
			this.ScriptsFolder = scriptsFolder;
			this.ModulePaths = new [] { "?.lua" };
		}

		private string GetPath(string script) => Path.Combine(ScriptsFolder, script);

		public override object LoadFile(string file, Table globalContext) => File.ReadAllText(GetPath(file));

		public override bool ScriptFileExists(string name) => File.Exists(GetPath(name));
	}
}
