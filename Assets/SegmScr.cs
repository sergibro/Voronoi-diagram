using UnityEngine;
using System.Collections;

public class SegmScr : MonoBehaviour {

	LineRenderer lr;
	float counter, dist;
	//bool cont = true;
	
	public float speed = 6f;
	public Material mm;

	//// Use this for initialization
	//void Start()
	//{
	//	lr = gameObject.AddComponent<LineRenderer>();
	//	cont = true;
	//	lr.SetPosition(0, new Vector3(0, 0, -10));
	//	lr.SetWidth(1f, 1f);
	//	lr.material = mm;
	//	dist = Vector3.Distance(new Vector3(0, 0, -10), new Vector3(20, 20, -10));
	//}

	//// Update is called once per frame
	//void Update()
	//{
	//	if (Input.GetKeyDown(KeyCode.R))
	//	{
	//		Destroy(lr);
	//		cont = false;
	//	}
	//	if (cont && counter < dist)
	//		lr.SetPosition(1, Mathf.Lerp(0, dist, counter += .1f / speed) * Vector3.Normalize(new Vector3(20, 20,- 10) - new Vector3(0, 0, -10)) + new Vector3(0, 0, -10));
	//}
}
