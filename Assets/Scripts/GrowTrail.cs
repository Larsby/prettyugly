using UnityEngine;
using System.Collections;

/*
 * 
 * This is a quick fix for leaving a eating trail for the pretties. 
 *  First of the Trail Renderer have a lot of limitations.
 *  1. The trail needs to be one size, it can't have different widths
 *  2. They must be in a gameObject without another renderer or bad things happen.
 * 
 *  So we want the pretties to leave a trail that is the size of them. So bigger pretty = new TrailObject. 
 *  Also trails leave a gap so we don't nuke the old trailobject directly we wait 2s to fill the gap.
 **/
public class GrowTrail : MonoBehaviour
{
	private TrailRenderer rend;

	public float startWidth;
	public bool spawn = false;
	float scale = 0.0f;
	bool start = false;
	Transform previousPosition;
	bool gotCorrectWidth = false;
	public Transform targetTransform;
	public float latestScale = 0;
	// Use this for initialization
	void Start ()
	{
		rend = gameObject.GetComponent<TrailRenderer> ();
	
		if (rend != null) {
			if (targetTransform != null) {
				scale = targetTransform.localScale.x;
				//	rend.endWidth = rend.startWidth;
				rend.startWidth = startWidth = targetTransform.localScale.x;
			} else {
				rend.startWidth = startWidth;
			}
			rend.endWidth = rend.startWidth;
			rend.sortingOrder = 50;
			rend.sortingLayerName = "Default";
			latestScale = startWidth;
			start = true;
		}

	}

	IEnumerator LateDeletion ()
	{
		yield return new WaitForSeconds (2.0f);
		GetComponent<GrowTrail> ().enabled = false;
		transform.parent = null; 
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (start) {
			if (gotCorrectWidth == false) {
				rend.startWidth = startWidth = targetTransform.localScale.x;
				latestScale = rend.startWidth;
				gotCorrectWidth = true;
				spawn = false;
				return;
			}
			if (gotCorrectWidth) {
				float startw = rend.startWidth;
				float target = targetTransform.localScale.x;
				if (latestScale < target) {
					latestScale = targetTransform.localScale.x;
					spawn = true;
				}
			}
			previousPosition = transform;
		
			if (spawn) {

				GameObject trail = (GameObject)Instantiate (Resources.Load ("TrailObject"));
				trail.transform.parent = transform.parent;
				trail.transform.position = new Vector3 (previousPosition.position.x, previousPosition.position.y, 2.0f);

				trail.transform.localPosition = new Vector3 (previousPosition.localPosition.x, previousPosition.localPosition.y, 2.0f);
				trail.GetComponent<GrowTrail> ().targetTransform = targetTransform;
				//trail.targetTransform = targetTransform;
				StartCoroutine (LateDeletion ());
				spawn = false;
			}

		}

		
	}
}
