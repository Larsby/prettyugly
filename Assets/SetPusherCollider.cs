using UnityEngine;
using System.Collections;

public class SetPusherCollider : MonoBehaviour
{
	Collider2D coll;
	// Use this for initialization
	void Start ()
	{
		coll = GetComponent<Collider2D> ();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Blob")) {

			col.isTrigger = false;
			//col.GetComponent<PlayerController> ().DisableLegs (); 
			//	col.GetComponent<PusherDrag> ().DisableLegs ();
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Blob")) {
			col.isTrigger = false;

		}
	}


}
