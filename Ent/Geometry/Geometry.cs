using System;
using System.Diagnostics.CodeAnalysis;

namespace Ent.Geometry {

	/// <summary>
	///     Provides helper functions for geometry related tasks.
	/// </summary>
	public static class Geometry {
		#region Intersection

		/// <summary>
		///     Determines where and whether a line intersects another.
		/// </summary>
		/// <param name="l1">Line 1</param>
		/// <param name="l2">Line 2</param>
		/// <returns>[0]: intersect x, [1]: intersect y, [2]: t1</returns>
		[SuppressMessage("ReSharper", "InconsistentNaming")]
		public static float[] intersectsFloat(Vector2<Vector2<float>> l1, Vector2<Vector2<float>> l2) {
			//if (isParallel(l1, l2)) return null;

			float l1dx = l1.y.x - l1.x.x, l2dx = l2.y.x - l2.x.x;
			float l1dy = l1.y.y - l1.x.y, l2dy = l2.y.y - l2.x.y;


			float t2 = ( l1dx * ( l2.x.y - l1.x.y ) + l1dy * ( l1.x.x - l2.x.x ) ) / ( l2dx * l1dy - l2dy * l1dx );
			float t1 = ( l2.x.x + l2dx * t2 - l1.x.x ) / l1dx;

			if (t1 < 0) {
				return null;
			}
			if (t2 < 0 || t2 > 1) {
				return null;
			}

			return new[] {l1.x.x + ( l1dx * t1 ), l1.x.y + ( l1dy * t1 ), t1};
		}

		#endregion

		#region Quaternion Operations

		// Or, "How Do I Rotate"

		// Multiplication is in the quaternion class because it overloads the * operator for it.

		/// <summary>
		///     Makes a quaternion to be used to modify another one.
		/// </summary>
		/// <param name="axis">The axis of rotation of this quaternion.</param>
		/// <param name="angle">The angle of this quaternion.</param>
		/// <returns>The modifier quaternion.</returns>
		public static Quaternion GenerateRotationMod(Vector3<float> axis, float angle) {
			Vector4<float> mod = new Vector4<float>(
				(float) Math.Cos(angle / 2),
				(float) ( axis.x * Math.Sin(angle / 2) ),
				(float) ( axis.y * Math.Sin(angle / 2) ),
				(float) ( axis.z * Math.Sin(angle / 2) )
				);
			return new Quaternion(mod);
		}

		#endregion

		#region Angle

		/// <summary>
		///     Gets the angle between two points.
		/// </summary>
		/// <param name="p1">First point.</param>
		/// <param name="p2">Second point.</param>
		/// <returns>The angle in radians.</returns>
		public static float Get2DAngle(Vector2<float> p1, Vector2<float> p2) {
			float xDiff = p2.x - p1.x;
			float yDiff = p2.y - p1.y;
			return (float) Math.Atan2(yDiff, xDiff);
		}

		/// <summary>
		///     Determines whether two float lines are parallel.
		/// </summary>
		/// <param name="l1">The first line.</param>
		/// <param name="l2">The second line.</param>
		/// <returns>True if the lines are parallel.</returns>
		public static bool IsParallelFloat(Vector2<Vector2<float>> l1, Vector2<Vector2<float>> l2) { return Get2DAngle(l1.x, l1.y).Equals(Get2DAngle(l2.x, l2.y)); }

		#endregion
	}

}