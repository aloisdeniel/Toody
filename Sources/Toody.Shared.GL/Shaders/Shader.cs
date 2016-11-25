namespace Toody
{
	using System;
	using System.Text;
	using OpenTK.Graphics.ES30;

	public class Shader : IResource
	{
		public Shader(string program, ShaderType type)
		{
			this.program = program;
			this.type = type;
		}

		private readonly string program;

		private readonly ShaderType type;

		public int ID { get; private set; }

		public void Load(IContent content)
		{
			this.ID = GL.CreateShader(this.type);

			GL.ShaderSource(this.ID, this.program);
			GL.CompileShader(this.ID);

			int compiled = 0;

			GL.GetShader(this.ID, ShaderParameter.CompileStatus, out compiled);

			if (compiled == 0)
			{
				var length = 0;
				string message = null;

				GL.GetShader(this.ID, ShaderParameter.InfoLogLength, out length);

				if (length > 0)
				{
					var log = new StringBuilder(length);
					GL.GetShaderInfoLog(this.ID, length, out length, log);
					message = log.ToString();
				}

				GL.DeleteShader(this.ID);

				throw new InvalidOperationException("Unable to compile shader : " + message);
			}
		}

		public void Dispose()
		{
			GL.DeleteShader(this.ID);
		}
	}
}
