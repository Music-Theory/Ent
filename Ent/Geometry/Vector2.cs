using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent.Geometry {
	/// <summary>
	/// A vector with two dimensions.
	/// </summary>
	/// <typeparam name="T">The type of the dimensions. It would make the most sense to make this something that involves numbers.</typeparam>
	public class Vector2<T> {

		/// <summary>
		/// A dimension of the vector.
		/// </summary>
		public T x, y;

		public Vector2(T x, T y) {
			this.x = x;
			this.y = y;
		}

	}
}
