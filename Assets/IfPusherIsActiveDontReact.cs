using UnityEngine;
using System.Collections;

public class IfPusherIsActiveDontReact : MonoBehaviour
{

	Collider2D coll;
	// Use this for initialization
	void Start ()
	{
		coll = GetComponent<Collider2D> ();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		// add check for triggers so we only trigger on enemy
		// probably use the coliders contact points too

		if (col.gameObject.CompareTag ("Blob")) {
			if (GameManager.instance.IsPlayerAiming ()) {
				if (coll != null) {
					coll.isTrigger = true;
				}
			} 
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Blob")) {
			if (coll != null) {
				if (!GameManager.instance.IsPlayerAiming())
				{
					coll.isTrigger = false;
				}
			}
		}
	}

	void OnCollisionExit2D (Collision2D col)
	{
		// add check for triggers so we only trigger on enemy
		// probably use the coliders contact points too

		if (col.gameObject.CompareTag ("Blob")) {
			if (coll != null) {
				if (GameManager.instance == null) return;
				if (!GameManager.instance.IsPlayerAiming())
				{
					coll.isTrigger = false;
				}
			}
		}
	}
	private void Update()
	{
		if (!GameManager.instance)
		   return;
		if (!GameManager.instance.IsPlayerAiming()) {
			coll.isTrigger = false;
		}
	}

}
