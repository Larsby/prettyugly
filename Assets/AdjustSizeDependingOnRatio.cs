using UnityEngine;
using System.Collections;

public class AdjustSizeDependingOnRatio : MonoBehaviour
{
	bool recalc = false;
	bool init;
	public bool takeScreenshot = true;
	public bool usePusherAsStartingPoint = false;
	// Use this for initialization
	private Transform pusherTransform = null;

	void Start ()
	{
		init = false;	
		if (usePusherAsStartingPoint) {
			pusherTransform = GameObject.FindGameObjectWithTag ("Blob").transform;
			Camera.main.transform.parent = pusherTransform;
		}
	}

	void TakeScreenShot ()
	{
		if (takeScreenshot) {
			gameObject.GetComponent<TakeScreenShot> ().capture = true;
			takeScreenshot = false;
		}
	}

	void Update ()
	{
		Vector3 viewPos = Camera.main.WorldToViewportPoint (transform.position);

		if ((viewPos.x < 0.0f || viewPos.x > 0.995f) || (viewPos.y < 0.05f || viewPos.y > 0.995f)) {
			Camera.main.orthographicSize += 0.1f;
			recalc = true;
			init = true;
		
		} else {
			if (recalc) {
				recalc = false;
			
				MessureArea.instance.Calculate ();
				init = true;
				GameManager.instance.ActivatePrettyOutDetection ();
				GameManager.instance.AspectSet ();
				TakeScreenShot ();
				Camera.main.transform.parent = null;
			} 	
		} 
		if (init == false) {
			GameManager.instance.ActivatePrettyOutDetection ();
			GameManager.instance.AspectSet ();
			TakeScreenShot ();
		}
	}

}
