using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineToTarget : MonoBehaviour {

	public GameObject target;
	private LineRenderer draw;
	public int segements = 22;

	// Use this for initialization
	void Start () {
		draw = GetComponent<LineRenderer> ();
		draw.positionCount = segements+1;

		DrawIt ();
	}

	void DrawIt() {
		Vector3 pointA = transform.position;
		Vector3 pointB = target.transform.position;

		Vector3 current = pointA;
		//draw.SetPosition (0, pointA);
		Vector3 step = pointB - pointA;
		step = new Vector3 (step.x /segements  , step.y/segements, 0.0f);
		Vector3 iterator = pointA-step;
		for (int i = 0; i < segements; i++) {
			

			iterator = iterator + step;
			draw.SetPosition (i, iterator);

		
		}
		draw.SetPosition (segements, pointB);
		//Loop through all line segments (except the last because it's always connected to something)

	}
	// Update is called once per frame
	void Update () {
		DrawIt ();
	}
}
