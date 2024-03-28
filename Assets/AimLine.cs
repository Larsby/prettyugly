using UnityEngine;
using System.Collections;

public class AimLine : MonoBehaviour
{

	// Use this for initialization
	float ypos = 0;
	public bool willHitObstacle = false;
	GameObject target = null;

	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("PusherPointer");
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Obstacle") == true) {
			willHitObstacle = true;
		}
	}


	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Obstacle") == true) {
			willHitObstacle = true;
		}
	}


	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Obstacle") == true) {
			willHitObstacle = false;
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("PusherPointer");
		}
		if (target != null) {
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint (target.transform.position);
			transform.rotation = Quaternion.LookRotation (Vector3.forward, target.transform.position - transform.position);
			//transform.position = target.transform.position;

		}
	}

}