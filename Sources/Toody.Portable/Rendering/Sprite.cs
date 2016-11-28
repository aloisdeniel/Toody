namespace Toody
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class Sprite : ISprite
	{
		public Sprite(ITexture texture)
		{
			this.color = Color.White;
			this.scale = 1.0f;
			this.Texture = texture;
		}

		#region Vertices

		private bool needsVerticesUpdate = true;

		private float[] vertices;

		public float[] Vertices
		{
			get
			{
				if (needsVerticesUpdate)
				{
					this.vertices = CreateVertices();
					this.needsVerticesUpdate = false;
				}

				return this.vertices;
			}
		}

		private float[] CreateVertices()
		{
			// Texture

			var fmaxW = this.Texture.Width * 1.0f;
			var fmaxH = this.Texture.Height * 1.0f;

			var t_l = this.Source.X / fmaxW;
			var t_b = this.Source.Y / fmaxH;
			var t_r = t_l + (this.Source.Width / fmaxW);
			var t_t = t_b + (this.Source.Height / fmaxH);

			// Position
			var origi = Origin * this.Scale;
			var center = this.Destination - (this.Source.Size / 2);
			var rot = (this.Source.Size / 2).Rotate(this.Rotation) * this.Scale;

			var v_rt = origi + center + rot;
			var v_rb = origi + center + new Point(rot.Y, -rot.X);
			var v_lb = origi + center - rot;
			var v_lt = origi + center - new Point(rot.Y, -rot.X);

			return (this.vertices = new float[]
			{
				//Pos           //Tex      // Colors
				v_lt.X, v_lt.Y,  t_l,t_t,  this.Color.R, this.Color.G, this.Color.B, this.Color.A, // TODO improve repetition of colors with IBO
				v_rb.X, v_rb.Y,  t_r,t_b,  this.Color.R, this.Color.G, this.Color.B, this.Color.A,
				v_rt.X, v_rt.Y,  t_r,t_t,  this.Color.R, this.Color.G, this.Color.B, this.Color.A,
								 					 			   			   				 
				v_lt.X, v_lt.Y,  t_l,t_t,  this.Color.R, this.Color.G, this.Color.B, this.Color.A,
				v_lb.X, v_lb.Y,  t_l,t_b,  this.Color.R, this.Color.G, this.Color.B, this.Color.A,
				v_rb.X, v_rb.Y,  t_r,t_b,  this.Color.R, this.Color.G, this.Color.B, this.Color.A,
			});
		}

		private void SetAndUpdateVertices<T>(ref T field, T value)
		{
			if (!EqualityComparer<T>.Default.Equals(field, value))
			{
				field = value;
				this.needsVerticesUpdate = true;
			}
		}

		#endregion

		#region Animations

		public ISpriteAnimation CreateAnimation(float interval, params Rectangle[] frames)
		{
			throw new NotImplementedException();
		}

		public ISpriteAnimation CreateAnimation(float interval, Rectangle startFrame, params Point[] frameIndexes)
		{
			var allFrameIndexes = new[] { Point.Zero }.Concat(frameIndexes);
			var allFrames = allFrameIndexes.Select(p => new Rectangle(startFrame.X + p.X * startFrame.Width, startFrame.Y + p.Y * startFrame.Height, startFrame.Width, startFrame.Height));
			return this.CreateAnimation(interval,allFrames.ToArray());
		}

		#endregion

		private Color color;

		private float rotation, scale;

		private Point destination;

		private Point? origin;

		private Rectangle? source;

		public ITexture Texture { get; private set; }

		public Rectangle Source 
		{  
			get { return source ?? (source = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height)).Value; } 
			set { SetAndUpdateVertices(ref source, value); }
		}

		public Point Destination
		{
			get { return destination; }
			set { SetAndUpdateVertices(ref destination, value); }
		}

		public Point Origin
		{
			get { return origin ?? (origin = new Point(this.Source.Width / 2, this.Source.Height / 2)).Value; }
			set { SetAndUpdateVertices(ref origin, value); }
		}

		public float Rotation
		{
			get { return rotation; }
			set { SetAndUpdateVertices(ref rotation, value); }
		}

		public float Scale
		{
			get { return scale; }
			set { SetAndUpdateVertices(ref scale, value); }
		}

		public Color Color
		{
			get { return color; }
			set
			{
				//if (color != value)
				//{
					this.color = value;
					this.CreateVertices();
				//}
			}
		}
	}
}