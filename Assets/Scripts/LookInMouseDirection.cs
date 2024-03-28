using UnityEngine;
using System.Collections;

public class LookInMouseDirection : MonoBehaviour {
	private bool look = true;
	// Use this for initialization
	void Start () {

	}
	void OnMouseDown()
	{
		look = false;
	}	
	void OnMouseUp() {
		look = true;
	}
	// Update is called once per frame
	void Update () {
		if (look) {
			if (!Input.GetMouseButtonUp(0)) {
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePosition - transform.position);
			}
		}
	}
}
