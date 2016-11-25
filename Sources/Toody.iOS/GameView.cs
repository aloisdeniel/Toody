namespace Toody.iOS
{
	using System;
	using CoreAnimation;
	using CoreGraphics;
	using Foundation;
	using ObjCRuntime;
	using OpenGLES;
	using OpenTK;
	using OpenTK.Graphics.ES30;
	using OpenTK.Platform.iPhoneOS;
	using Toody.Shared.GL;
	using UIKit;

	[Register ("GameView")]
	public class GameView : iPhoneOSGameView
	{
		[Export("initWithCoder:")]
		public GameView(NSCoder coder) : base (coder)
		{
			this.Inititialize();
		}

		public GameView(GLGame game, CGRect frame) : base(frame)
		{
			this.Game = game;
			this.Inititialize();
		}

		private void Inititialize()
		{
			LayerRetainsBacking = true;
			LayerColorFormat = EAGLColorFormat.RGBA8;
			ContentScaleFactor = UIScreen.MainScreen.Scale; // Retina
		}

		Content content;

		private Device device;

		public GLGame Game { get; set; }

		[Export("layerClass")]
		public static new Class GetLayerClass() => iPhoneOSGameView.GetLayerClass();

		protected override void ConfigureLayer(CAEAGLLayer eaglLayer) => eaglLayer.Opaque = true;

		protected override void CreateFrameBuffer()
		{
			var eaglLayer = (CAEAGLLayer)Layer;
			this.device = new Device()
			{
				Layer = eaglLayer
			};

			this.content = new Content(this.device);

			var size = new CGSize(
				(int)Math.Round(this.device.Viewport.Width),
				(int)Math.Round(this.device.Viewport.Height));

			try
			{
				ContextRenderingApi = EAGLRenderingAPI.OpenGLES3;

				base.CreateFrameBuffer();

				this.Game.Load(content);

				int depthRenderbuffer;
				GL.GenRenderbuffers(1, out depthRenderbuffer);
				GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, depthRenderbuffer);
				GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferInternalFormat.DepthComponent16, (int)size.Width, (int)size.Height);
				GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferSlot.DepthAttachment, RenderbufferTarget.Renderbuffer, depthRenderbuffer);
			}
			catch (Exception e)
			{
				throw new Exception("Failed to load", e);
			}
		}

		protected override void DestroyFrameBuffer()
		{
			base.DestroyFrameBuffer();
			this.Game.Dispose();
		}

		#region DisplayLink support


		int frameInterval;
		CADisplayLink displayLink;

		public bool IsAnimating { get; private set; }

		public int FrameInterval
		{
			get
			{
				return frameInterval;
			}
			set
			{
				if (value <= 0)
					throw new ArgumentException();
				frameInterval = value;
				if (IsAnimating)
				{
					StopAnimating();
					StartAnimating();
				}
			}
		}
		public void StartAnimating()
		{
			if (IsAnimating)
				return;

			CreateFrameBuffer ();
			displayLink = UIScreen.MainScreen.CreateDisplayLink(this, new Selector("drawFrame"));
			displayLink.FrameInterval = frameInterval;
			displayLink.AddToRunLoop(NSRunLoop.Current, NSRunLoop.NSDefaultRunLoopMode);
			IsAnimating = true;
		}

		public void StopAnimating()
		{
			if (!IsAnimating)
				return;

			displayLink.Invalidate();
			displayLink = null;
			DestroyFrameBuffer ();
			IsAnimating = false;
		}

		#endregion

		#region Rendering

		[Export("drawFrame")]
		public void DrawFrame() => OnRenderFrame(new FrameEventArgs(displayLink.Duration));

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			this.MakeCurrent();
			this.Game.Update(e.Time);
			this.Game.Draw();
			this.SwapBuffers();
		}

		#endregion

		#region Touches

		bool touchesMoved;
		bool touchesActive;

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			touchesMoved = false;
			touchesActive = true;
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			touchesMoved = true;

			var touch = touches.AnyObject as UITouch;
			var xdiff = touch.PreviousLocationInView(this).X - touch.LocationInView(this).X;
			var ydiff = touch.PreviousLocationInView(this).Y - touch.LocationInView(this).Y;
			//cube.Move((float)xdiff, (float)ydiff);
			MakeCurrent();

			//cube.Render();
			SwapBuffers();
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			//if (!touchesMoved) cube.ToggleTexture();
			touchesActive = false;
		}

		#endregion
	}
}
