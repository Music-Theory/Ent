using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent.Geometry {
	/// <summary>
	/// A vector with four dimensions.
	/// </summary>
	/// <typeparam name="T">The type of the dimensions. It would make the most sense to make this something that involves numbers.</typeparam>
	class Vector4<T> : Vector3<T> {

		/// <summary>
		/// A dimension of the vector.
		/// </summary>
		T a;

		public Vector4(T x, T y, T z, T a) : base(x, y, z) {
			this.a = a;
		}

	}
}
