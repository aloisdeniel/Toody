namespace Toody
{
	using System;
	using System.Linq;

	public class Sprite : ISprite
	{
		public Sprite(ITexture texture)
		{
			this.color = Color.White;
			this.Texture = texture;
		}

		private float[] vertices;

		public float[] Vertices => vertices ?? (vertices = UpdateVertices());

		private float[] UpdateVertices()
		{
			// Texture

			var fmaxW = this.Texture.Width * 1.0f;
			var fmaxH = this.Texture.Height * 1.0f;

			var t_l = this.Source.X / fmaxW;
			var t_b = this.Source.Y / fmaxH;
			var t_r = t_l + (this.Source.Width / fmaxW);
			var t_t = t_b + (this.Source.Height / fmaxH);

			// Position

			var sw = this.Destination.Width / 2;
			var sh = this.Destination.Height / 2;

			var origin = new Point(this.Destination.X + sw, this.Destination.Y + sh);
			var rot = new Point(sw, sh).Rotate(this.Rotation);

			var v_rt = origin + rot;
			var v_rb = origin + new Point(rot.Y, -rot.X);
			var v_lb = origin - rot;
			var v_lt = origin - new Point(rot.Y, -rot.X);

			return (this.vertices = new float[]
			{
				//Pos           //Tex      // Colors
				v_lt.X, v_lt.Y,  t_l,t_t,  this.Color.Red, this.Color.Green, this.Color.Blue, this.Color.Alpha, // TODO improve repetition of colors with IBO
				v_rb.X, v_rb.Y,  t_r,t_b,  this.Color.Red, this.Color.Green, this.Color.Blue, this.Color.Alpha,
				v_rt.X, v_rt.Y,  t_r,t_t,  this.Color.Red, this.Color.Green, this.Color.Blue, this.Color.Alpha,
								 
				v_lt.X, v_lt.Y,  t_l,t_t,  this.Color.Red, this.Color.Green, this.Color.Blue, this.Color.Alpha,
				v_lb.X, v_lb.Y,  t_l,t_b,  this.Color.Red, this.Color.Green, this.Color.Blue, this.Color.Alpha,
				v_rb.X, v_rb.Y,  t_r,t_b,  this.Color.Red, this.Color.Green, this.Color.Blue, this.Color.Alpha,
			});
		}

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

		private Color color;

		private float rotation;

		private Rectangle? source, destination;

		public ITexture Texture { get; private set; }

		public Rectangle Source 
		{  
			get { return source ?? (source = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height)).Value; } 
			set 
			{
				if (source != value)
				{
					this.source = value;
					this.UpdateVertices();
				}
			} 
		}

		public Rectangle Destination
		{
			get { return destination ?? (destination = this.Source).Value; }
			set 
			{
				if (destination != value)
				{
					this.destination = value;
					this.UpdateVertices();
				}
			}
		}

		public float Rotation
		{
			get { return rotation; }
			set
			{
				if (rotation != value)
				{
					this.rotation = value;
					this.UpdateVertices();
				}
			}
		}

		public Color Color
		{
			get { return color; }
			set
			{
				//if (color != value)
				//{
					this.color = value;
					this.UpdateVertices();
				//}
			}
		}
	}
}