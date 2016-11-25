namespace Toody
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using OpenTK.Graphics.ES30;

	public class Renderer : IRenderer
	{
		public Renderer()
		{
		}

		private int vbo;

		public ShaderProgram Program { get; private set; }

		public Camera Camera { get; private set; } = new Camera();

		public void Load(IContent content)
		{
			this.Program = content.Get<ShaderProgram>(null);

			GL.ClearDepth(1.0f);
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
			GL.CullFace(CullFaceMode.Back);

			GL.GenBuffers(1, out this.vbo);
		}

		public void Dispose()
		{
			this.Program = null;

			GL.DeleteBuffers(1, ref this.vbo);
		}

		public void Draw(IEnumerable<ISprite> sprites)
		{

			var byTexture = sprites.GroupBy(s => s.Texture);

			foreach (var spriteGroup in byTexture)
			{
				var vertices = spriteGroup.SelectMany(s => s.CreateVertices()).ToArray();

				// Select texture
				GL.ActiveTexture(TextureUnit.Texture0); // TODO improve by sending more than one texture unit and merge sprites vertices
				GL.BindTexture(TextureTarget.Texture2D, spriteGroup.Key.ID); 

				// Start (binding buffer)
				GL.BindBuffer(BufferTarget.ArrayBuffer, this.vbo);

				// Sending vertices data
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), vertices, BufferUsage.DynamicDraw);

				// Setting up attribute pointers
				GL.VertexAttribPointer(ShaderProgram.AttributePosition, 4, VertexAttribPointerType.Float, false, sizeof(float) * 4, IntPtr.Zero);
				GL.EnableVertexAttribArray(ShaderProgram.AttributePosition);

				// Send vertices to shaders and draw
				GL.DrawArrays(BeginMode.TriangleFan,0, vertices.Length);

				// End (unbind buffer)
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			}

			// End (Unbind texture)
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public void Begin()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.ClearColor(0.6f, 0.6f, 0.65f, 1);

			GL.UseProgram(this.Program.ID);

			this.Program.Update(this.Camera);
		}

		public void End()
		{
			//throw new NotImplementedException();
		}
	}
}
