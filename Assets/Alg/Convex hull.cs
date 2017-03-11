using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Convex_hull
{
	class Convex_hull: Line
	{
		public Convex_hull()
		{
			size = 0;
		}
		public int Size
		{
			get { return size; }
		}
		public Convex_hull(int s, List<Tuple<double, double>> list)
		{
			size = s;
			for(int i=0;i< s;i++)
			{
				Point tmp = new Point(i,list[i].Item1,list[i].Item2);
				points.Add(tmp);
			}
		}
		public Convex_hull(int s, List<Point> list)
		{
			size = s;
			for (int i = 0; i < s; i++)
			{
				points.Add(list[i]);
			}
		}
		public Convex_hull(string file)
		{
			string[] tmp = File.ReadAllLines(file);
			size = int.Parse(tmp[0]);
			for (int i = 0; i < size; i++)
			{
				List<double> tmp2 = new List<double>();
				tmp2 = tmp[i + 1].Split(' ').Select(e => double.Parse(e)).ToList();
				Point point = new Point(i,tmp2[0], tmp2[1]);
				points.Add(point);
			}
		}
		public static int cmp(Point a, Point b)//compare
		{
			if ( a.x < b.x || a.x == b.x && a.y < b.y)
				return 1;
			return -1;
		}
		public static bool cw(Point a, Point b, Point c)//clockwise(за годиниковою)
		{
	
			return a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y- b.y) < 0;
		}
		public static bool ccw(Point a, Point b, Point c)//counterclockwise(проти годиниковиї)
		{
			return a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y) > 0;
		}

		public List<Point> Grehem()
		{
			List<Line> list = new List<Line>();
			List<Point> sorted = new List<Point>();
			sorted = points.OrderBy(a => a.y).ToList();
			Point main = sorted[0];
			sorted.Remove(main);
			List<Point> anglesort = new List<Point>();
			anglesort = sorted.OrderBy(a => Math.Acos((a.x - main.x) / Math.Sqrt(((a.x - main.x) * (a.x - main.x) + (a.y - main.y) * (a.y - main.y))))).ToList();
			//anglesort.Add(main);
			/*Point cur = main;
			for (int i = 0; i < anglesort.Count - 1; i++)
			{
				if (anglesort[i].num != cur.num)
				{
					if (((anglesort[i].x - cur.x) * (anglesort[i + 1].y - cur.y) - (anglesort[i].y - cur.y) * (anglesort[i + 1].x - cur.x)) >= 0)
					{
						list.Add(new Line(cur, anglesort[i]));
						cur = anglesort[i];
					}
				}
			}
			list.Add(new Line(list[list.Count - 1].b, main));*/
			List<Point> res = new List<Point>();
			res.Add(main);
			res.Add(anglesort[0]);
			res.Add(anglesort[1]);
			for(int i=2;i< size-1;i++)
			{
				while (res.Count>1 &&  !ccw(res[res.Count - 2], res[res.Count - 1], anglesort[i]) )
					res.RemoveAt(res.Count - 1);
				res.Add(anglesort[i]);
			}





			return res;
		}
		public List<Point> Graham()
		{
			List<Line> visual =new List<Line> ();
			return Graham(visual);
		}
		public List<Point> Kirkpatrick()
		{
			List<Line> vis1 = new List<Line>();
			List<Line> vis2 = new List<Line>();
			List<Line> vis3 = new List<Line>();
			Tuple<List<Line>, List<Line>, List<Line>> visual = new Tuple<List<Line>, List<Line>, List<Line>>(vis1, vis2, vis3);
			return Kirkpatrick(visual);

		}
		public List<Point> Edwin_Jarvis()
		{
			List<Line> visual = new List<Line>();
			return Edwin_Jarvis(visual);
		}
		public List<List<Tuple<Line,int>>> Voronoi()
		{
			List<List<Tuple<Line, int>>> res = new List<List<Tuple<Line, int>>>();
			List<List<Line>> visual = new List<List<Line>>();
			res = Voronoi(visual);
			return res;
		}


		/*	public List<Point> Kirkpatrick()
			{
				List<Point> res = new List<Point>();
				Convex_hull sort = new Convex_hull();
				List<Point> sort_points = new List<Point>();
				sort_points=bucket_sort(this);
				List<Point> left = new List<Point>();
				List<Point> right = new List<Point>();


				left.Add(sort_points[0]);
				for (int i=1;i< size;i++ )
				{
					if ( sort_points[i].y!=sort_points[i-1].y)
					{
						right.Add(sort_points[i - 1]);
						left.Add(sort_points[i]);
					}
				}
				right.Add(sort_points[size - 1]);
				res.Add(left[0]);
				for (int i=1;i<left.Count;i++)
				{
					if (cw(res[res.Count - 1], left[i], left[left.Count - 1]))
						res.Add(left[i]);
				}
				res.Add(left[left.Count - 1]);
				if (right[0] != left[0])
					res.Add(right[0]);
				for (int i = 1; i < right.Count; i++)
				{
					if (ccw(res[res.Count - 1],right[i], right[right.Count - 1]))
						res.Add(right[i]);
				}
				if (right[right.Count - 1] != left[left.Count - 1])
					res.Add(right[right.Count - 1]);



				return res;
			}*/

	



		public List<Point> Graham(List<Line> visual)
		{
			List<Point> res = new List<Point>();

			if (size == 1)
				return res;
			List<Point> sort_points = new List<Point>();
			for(int i=0;i< size;i++)
			{
				sort_points.Add(points[i]);
			}

			sort_points.Sort(cmp);
			Point p1 = sort_points[0], p2 = sort_points[size-1];
			List<Point> up= new List<Point>();
			List<Point> down = new List<Point>();
			up.Add(p1);
			down.Add(p1);
			for (int i = 1; i < size; ++i)
			{
				if (i == size - 1 || cw(p1, sort_points[i], p2))
				{
					while (up.Count >= 2 && !cw(up[up.Count - 2], up[up.Count - 1], sort_points[i]))
					{
						Line add = new Line(up[up.Count - 1], sort_points[i]);
						visual.Add(add);
						Line drop = new Line(up[up.Count-2], up[up.Count - 1],false);
						visual.Add(drop);
						drop =new Line(up[up.Count - 1], sort_points[i],false);
						visual.Add(drop);
						up.RemoveAt(up.Count - 1);
					}
					Line tmp = new Line(up[up.Count - 1], sort_points[i]);
					visual.Add(tmp);
					up.Add(sort_points[i]);
				}
				if (i ==  size - 1 || ccw(p1, sort_points[i], p2))
				{
					while (down.Count >= 2 && !ccw(down[down.Count - 2],down[down.Count - 1], sort_points[i]))
					{
						Line add = new Line(down[down.Count - 1], sort_points[i]);
						visual.Add(add);
						Line drop = new Line(down[down.Count - 2], down[down.Count - 1], false);
						visual.Add(drop);
						drop = new Line(down[down.Count - 1], sort_points[i], false);
						visual.Add(drop);
						down.RemoveAt(down.Count - 1);
					}
					Line tmp = new Line(down[down.Count - 1], sort_points[i]);
					visual.Add(tmp);
					down.Add(sort_points[i]);
				}
			}
			sort_points.Clear();
			for (int i = 0; i < up.Count; ++i)
			{
				
				res.Add(up[i]);
			}
			for (int i = down.Count-2; i > 0; --i)
			{
				res.Add(down[i]);
			}
			return res;
		}

		public List<Point> Kirkpatrick(Tuple<List<Line>,List<Line>,List<Line>> visual)
		{
			Tuple<List<Point>, List < Point >>tmp = bucket_sort(this);
			Convex_hull l = new Convex_hull(tmp.Item1.Count, tmp.Item1);
			Convex_hull r = new Convex_hull(tmp.Item2.Count, tmp.Item2);
			List<Line> vis1 = new List<Line>();
			List<Point> left = l.Quickhull(vis1);
			List<Line> vis2 = new List<Line>();
			List<Point> right = r.Quickhull(vis2);
			left.AddRange(right);
			Convex_hull r_and_l = new Convex_hull(left.Count, left);
			List<Line> vis3 = new List<Line>();
			List<Point> res = r_and_l.Quickhull(vis3);
			return res;
		}
		public List<Point> Edwin_Jarvis(List<Line> visual)
		{
			List<Point> res = new List<Point>();
			List<Point> copy_points = new List<Point>();
			copy_points.AddRange(points);
			copy_points.Sort(cmp_x);
			Point leftmost = copy_points[0];
			res.Add(leftmost);
			Point rightmost = copy_points[copy_points.Count - 1];
			List<Point> higher = new List<Point>();
			List<Point> below = new List<Point>();
			below.Add(leftmost);
			//higher.Add(rightmost);
			Line basic = new Line(leftmost, rightmost);
			for (int i=0;i< size;i++)
			{
				double tmp = basic.sign_line(copy_points[i]);
				if (tmp > eps)
					higher.Add(copy_points[i]);
				if (tmp < -eps)
					below.Add(copy_points[i]);
			}
			//below.Add(leftmost);
			higher.Add(rightmost);
			Point last = leftmost;
			for (int i=0; i<higher.Count;i++)
			{
				Line check_line = new Line(last, higher[i]);
				bool check = true;
				for (int j= i;j< higher.Count;j++)
				{
					if (check_line.sign_line(higher[j])>eps)
					{
						check = false;
						break;
					}
				}
				if (check)
				{
					res.Add(higher[i]);
					Line tmp = new Line(last, higher[i]);
					visual.Add(tmp);
					last = higher[i];
				}
			}
			res.Add(rightmost);
			Line tm = new Line(last, rightmost);
			visual.Add(tm);
			last = rightmost;
			for (int i = below.Count-1; i >-1; i--)
			{
				Line check_line = new Line(last, below[i]);
				bool check = true;
				for (int j = i; j >-1; j--)
				{
					if (check_line.sign_line(below[j]) < -eps)
					{
						check = false;
						break;
					}
				}
				if (check)
				{
					res.Add(below[i]);
					Line tmp = new Line(last, below[i]);
					visual.Add(tmp);
					last = below[i];
				}
			}
			Line t = new Line(last, leftmost);
			visual.Add(t);


			return res;

		}
		public List<Point> Quickhull(List<Line> visual)
		{
			List<Point> res = new List<Point>();
			List<Point> copy_points = new List<Point>();
			copy_points.AddRange(points);
			copy_points.Sort(cmp_x);
			Point leftmost = copy_points[0];
			res.Add(leftmost);
			Point rightmost = copy_points[copy_points.Count - 1];
			Line basic = new Line(leftmost, rightmost);
			List<Point> s1 = new List<Point>();
			List<Point> s2 = new List<Point>();
			visual.Add(basic);
			for(int i=0;i< size; i++)
			{
				if (basic.sign_line(copy_points[i]) > eps)
					s1.Add(copy_points[i]);
				if (basic.sign_line(copy_points[i]) < -eps)
					s2.Add(copy_points[i]);
			}

			List<Point> res1 = Qhull(leftmost,rightmost, s1);
			res.AddRange(res1);
			res.Add(rightmost);
			List<Point> res2 = Qhull( rightmost,leftmost, s2);
			res.AddRange(res2);


			return res;
		}
		public List<Point> Qhull(Point A,Point B, List<Point> s)
		{
			List<Point> res = new List<Point>();
			if (s.Count == 0)
				return res;
			Line basic = new Line(A, B);
			double max = 0;
			int index_max = -1;
			for (int i = 0; i < s.Count; i++)
			{
				if (Math.Abs(basic.sign_line(s[i])) > max)
				{
					index_max = i;
					max = Math.Abs(basic.sign_line(s[i]));
				}
			}
			Point C = new Point (s[index_max]);
			List<Point> s1 = new List<Point>();
			List<Point> s2 = new List<Point>();
			if (A.x < B.x)
			{
				Line A_C = new Line(A, C);
				Line C_B = new Line(C, B);
				for (int i = 0; i < s.Count; i++)
				{
					if (A_C.sign_line(s[i]) > eps )
					{
						s1.Add(s[i]);
					}
					if (C_B.sign_line(s[i]) > eps)
					{
						s2.Add(s[i]);
					}
				}
			}
			else
			{
				Line A_C = new Line(A, C);
				Line C_B = new Line(C, B);
				for (int i = 0; i < s.Count; i++)
				{
					if (A_C.sign_line(s[i]) < -eps)
					{
						s1.Add(s[i]);
					}
					if (C_B.sign_line(s[i]) < -eps)
					{
						s2.Add(s[i]);
					}
				}
			}
			List<Point> res1 = new List<Point>();
			res1 = Qhull(A, C, s1);
			res.AddRange(res1);
			res.Add(C);
			List<Point> res2 = new List<Point>();
			res2 = Qhull(C,B, s2);
			res.AddRange(res2);


			return res;
		}


		public List<List<Tuple<Line, int>>> Voronoi(List<List<Line>> visual)
		{
			List<List<Tuple<Line, int>>> res = new List<List<Tuple<Line, int>>>();
			for (int i =0; i< size;i++)
			{
				List<Line> vis_i = new List<Line>();
				res.Add(Voronoi_cell(i, vis_i));
				visual.Add(vis_i);
			}
			return res;
		}
		public List<Tuple<Line, int>> Voronoi_cell(int i, List<Line> vis_i)
		{
		    List<Tuple<Line, int>> res = new List<Tuple<Line, int>>();
			List<Tuple<Line, int>> hull = new List<Tuple<Line, int>>();
			Point A = new Point(min_x, min_y);
			Point B = new Point(min_x, max_y);
			Point C = new Point(max_x, max_y);
			Point D = new Point(max_x, min_y);
			Tuple<Line, int> tmp = new Tuple<Line, int>(new Line(A, B), -1);
			hull.Add(tmp);
			tmp = new Tuple<Line, int>(new Line(B, C),-1);
			hull.Add(tmp);
			tmp = new Tuple<Line, int>(new Line(C,D), -1);
			hull.Add(tmp);
			tmp = new Tuple<Line, int>(new Line(D,A), -1);
			hull.Add(tmp);
			for (int j=0; j< size;j++)
			{
				if ( j != i)
				{
					Line1 basic = Line1.perp_bis_cons(points[i], points[j]);
					int drop_s=-1;
					int  drop_f=-1;
					bool type=false;// type=0 if from 0 to drop_s and from drop_s to hull.Count 
					bool across = false;
					Point new1 = new Point();
					Point new2 = new Point();
					for (int t=0;t<hull.Count;t++)
					{
						if (!hull[t].Item1.is_parallel(basic))
						{
							Point meet = hull[t].Item1.piercing_lines(basic);
							if (hull[t].Item1.in_edge(meet))
							{

								if (drop_s == -1)
								{
									if (basic.sign_line(points[i]) * (basic.sign_line(hull[t].Item1.a)) > 0)
									{
										drop_s = t;
										type = false;
									}
									else
									{
										drop_s = t;
										type = true;
									}
									new1 = meet;
								}
								else
								{
									if (basic.sign_line(points[i]) * (basic.sign_line(hull[t].Item1.a)) > 0)
									{
										drop_f =t;
										//type = false;
									}
									else
									{
										drop_f =t;
										//type = true;
									}
									new2 = meet;
									across = true;
									break;
								}
							}
						}

					}
					if (across)
					if (type )
					{
						List<Tuple<Line, int>> copy_hull = new List<Tuple<Line, int>>();
						Tuple<Line, int> tmpq = new Tuple<Line, int>(new Line(new1, hull[drop_s].Item1.b), hull[drop_s].Item2);
						copy_hull.Add(tmpq);
						for (int k = drop_s + 1; k < drop_f; k++)
							copy_hull.Add(hull[k]);
						tmpq = new Tuple<Line, int>(new Line(hull[drop_f].Item1.a,new2), hull[drop_f].Item2);
						copy_hull.Add(tmpq);
						tmpq = new Tuple<Line, int>(new Line(new2, new1), j);
						copy_hull.Add(tmpq);
						hull = new List<Tuple<Line, int>>(copy_hull);
					}
					else
					{
						List<Tuple<Line, int>> copy_hull = new List<Tuple<Line, int>>();
						for (int k = 0; k < drop_s; k++)
							copy_hull.Add(hull[k]);
						Tuple<Line, int>  tmpq = new Tuple<Line, int>(new Line(hull[drop_s].Item1.a,new1), hull[drop_s].Item2);
						copy_hull.Add(tmpq);
						tmpq = new Tuple<Line, int>(new Line(new1, new2), j);
						copy_hull.Add(tmpq);
						tmpq = new Tuple<Line, int>(new Line(new2, hull[drop_f].Item1.b), hull[drop_f].Item2);
						copy_hull.Add(tmpq);
						for (int k = drop_f+1; k < hull.Count; k++)
							copy_hull.Add(hull[k]);
						hull = new List<Tuple<Line, int>>(copy_hull);
					}

				}
			}

			res = hull;



			return res;
		}
		public List<Tuple<int,int>> Delaunay()
		{
			List<Tuple<int, int>> res = new List<Tuple<int, int>>();

			List<List<Tuple<Line, int>>> vor = new List<List<Tuple<Line, int>>>();
			vor = Voronoi();
			List<List<int>> mtrx = new List<List<int>>(size);
			for (int i = 0; i < size; i++)
			{
				List<int> tmp = new List<int>(Enumerable.Repeat(0, size));
				mtrx.Add(tmp);
			}
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < vor[i].Count; j++)
				{
					if (vor[i][j].Item2 !=-1)
					mtrx[i][vor[i][j].Item2] = 1;
				}
			}
			List<Point> hull = new List<Point>();
			List<Line> visual =  new List<Line>();
			hull = Grehem();
			for (int i = 0; i < hull.Count-1; i++)
			{
				mtrx[hull[i].num][hull[i + 1].num] = 1;
				mtrx[hull[i+1].num][hull[i].num] = 1;
			}
			mtrx[hull[0].num][hull[hull.Count - 1].num] = 1;
			mtrx[hull[hull.Count - 1].num][hull[0].num] = 1;

			for (int i = 0; i < size-1; i++)
				for (int j = i+1; j < size; j++)
					if (mtrx[i][j]==1)
					{
						res.Add(new Tuple<int, int>(i, j));
					}
			return res;
		}




		private void domain()
		{
			max_x = points[0].x;
			max_y = points[0].y;
			min_x = points[0].x;
			min_y = points[0].y;
			for (int i=1;i< size;i++)
			{
				if (max_x < points[i].x)
					max_x = points[i].x;
				if (max_y < points[i].y)
					max_y = points[i].y;
				if (min_x > points[i].x)
					min_x = points[i].x;
				if (min_y > points[i].y)
					min_y = points[i].y;
			}
		}
		/*public List<Point> bucket_sort(Convex_hull arr)
		{
			arr.domain();
			List<Point> res = new List<Point>();
			List<List<Point>> bucket = new List<List<Point>>();
			for(int i=0;i < arr.size; i++)
			{
				List<Point> tmp = new List<Point>();
				bucket.Add(tmp);
			}
			for (int i = 0; i < arr.size; i++)
				bucket[arr.msbits(arr.points[i].y, size)].Add(arr.points[i]);
			for (int i = 0; i < size; i++)
				bucket[i].Sort(cmp_y);
			for (int i = 0; i < size; i++)
				res.AddRange(bucket[i]);
			return res;
		}*/
		public Tuple<List<Point>,List<Point>> bucket_sort(Convex_hull arr)
		{
			arr.domain();
			List<Point> left = new List<Point>();
			List<Point> right = new List<Point>();
			List<List<Point>> bucket = new List<List<Point>>();
			for (int i = 0; i < Convert.ToInt32(max_y) - Convert.ToInt32(min_y)+1; i++)
			{
				List<Point> tmp = new List<Point>();
				bucket.Add(tmp);
			}
			for (int i = 0; i < arr.size; i++)
				bucket[arr.msbits(arr.points[i].y)].Add(arr.points[i]);
			for (int i = 0; i < Convert.ToInt32(max_y) - Convert.ToInt32(min_y) + 1; i++)
			{
				bucket[i].Sort(cmp_y);
				if (bucket[i].Count > 0)
				{
					left.Add(bucket[i][0]);
					right.Add(bucket[i][bucket[i].Count - 1]);
				}
			}
			Tuple<List<Point>, List<Point>> res = new Tuple<List<Point>, List<Point>>(left, right);
			return res;
		}
		private int cmp_y(Point a,Point b)
		{
			if (a.y < b.y)
				return -1;
			if (a.y > b.y)
				return 1;
			if (a.x < b.x)
				return -1;
			if (a.x > b.x)
				return 1;
			return 0;
		}
		private int cmp_x(Point a, Point b)
		{
			if (a.x < b.x)
				return -1;
			if (a.x > b.x)
				return 1;
			if (a.y < b.y)
				return -1;
			if (a.y > b.y)
				return 1;
			return 0;
		}
		private int msbits(double k)
		{
			return Convert.ToInt32(k) - Convert.ToInt32(min_y);
		}


		public List<Point> points = new List<Point>();
		private int size;
		public double max_x = 15;
		public double max_y = 10;
		public double min_x = 0;
		public double min_y = 0;
		private static double eps = 0.000000001;
	}
}
