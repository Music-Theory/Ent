using Ent.Geometry;

namespace Ent.UI {
	/// <summary>
	/// A camera through which the game can be rendered.
	/// </summary>
	class Camera {

		/// <summary>
		/// The camera's location in space.
		/// </summary>
		public Vector3<float> loc;
		/// <summary>
		/// The rotation of the camera. A quaternion.
		/// </summary>
		public Quaternion rot;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="loc">The location of the camera.</param>
		/// <param name="rot">The rotation of the camera.</param>
		public Camera(Vector3<float> loc, Quaternion rot) {
			this.loc = loc;
			this.rot = rot;
		}

	}
}
