namespace Ent.Geometry {

	/// <summary>
	///     A four-dimensional representation of rotation. The first three dimensions are the axis of rotation and the fourth
	///     is the rotation around that axis.
	/// </summary>
	public class Quaternion {

		/// <summary>
		///     The dimensions of the quaternion.
		/// </summary>
		public Vector4<float> dims;

		/// <summary>
		///     Constructor.
		/// </summary>
		/// <param name="dims">Dimensions.</param>
		public Quaternion(Vector4<float> dims) { this.dims = dims; }

		/// <summary>
		///     Quaternion multiplication. THIS IS NOT COMMUTATIVE.
		/// </summary>
		/// <param name="q1">The amount that q2 will be rotated.</param>
		/// <param name="q2">The quaternion that will be modified by q1.</param>
		/// <returns>The product of a quaternion multiplication operation.</returns>
		public static Quaternion operator *(Quaternion q1, Quaternion q2) {
			Vector4<float> newDims = new Vector4<float>(
				( q1.dims.a * q2.dims.x ) + ( q1.dims.x * q2.dims.a ) + ( q1.dims.y * q2.dims.z ) - ( q1.dims.z * q2.dims.y ), // X
				( q1.dims.a * q2.dims.y ) - ( q1.dims.x * q2.dims.z ) + ( q1.dims.y * q2.dims.a ) + ( q1.dims.z * q2.dims.x ), // Y
				( q1.dims.a * q2.dims.z ) + ( q1.dims.x * q2.dims.y ) - ( q1.dims.y * q1.dims.x ) + ( q1.dims.z * q2.dims.a ), // Z
				( q1.dims.a * q2.dims.a ) - ( q1.dims.x * q2.dims.x ) - ( q1.dims.y * q2.dims.y ) - ( q1.dims.z * q2.dims.z )  // W
				);
			return new Quaternion(newDims);
		}

	}

}