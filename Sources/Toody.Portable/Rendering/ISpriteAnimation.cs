namespace Toody
{
	public interface ISpriteAnimation : IUpdatable
	{
		ISprite Sprite { get; }

		float Interval { get; }

		bool IsStarted { get; }

		void Reset();

		void Start(Repeat.Mode repeat);

		void Stop();
	}
}
