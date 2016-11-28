using System;
using System.IO;
using Foundation;
using Toody.Interpreter;
using Toody.iOS;
using UIKit;

namespace Toody.Sample.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}

		private static string LoadScript(string name)
		{
			var assembly = typeof(SampleGame).Assembly;
			var rname = $"{assembly.GetName().Name}.{name}";
			var stream = assembly.GetManifestResourceStream(rname);
			return new System.IO.StreamReader(stream).ReadToEnd();
		}

		private string CopyScriptsToLocalStorage()
		{
			string[] names = { "Game", "SampleModule" }; 

			var scripts = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			foreach (var name in names)
			{
				var luafile = $"{name}.lua";
				var filename = Path.Combine(scripts, luafile);
				File.WriteAllText(filename, LoadScript(luafile));
			}

			return scripts;
		}

		private string CopyAssetsToLocalStorage()
		{
			string[] names = { "assets.png" };

			var assets = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			foreach (var name in names)
			{
				var folder = NSBundle.MainBundle.BundlePath;
				File.Copy(Path.Combine(folder, name), Path.Combine(assets, name));
			}

			return assets;
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			CopyAssetsToLocalStorage();

			var game = new InterpretedGame(CopyScriptsToLocalStorage()); // new SampleGame()

			this.Window = new UIWindow(UIScreen.MainScreen.Bounds);
			this.Window.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleMargins;
			this.Window.RootViewController = new GameViewController(new GLGame(game));
			this.Window.BackgroundColor = UIColor.White;
			this.Window.MakeKeyAndVisible();

			return true;
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
	}
}

