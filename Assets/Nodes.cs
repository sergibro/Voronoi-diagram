using UnityEngine;
using System.Collections;

public class Nodes : MonoBehaviour {

	public Transform n0;
	public static int count = 0;
	public int n = 0;
	public Color cur = Color.blue;
	// Use this for initialization
	void Start () {
		n = ++count;
		transform.parent = GameObject.Find("Nodes").transform;
		name = "n" + count.ToString();
		GameObject.Find("N0").GetComponent<TextMesh>().text = "";// count.ToString();
		GameObject.Find("N0").name = "N" + count.ToString();
		GetComponent<Renderer>().material.color = cur;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver()
	{
		cur = GetComponent<Renderer>().material.color == Color.red ? cur : GetComponent<Renderer>().material.color;
		GetComponent<Renderer>().material.color = Color.red;
		if (Input.GetMouseButtonDown(1))
		{
			cur = GetComponent<Renderer>().material.color = (cur == Color.blue ? Color.green : Color.blue);
		}
		//Vector3 pp = GetComponent<LineRenderer>().transform.position;
		//pp.z = -15f;
		//GetComponent<LineRenderer>().transform.position = pp;
		//GetComponent<LineRenderer>().material.SetColor("_EmissionColor", Color.blue);
	}

	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = cur;
		//Vector3 pp = GetComponent<LineRenderer>().transform.position;
		//pp.z = -10f;
		//GetComponent<LineRenderer>().transform.position = pp;
		//GetComponent<LineRenderer>().material.SetColor("_EmissionColor", Color.green);
	}
}
