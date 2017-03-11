using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convex_hull
{
	class Program
	{
		static int Main(string[] args)
		{
			Convex_hull t = new Convex_hull("points.txt");
			List<Point> tmp1 = new List<Point>();
			List<Point> tmp2 = new List<Point>();
			List<Point> tmp3 = new List<Point>();
			List<Point> tmp4 = new List<Point>();
			List<Tuple<int, int>> tmp0 =new  List<Tuple<int, int>>();
			List<Line> visual = new List<Line>();
			tmp1 = t.Grehem();
			tmp2 = t.Kirkpatrick();
			tmp3 = t.Edwin_Jarvis(visual);
			tmp4 = t.Quickhull(visual);
			List<List<Tuple<Line, int>>> tmp = new List<List<Tuple<Line, int>>>();
			tmp = t.Voronoi();
			tmp0 = t.Delaunay();
			//Console.ReadKey();
			Tuple<List<Point>, List<Point>> r = t.bucket_sort(t);
			List<Tuple<Line, int>> t4 = new List<Tuple<Line, int>>();
			t4 = t.Voronoi_cell(4,visual);
			//Console.ReadKey();
			return 0;
		}
	}
}
