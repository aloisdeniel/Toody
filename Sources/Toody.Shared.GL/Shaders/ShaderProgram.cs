namespace Toody
{
	using System;
	using System.Diagnostics;
	using System.Text;
	using System.Reflection;
	using OpenTK;
	using OpenTK.Graphics.ES30;
	using Toody.Shared.GL;

	public class ShaderProgram : IResource
	{
		public ShaderProgram()
		{
		}

		private IDevice device;

		public const int AttributePosition = 0;

		public const int AttributeTextureCoordinates = 1;

		public int ID { get; private set; }

		public int UniformProjection { get; private set; }

		#region Loading

		public void Load(IContent content)
		{
			var glContent = Content.PlatformCast(content);
			this.device = glContent.Device;

			var vs = DefaultVertexShader;
			var fs = DefaultFragmentShader;

			vs.Load(content);
			fs.Load(content);

			this.ID = GL.CreateProgram();
			if (this.ID == 0)
				throw new InvalidOperationException("Unable to create program");

			GL.AttachShader(this.ID, vs.ID);
			GL.AttachShader(this.ID, fs.ID);

			GL.BindAttribLocation(this.ID, AttributePosition, "i_position");

			GL.LinkProgram(this.ID);

			int linked;
			GL.GetProgram(this.ID, ProgramParameter.LinkStatus, out linked);
			if (linked == 0)
			{
				int length = 0;
				GL.GetProgram(this.ID, ProgramParameter.InfoLogLength, out length);
				if (length > 0)
				{
					var log = new StringBuilder(length);
					GL.GetProgramInfoLog(this.ID, length, out length, log);
					Debug.WriteLine("Couldn't link program: " + log.ToString());
				}

				GL.DeleteProgram(this.ID);
				throw new InvalidOperationException("Unable to link program");
			}

			this.UniformProjection = GL.GetUniformLocation(this.ID, "u_projection");

			GL.UseProgram(this.ID);
		}

		public void Dispose()
		{
			GL.DeleteProgram(this.ID);
			DefaultVertexShader.Dispose();
			DefaultFragmentShader.Dispose();
		}

		#endregion

		#region Projection

		private Matrix4 projection = new Matrix4();

		public void Update(Camera camera)
		{
			var density = (float)UIKit.UIScreen.MainScreen.Scale;
			var width = this.device.Viewport.Width / density;
			var height = this.device.Viewport.Height / density;
			projection = Matrix4.CreateOrthographic(width , height, 0, 100);
			//projection = Matrix4.Scale(camera.Zoom, camera.Zoom, 1) * projection;
			//projection = Matrix4.CreateTranslation(-camera.Position.X, -camera.Position.Y, 0) * projection;
			//projection = Matrix4.CreateRotationZ(camera.Rotation) * projection;

			GL.UniformMatrix4(this.UniformProjection, false, ref projection);
		}

		#endregion

		#region Default shaders

		private static string LoadResource(string name)
		{
			var assembly = typeof(ShaderProgram).GetTypeInfo().Assembly;
			var rname = $"{assembly.GetName().Name}.{name}";
			var stream = assembly.GetManifestResourceStream(rname);
			return new System.IO.StreamReader(stream).ReadToEnd();
		}

		private static Lazy<Shader> defaultVertexShader = new Lazy<Shader>(() => new Shader(LoadResource("Shaders.Default.vs"), ShaderType.VertexShader));

		private static Lazy<Shader> defaultFragmentShader = new Lazy<Shader>(() => new Shader(LoadResource("Shaders.Default.fs"), ShaderType.FragmentShader));

		public static Shader DefaultFragmentShader => defaultFragmentShader.Value;

		public static Shader DefaultVertexShader => defaultVertexShader.Value;

		#endregion
	}
}
