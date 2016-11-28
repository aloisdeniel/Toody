﻿namespace Toody
{
	public class Curves
	{
		#region Bezier

		public static Point Bezier(float time, Point start0, Point start1, Point end0, Point end1)
		{
			float u = 1 - time;
			float tt = time * time;
			float uu = u * u;
			float uuu = uu * u;
			float ttt = tt * time;

			var p = uuu * start0;
			p += 3 * uu * time * start1;
			p += 3 * u * tt * end0;
			p += ttt * end1;

			return p;
		}

		#endregion
	}
}