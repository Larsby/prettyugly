using UnityEngine;
using System.Collections;

public class RotateInPusherDirection : MonoBehaviour
{
	
	private bool look = true;
	private GameObject target;
	private Rigidbody2D body;
	// Use this for initialization
	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Blob");
		body = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update ()
	{
		look = true;
		if (body.velocity.x != 0 || body.velocity.y != 0) {
			look = false;
		}
		
		if (look) {

			transform.rotation = Quaternion.LookRotation (Vector3.forward, target.transform.position - transform.position);
		}
	
	}
}
