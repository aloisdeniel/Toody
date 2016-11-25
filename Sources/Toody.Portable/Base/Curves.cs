namespace Toody
{
	public class Curves
	{
		#region Bezier

		public struct BezierNode
		{
			public BezierNode(Point point, Point dir)
			{
				this.Point = point;
				this.Direction = dir;
			}

			public Point Point { get; set; }

			public Point Direction { get; set; }
		}

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

		public static Point Bezier(float time, BezierNode start, BezierNode end) => Bezier(time, start.Point, start.Direction, end.Point, end.Direction);

		#endregion
	}
}