namespace Toody.Interpreter
{
	public static class Extensions
	{
		#region Generics

		public static ITexture GetTexture(this IContent content, string path) => content.Get<ITexture>(path);

		#endregion
	}
}
