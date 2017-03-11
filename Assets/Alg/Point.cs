using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convex_hull
{
	class Point
	{
		public static int count=0;
		public int num;
		public double x;
		public double y;
		public Point()
		{
			num = -1;
			x = -1;
			y = -1;
			count++;
		}
		public Point(int n,double a,double b)
		{
			num = n;
			x = a;
			y = b;
			count++;
		}
		public Point( double a, double b)
		{

			num = count;
			x = a;
			y = b;
			count++;
		}
		public Point(Point other)
		{
			x = other.x;
			y = other.y;
			num = other.num;
		}

	}
}
