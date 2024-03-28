using UnityEngine;
using System.Collections;

public class PlacePusherLegs : MonoBehaviour
{
	
	Vector3 center;
	public float radius = 0.4f;
	public bool legsDone = false;

	void Rotate (Transform child, int index)
	{
		//if (index ==1 ) {
		child.eulerAngles = new Vector3 (0, 0, (360 / 8) * index);
		//	}
	}

	Vector3 getPoint (Vector3 origin, float angle, float rad)
	{
		float a = angle * Mathf.PI / 180.0f;
		return new Vector3 (origin.x + rad * Mathf.Cos (a), origin.y + rad * Mathf.Sin (a), 0.0f);

	}

	void doneLeg ()
	{
		legsDone = true;
	}

	private void  doIt (Vector3 center, Transform child, int i, bool rotate, bool animate)
	{
		float degrees = 45.0f;
		Vector3 v = center;
		float angle = 360.0f - (degrees * i);
		if (animate) {
//			iTween.MoveTo (child.gameObject, getPoint (center, angle, radius), 0.5f);
			//		iTween.MoveTo (child.gameObject, iTween.Hash ("position", getPoint (center, angle, radius), "time", 0.2f, "oncomplete", "doneLeg", "islocal", true));
			child.transform.position = getPoint (center, angle, radius);
			legsDone = true;
		} else {
			child.transform.position = getPoint (center, angle, radius);
		}
		if (rotate) {
			child.eulerAngles = new Vector3 (0, 0, 270 - (45 * i));
		}


	
	}


	void Place (bool rotate, bool animate)
	{
		int i = 0;
		int j = 8;

		Vector3 center = gameObject.GetComponent<Collider2D> ().bounds.center;
		legsDone = false;
		foreach (Transform child in transform) {
			
			if (child.CompareTag ("PusherLeg")) {
				child.name = "PusherLeg" + i;
				//	child.gameObject.GetComponent<ResetPosition> ().Reset (gameObject.transform);
				doIt (center, child, i, rotate, animate);
				i++;
			}

		}
	
	}

	public	void Place ()
	{

		Place (true, true);
	
	}
		
	// Use this for initialization
	void Start ()
	{
		

		Place (true, false);	
	}
	

}
