namespace Toody.iOS
{
	using System;
	using System.IO;
	using CoreAnimation;
	using CoreGraphics;
	using Foundation;
	using UIKit;

	public class Device : IDevice
	{
		internal CAEAGLLayer Layer { get; set; }

		public Rectangle Viewport => new Rectangle(0, 0, (float)(UIScreen.MainScreen.Scale * Layer.Bounds.Size.Width), (float)(UIScreen.MainScreen.Scale * Layer.Bounds.Size.Height));

		const string BundleFolder = "bundle://";
		const string LocalFolder = "local://";

		private string GetPlatformPath(string path)
		{
			if (path.StartsWith(BundleFolder))
			{
				var folder = NSBundle.MainBundle.BundlePath;
				return Path.Combine(folder, path.Replace(BundleFolder, ""));
			}
			else if (path.StartsWith(LocalFolder))
			{
				var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				return Path.Combine(folder, path.Replace(LocalFolder, ""));
			}

			throw new ArgumentException($"The given ressource path should start with either {BundleFolder} or {LocalFolder}");
		}

		public byte[] LoadTexture(string path, out int width, out int height)
		{
			var texData = NSData.FromFile(GetPlatformPath(path));
			var image = UIImage.LoadFromData(texData);

			if (image == null)
				throw new InvalidOperationException($"Can't load texture with path {path}");

			width = (int)image.CGImage.Width;
			height = (int)image.CGImage.Height;

			using (var colorSpace = CGColorSpace.CreateDeviceRGB())
			{
				byte[] imageData = new byte[height * width * 4];
				using (var context = new CGBitmapContext(imageData, width, height, 8, 4 * width, colorSpace, CGBitmapFlags.PremultipliedLast | CGBitmapFlags.ByteOrder32Big))
				{
					context.TranslateCTM(0, height);
					context.ScaleCTM(1, -1);
					context.ClearRect(new CGRect(0, 0, width, height));
					context.DrawImage(new CGRect(0, 0, width, height), image.CGImage);
					return imageData;
				}
			}
		}
	}
}