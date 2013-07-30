using System;

namespace TaoOpenGL
{
	public class Shape
	{
		public double x, y, z;
		public int shape;
		public double radius;
		public int color;

		public Shape (double x, double y, double z, int shape, double radius, int color)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.shape = shape;
			this.radius = radius;
			this.color = color;
		}
	}
}

