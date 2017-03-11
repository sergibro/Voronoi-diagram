using UnityEngine;
using System.Collections;

public class lrs : MonoBehaviour {

	LineRenderer lr;
	float counter, dist;

	public Transform fr, to;
	public float speed = 6f;

	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer>();
		lr.SetPosition(0, fr.position);
		lr.SetWidth(.1f, .1f);
		dist = Vector3.Distance(fr.position, to.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (counter < dist)
		{
			counter += .1f / speed;
			float x = Mathf.Lerp(0, dist, counter);
			lr.SetPosition(1, x * Vector3.Normalize(to.position - fr. position) + fr.position);
		}
	}
}
