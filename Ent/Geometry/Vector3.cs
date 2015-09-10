namespace Ent.Geometry {
	/// <summary>
	/// A vector with three dimensions.
	/// </summary>
	/// <typeparam name="T">The type of the dimensions. It would make the most sense to make this something that involves numbers.</typeparam>
	public class Vector3<T> : Vector2<T> {

		/// <summary>
		/// A dimension of the vector.
		/// </summary>
		public T z;

		public Vector3(T x, T y, T z) : base(x, y) {
			this.z = z;
		}

	}
}
