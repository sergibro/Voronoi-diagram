using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convex_hull
{
	class Line1
	{
		public double k;
		public double c;
		public double type;
		public Line1()
		{
			k = 0;
			c = 0;
			type = 1;
		}
		public Line1(double a,double b)
		{
			k = a;
			c = b;
			type = 1;
		}
		public Line1(Point a,Point b)
		{
			if (b.x != a.x)
			{
				k = (b.y - a.y) / (b.x - a.x);
				c = (b.y * a.x - a.y * b.x) / (a.x - b.x);
			}
			else
			{
				k = 1;
				type = 0;
				c = -b.x;
			}
		}
		public Line1(Line l)
		{
			k = l.K;
			c = l.C;
			type = l.type;
		}
		public static Line1 perp_bis_cons(Point a, Point b)
		{
			Line1 res = new Line1();
			if ((b.y-a.y)!=0)
			{
				res.k = -(b.x - a.x) / (b.y - a.y);
				res.c = (b.y * b.y + b.x * b.x - a.y * a.y - a.x * a.x) / (2 * (b.y - a.y));
				res.type = 1;
			}
			else
			{
				res.k = 0;
				res.c = -(a.x+b.x)/2;
				res.type = 0;
			}
			
			return res;
		}
		public double sign_line(double x, double y)
		{
			if (type==1)
			return (y - k * x - c);
			return -(x + c);
		}
		public double sign_line(Point other)
		{
			if (type == 1)
				return (other.y - k * other.x - c);
			return -(other.x + c);
		}


	}
}
