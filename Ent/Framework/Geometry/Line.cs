﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soul.Systems;

namespace Soul.Framework {
	public class Line {

		public Point s;
		public Point e;

		public bool horizontal;

		public Line(Point s, Point e, bool horiz = false) {
			this.s = s;
			this.e = e;
			this.horizontal = horiz;
		}

	}
}
