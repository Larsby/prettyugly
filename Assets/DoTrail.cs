using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTrail : MonoBehaviour {
	private Vector3 currentPosition;
	int index = 0;
	LineRenderer ren;
	Fatness fat;
	// Use this for initialization
	void Start () {
		ren = GetComponent<LineRenderer> ();
		fat = GetComponent<Fatness> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance(transform.position, new Vector3 (currentPosition.x, currentPosition.y, transform.position.z) ) > 0.25f) {
			if(index >1) {
				ren.positionCount = index+1;
			}
			ren.SetPosition (index, currentPosition);
			ren.startWidth = fat.scale - 0.1f;
			ren.startWidth = fat.scale + 0.1f;
			index++;
			currentPosition = transform.position;
		}
}
}
