using System;

namespace Toody
{
	public class Sprite : ISprite
	{
		internal Sprite(ITexture texture)
		{
			this.Texture = texture;
		}

		public float[] CreateVertices()
		{
			var fmaxW = this.Texture.Width * 1.0f;
			var fmaxH = this.Texture.Height * 1.0f;

			var tl = this.Source.X / fmaxW;
			var tt = this.Source.Y / fmaxH;
			var tr = tl + (this.Source.Width / fmaxW);
			var tb = tt + (this.Source.Height / fmaxH);

			var vl = this.Destination.X;
			var vt = this.Destination.Y;
			var vr = vl + this.Destination.Width;
			var vb = vt + this.Destination.Height;

			return new float[]
			{
				//Pos   //Tex
				vl,vt,  tl,tt,
				vr,vt,  tr,tt,
				vr,vb,  tr,tb,
				vl,vb,  tl,tb,
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