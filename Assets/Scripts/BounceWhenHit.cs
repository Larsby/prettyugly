using UnityEngine;
using System.Collections;

public class BounceWhenHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void turnOnCollision() {
		Collider2D collider = gameObject.GetComponent<Collider2D> ();
	//	collider.enabled = true;
	
	}
	void puchBack() {
		float val = 1.0f;
		iTween.ScaleTo(gameObject,iTween.Hash("x",val,"y",val,"oncomplete","turnOnCollision","time",0.5f));
	}
	void OnCollisionEnter2D(Collision2D col) {
		
		Rigidbody2D body = col.gameObject.GetComponent<Rigidbody2D> ();
		Vector3 velocity = col.relativeVelocity;
		float scale = Mathf.Abs(velocity.x);
	//	scale = scale * 0.010f;
		Collider2D collider = gameObject.GetComponent<Collider2D> ();
	//	collider.enabled = false;
		float val = scale;
		if (scale > 1.0f) {
			val = scale * 0.1f;
		}
		val = 1.0f - val;

		//val = 0.5f;
		velocity = new Vector3 (0.5f, 0.5f,0f);
		//iTween.PunchScale(gameObject,iTween.Hash("x",scale,"y",scale,"oncomplete","turnOnCollision","time",0.5f));
		iTween.ScaleTo(gameObject,iTween.Hash("x",val,"y",val,"oncomplete","puchBack","time",0.5f));

	}

}
