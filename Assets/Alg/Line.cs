using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convex_hull
{
	class Line
	{
		public Point a = new Point();
		public Point b = new Point();
		protected bool visible;
		public double K
		{
			get {
				if (type==1)
				   return (b.y - a.y) / (b.x - a.x);
				return 1;
			}
		}
		public double C
		{
			get{
				if (type == 1)
					return (b.y * a.x - a.y * b.x) / (a.x - b.x);
				return -a.x;
					}
		}
		public double type
		{
			get
			{
				if (b.x != a.x)
					return 1;
				return 0;
			}
		}


		public double sign_line(double x,double y)
		{
			if (type == 1)
				return (y - K * x - C);
			return -(x + C);
			
		}
		public double sign_line(Point other )
		{
			if (type == 1)
				return (other.y - K * other.x - C);
			return -(other.x + C);
		}

		public Line()
		{
			visible = false;
		}
		public Line(Point x,Point y)
		{
			a = new Point(x);
			b = new Point(y);
			visible = true;
		}
		public Line(Point x, Point y,bool t)
		{
			a = new Point(x);
			b = new Point(y);
			visible = t;
		}
		public Line(Line l, bool t=true)
		{
			a = l.a;
			b = l.b;
			visible = t;
		}
		public bool in_edge(Point c)
		{
			if (sign_line(c) > eps || sign_line(c) < -eps)
				return false;
			if (type == 1)
			{
				if ((c.x - a.x) * (c.x - b.x) > 0)
					return false;
			}
		    else
				if ((c.y - a.y) * (c.y - b.y) > 0)
				   return false;

			return true;
		}
		public bool is_parallel(Line1 l)
		{
			if (type == 1 && l.type==1)
				return Math.Abs(K - l.k) < eps;
			return (l.type==0 && type==0);

		}
		public Point piercing_lines(Line1 l)
		{
			Point res = new Point();
			if (type ==1 && l.type==1)
			{
				res.x = (l.c - C) / (K - l.k);
				res.y = (K * l.c - C * l.k) / (K - l.k);
			}
			if (type == 1 && l.type == 0)
			{
				res.x = -l.c;
				res.y = -K*l.c+C;
			}
			if (type == 0 && l.type == 1)
			{
				res.x = -C;
				res.y = -l.k*C+l.c;
			}
			return res;
		}
		private static double eps = 0.000000001;


	}
}
