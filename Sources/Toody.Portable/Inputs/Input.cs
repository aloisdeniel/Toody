namespace Toody
{
	using System.Collections.Generic;

	public class Input 
	{
		public Input()
		{
		}

		public IEnumerable<Touch> Touches { get; private set; }
	}
}
