using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Convex_hull;

public class AddP : MonoBehaviour {

	LineRenderer lr;
	public Transform n0;
	public int count = 5;
	public float speed = 6f;
	public Material mm;
	Convex_hull.Convex_hull t = new Convex_hull.Convex_hull();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
		{
			t = new Convex_hull.Convex_hull("tmpPoints.txt");
			for(int i = 0; i < t.Size; ++i)
			{
				float x0 = (float)t.points[i].x - 7.5f;
				float y0 = (float)t.points[i].y - 5f;
				Instantiate(n0, new Vector3(x0 * 10, y0 * 10), Quaternion.identity);
			}
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			int nn = GameObject.Find("Nodes").transform.childCount;
			for (int i = 0; i < nn; ++i)
			{
				Destroy(GameObject.Find("Nodes").transform.GetChild(i));
			}
			List<Point> tmp = new List<Point>();
			for (int i = 0; i < count; ++i)
			{
				float x0 = Random.Range(-7.5f, 7.5f);
				float y0 = Random.Range(-5f, 5f);
				Instantiate(n0, new Vector3(x0 * 10, y0 * 10), Quaternion.identity);
				tmp.Add(new Point(i, x0 + 7.5f, y0 + 5));
			}
			t = new Convex_hull.Convex_hull(count, tmp);
		}
		if (Input.GetKeyDown(KeyCode.V))
		{
			System.DateTime tt = System.DateTime.Now;
			List<List<System.Tuple<Line, int>>> toVis = t.Voronoi();
			for (int pn = 0; pn < toVis.Count; ++pn)
			{
				//for (int i = 0; i < toVis[pn].Count; ++i)
				//{
				//	int j = 0;
				//	for (int jj = 0; jj < pn; ++jj)
				//		j += toVis[jj].Count;
				//	StartCoroutine(toVisSegm(true, j + i, ((float)toVis[pn][i].Item1.a.x - 7.5f) * 10f, ((float)toVis[pn][i].Item1.a.y - 5) * 10f, ((float)toVis[pn][i].Item1.b.x - 7.5f) * 10f, ((float)toVis[pn][i].Item1.b.y - 5) * 10f));
				//}
				StartCoroutine(toVisAll(pn * 5 / (float)count, "n" + (pn + 1).ToString(), toVis[pn]));
			}
			Debug.Log((System.DateTime.Now - tt).ToString());
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			Destroy(lr);
			List<Point> visual = t.Grehem();
			StartCoroutine(toVisHull(0, visual, Color.gray));
		}
		if (Input.GetKeyDown(KeyCode.K))
		{
			Destroy(lr);
			List<Point> toVis = t.Kirkpatrick();
			StartCoroutine(toVisHull(0, toVis, Color.magenta));
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			Destroy(lr);
			List<Line> toVis = new List<Line>();
			List<Point> visual = t.Edwin_Jarvis(toVis);
			StartCoroutine(toVisHull(0, visual, Color.red));
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Destroy(lr);
			List<Line> toVis = new List<Line>();
			List<Point> visual = t.Quickhull(toVis);
			StartCoroutine(toVisHull(0, visual, Color.blue));
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			//Destroy(lr);
			t.max_x += 100;
			t.max_y += 100;
			t.min_x -= 100;
			t.min_y -= 100;
			List<System.Tuple<int, int>> toVis = t.Delaunay();
			for (int i = 0; i < toVis.Count; ++i)
			{
				GameObject p = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
				p.transform.position = new Vector3(0, 0, 100);
				StartCoroutine(toVisSegmN(p, i * 5 / (float)toVis.Count, ((float)t.points[toVis[i].Item1].x - 7.5f) * 10f, ((float)t.points[toVis[i].Item1].y - 5f) * 10f, ((float)t.points[toVis[i].Item2].x - 7.5f) * 10f, ((float)t.points[toVis[i].Item2].y - 5f) * 10f));
			}
		}
	}

	IEnumerator toVisAll(float ss, string name, List<System.Tuple<Line, int>> ll)
	{
		yield return new WaitForSeconds(ss);
		LineRenderer lr0 = GameObject.Find(name).gameObject.AddComponent<LineRenderer>();
		lr0.SetVertexCount(ll.Count + 1);
		lr0.SetWidth(0.5f, 0.5f);
		lr0.material = mm;
		for (int i = 0; i < ll.Count; ++i)
		{
			lr0.SetPosition(i, new Vector3(((float)ll[i].Item1.a.x - 7.5f) * 10f, ((float)ll[i].Item1.a.y - 5f) * 10f, -10f));
		}
		lr0.SetPosition(ll.Count, new Vector3(((float)ll[ll.Count - 1].Item1.b.x - 7.5f) * 10f, ((float)ll[ll.Count - 1].Item1.b.y - 5f) * 10f, -10));
	}

	IEnumerator toVisSegm(bool a, float ss, float x0 = 0, float y0 = 0, float x1 = 0, float y1 = 0)
	{
		yield return new WaitForSeconds(ss);
		if (a)
		{
			lr = gameObject.AddComponent<LineRenderer>();
			lr.SetVertexCount(3);
			lr.SetPosition(0, new Vector3(x0, y0, -10));
			lr.SetPosition(1, new Vector3(x1, y1, -10));
			lr.SetPosition(2, new Vector3(x1 - 1, y1 - 1, -10));
			lr.SetWidth(1f, 1f);
			lr.material = mm;
			StartCoroutine(toVisSegm(false, 0.9f));
		}
		else
		{
			Destroy(lr);
		}
	}

	IEnumerator toVisSegmN(GameObject p, float ss = 0, float x0 = 0, float y0 = 0, float x1 = 0, float y1 = 0)
	{
		yield return new WaitForSeconds(ss);
		LineRenderer ll = p.gameObject.AddComponent<LineRenderer>();
		ll.SetWidth(0.5f, 0.5f);
		ll.material = mm;
		ll.material.SetColor("_EmissionColor", Color.black);
		ll.SetPosition(0, new Vector3(x0, y0, -10));
		ll.SetPosition(1, new Vector3(x1, y1, -10));
	}

	IEnumerator toVisHull(float ss, List<Point> ll, Color cc)
	{
		yield return new WaitForSeconds(ss);
		lr = GameObject.Find(name).gameObject.AddComponent<LineRenderer>();
		lr.SetVertexCount(ll.Count + 1);
		lr.SetWidth(0.5f, 0.5f);
		lr.material = mm;
		lr.material.SetColor("_EmissionColor", cc);
		for (int i = 0; i < ll.Count; ++i)
		{
			lr.SetPosition(i, new Vector3(((float)ll[i].x - 7.5f) * 10f, ((float)ll[i].y - 5f) * 10f, -10f));
		}
		lr.SetPosition(ll.Count, new Vector3(((float)ll[0].x - 7.5f) * 10f, ((float)ll[0].y - 5f) * 10f, -10));
	}

	Vector3 screenPoint;
	void OnMouseOver()
	{
		screenPoint = Camera.allCameras[1].WorldToScreenPoint(transform.position);
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.allCameras[1].ScreenToWorldPoint(cursorPoint);
		if (Input.GetMouseButtonDown(1))
		{
			//Instantiate(n0, cursorPosition, Quaternion.identity);
		}
	}

	void OnMouseClick()
	{
		//for (int i = 0; i < count; ++i)
		//{
		//	Instantiate(n0, new Vector3(Random.Range(-75, 75), Random.Range(-50, 50)), Quaternion.identity);
		//}
	}
}
