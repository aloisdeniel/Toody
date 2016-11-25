using System;

namespace Toody
{
	public class Sprite : ISprite
	{
		public Sprite(ITexture texture)
		{
			this.Texture = texture;
		}

		public float[] CreateVertices()
		{
			var fmaxW = this.Texture.Width * 1.0f;
			var fmaxH = this.Texture.Height * 1.0f;

			var tl = this.Source.X / fmaxW;
			var tb = this.Source.Y / fmaxH;
			var tr = tl + (this.Source.Width / fmaxW);
			var tt = tb + (this.Source.Height / fmaxH);

			var vl = this.Destination.X;
			var vb = this.Destination.Y;
			var vr = vl + this.Destination.Width;
			var vt = vb + this.Destination.Height;

			return new float[]
			{
				//Pos   //Tex
				vl,vt,  tl,tt,
				vr,vb,  tr,tb,
				vr,vt,  tr,tt,

				vl,vt,  tl,tt,
				vl,vb,  tl,tb,
				vr,vb,  tr,tb,
			};
		}

		public ISpriteAnimation CreateAnimation(float interval, params Rectangle[] frames)
		{
			throw new NotImplementedException();
		}

		private Rectangle? source, destination;

		public ITexture Texture { get; private set; }

		public Rectangle Source 
		{  
			get { return source ?? (source = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height)).Value; } 
			set { this.source = value; } 
		}

		public Rectangle Destination
		{
			get { return destination ?? (destination = this.Source).Value; }
			set { this.destination = value; }
		}
	}
}