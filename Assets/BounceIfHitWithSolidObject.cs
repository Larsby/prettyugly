using UnityEngine;
using System.Collections;

public class BounceIfHitWithSolidObject : MonoBehaviour
{
	Rigidbody2D body;
	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody2D> ();
	}



	void OnCollisionStay2D (Collision2D col)
	{
		// add check for triggers so we only trigger on enemy
		// probably use the coliders contact points too

		if (col.gameObject.CompareTag ("Obstacle")) {
			body.velocity = new Vector2 (body.velocity.x * -1, body.velocity.y * -1);
			Vector2 forceVec = body.velocity * 0.14f;

			Debug.Log ("ENTER THE DRAGON. Collision with obstacel! Force" + forceVec.x + " " + forceVec.y);
			body.AddForce (forceVec, ForceMode2D.Impulse);
		}
	
	}

}