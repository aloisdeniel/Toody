namespace Toody.iOS
{
	using System;
	using Foundation;
	using UIKit;

	public class GameViewController : UIViewController
	{
		public GameViewController(GLGame game)
		{
			this.Game = game;
		}

		public GameViewController(IntPtr handle) : base (handle)
		{
		}

		public GLGame Game { get; set; }

		public GameView GameView => (GameView)this.View;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.View = new GameView(Game, this.View.Frame);
			this.View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleMargins;

			NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.WillResignActiveNotification, a =>
			{
				if (IsViewLoaded && GameView.Window != null)
					GameView.StopAnimating();
			}, this);
			NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidBecomeActiveNotification, a =>
			{
				if (IsViewLoaded && GameView.Window != null)
					GameView.StartAnimating();
			}, this);
			NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.WillTerminateNotification, a =>
			{
				if (IsViewLoaded && GameView.Window != null)
					GameView.StopAnimating();
			}, this);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			NSNotificationCenter.DefaultCenter.RemoveObserver(this);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			GameView.StartAnimating();
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			GameView.StopAnimating();
		}
	}
}
