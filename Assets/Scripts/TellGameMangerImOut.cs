using UnityEngine;
using System.Collections;

public class TellGameMangerImOut : MonoBehaviour
{
	bool toldGameManager = false;
	// Use this for initialization
	void Start ()
	{
		
	}

	IEnumerator DelayReport ()
	{
		yield return new WaitForSeconds (0.2f);

		GetComponent<Rigidbody2D> ().isKinematic = true;
		transform.position = new Vector2 (-222.0f, -15f);
		SetChildrenInactive ();
		//GetComponent<Rigidbody2D> ().rotation = 0;
		//gameObject.SetActive (false);
		//Destroy (gameObject);
	}

	void SetChildrenInactive ()
	{
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		foreach (Transform t in transform) {
			t.gameObject.SetActive (false);
		}
	
	}

	void Update ()
	{
		Vector3 viewPos = Camera.main.WorldToViewportPoint (transform.position);
		if (GameManager.instance != null && GameManager.instance.IsReady ()) {
			if (toldGameManager == false) {
				if ((viewPos.x < 0.0f || viewPos.x > 1.0f) || (viewPos.y < 0.0f || viewPos.y > 1.0f)) {
					toldGameManager = true;	
					GameManager.instance.PrettyOut ();
					//	SetChildrenInactive ();
					StartCoroutine ("DelayReport");
				}
			}
		}
	}
}
