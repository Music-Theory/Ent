using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent.Geometry {
	public class Point {

		public double x, y;
		public double angle;

		public Point(double x, double y) {
			this.x = x;
			this.y = y;
		}

		// override object.Equals
		public override bool Equals(object obj) {
			//       
			// See the full list of guidelines at
			//   http://go.microsoft.com/fwlink/?LinkID=85237  
			// and also the guidance for operator== at
			//   http://go.microsoft.com/fwlink/?LinkId=85238
			//

			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}

			Point comp = obj as Point;

			return x == comp.x && y == comp.y;
		}

		// override object.GetHashCode
		public override int GetHashCode() {
			// TODO: write your implementation of GetHashCode() here
			//throw new NotImplementedException();
			return base.GetHashCode();
		}

	}
}
