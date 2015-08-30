using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent.Geometry {
	/// <summary>
	/// Provides helper functions for geometry related tasks.
	/// </summary>
	public static class Geometry {

		#region Angle
		/// <summary>
		/// Gets the angle between two points. Numbers are floats.
		/// </summary>
		/// <param name="p1">First point.</param>
		/// <param name="p2">Second point.</param>
		/// <returns>The angle in radians.</returns>
		public static float get2DAngleFloat(Vector2<float> p1, Vector2<float> p2) {
			float xDiff = p2.x - p1.x;
			float yDiff = p2.y - p1.y;
			return (float) Math.Atan2(yDiff, xDiff);
        }
		/// <summary>
		/// Determines whether two float lines are parallel.
		/// </summary>
		/// <param name="l1">The first line.</param>
		/// <param name="l2">The second line.</param>
		/// <returns>True if the lines are parallel.</returns>
		public static bool isParallelFloat(Vector2<Vector2<float>> l1, Vector2<Vector2<float>> l2) {
			return get2DAngleFloat(l1.x, l1.y).Equals(get2DAngleFloat(l2.x, l2.y));
		}
		#endregion

		#region Intersection

		/// <summary>
		/// Determines where and whether a line intersects another.
		/// </summary>
		/// <param name="l1">Line 1</param>
		/// <param name="l2">Line 2</param>
		/// <returns>[0]: intersect x, [1]: intersect y, [2]: t1</returns>
		public static float[] intersectsFloat(Vector2<Vector2<float>> l1, Vector2<Vector2<float>> l2) {

			//if (isParallel(l1, l2)) return null;

			float l1dx = l1.y.x - l1.x.x, l2dx = l2.y.x - l2.x.x;
			float l1dy = l1.y.y - l1.x.y, l2dy = l2.y.y - l2.x.y;


			float t2 = (l1dx * (l2.x.y - l1.x.y) + l1dy * (l1.x.x - l2.x.x)) / (l2dx * l1dy - l2dy * l1dx);
			float t1 = (l2.x.x + l2dx * t2 - l1.x.x) / l1dx;

			if (t1 < 0) { return null; }
			if (t2 < 0 || t2 > 1) { return null; }

			return new float[3] { l1.x.x + (l1dx * t1), l1.x.y + (l1dy * t1), t1 };
		}

		#endregion

	}
}
