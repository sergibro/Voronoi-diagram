using UnityEngine;
using System.Collections;

public class StartOp3d : MonoBehaviour {

	public Transform point;
	const float c = 0.09f;
	const int n = 20;

	//Vector3 p0 = new Vector3(0, 0, 0);
	//float pl0 = 0.1f;

	// Use this for initialization
	void Start () {
		//for (; pl0 < 3f; pl0 += 0.5f)
		//{
		//	float x0 = (pl0 + p0.x) / 2;
		//	Instantiate(point, new Vector3(p0.x, 0, 0), Quaternion.identity);
		//	Transform pp =  Instantiate(point, new Vector3(pl0, 0, 0), Quaternion.identity) as Transform;
		//	pp.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
		//	for (int i = 0; i < 20; ++i)
		//	{
		//		float x = x0 - i * c, y = p0.y + Mathf.Sqrt((pl0 - x) * (pl0 - x) - (x - p0.x) * (x - p0.x));
		//		Transform p = Instantiate(point, new Vector3(x, y, 0), Quaternion.identity) as Transform;
		//		p.transform.parent = GameObject.Find("Un").transform;
		//		p.name = "p" + i.ToString();
		//		Transform p2 = Instantiate(point, new Vector3(x, -y, 0), Quaternion.identity) as Transform;
		//		p2.transform.parent = GameObject.Find("Un").transform;
		//		p2.name = "p2" + i.ToString();
		//	}
		//}

		for (int i = -n; i < n + 1; ++i)
		{
			for (int j = -n; j < n + 1; ++j)
			{
				float z = i * c * i * c + j * c * j * c - 1, x = i * c, y = j * c;
				Transform p = Instantiate(point, new Vector3(-z + 500f, x, y), Quaternion.identity) as Transform;
				p.transform.parent = GameObject.Find("Un").transform;
				p.name = "p" + ((i + n) * 100 + j + n).ToString();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
