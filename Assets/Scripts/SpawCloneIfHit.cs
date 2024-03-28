using UnityEngine;
using System.Collections;

public class SpawCloneIfHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		GameObject clone = Instantiate (col.gameObject);
		clone.transform.position = col.gameObject.transform.position;
		clone.transform.rotation = col.gameObject.transform.rotation;
		clone.transform.localPosition = col.gameObject.transform.localPosition;
		Rigidbody2D body = col.gameObject.GetComponent<Rigidbody2D> ();
	
		if (body) {
			Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D> ();
			cloneBody.velocity = body.velocity;

		}
		Collider2D collider = gameObject.GetComponent<Collider2D> ();
		collider.enabled = false;
		gameObject.SetActive (false);
	}

}
