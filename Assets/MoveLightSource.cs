using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;

	

public class MoveLightSource : MonoBehaviour {
	public GameObject player;
	private PlayerController pc;
	float turnSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		pc = player.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (pc.isAiming) {
			//Vector3 targetPosition = Camera.main.ScreenToWorldPoint (player.transform.position);
			//transform.rotation = Quaternion.LookRotation (Vector3.back, player.transform.position - transform.position);
			//float horiz = Input.GetAxis("Horizontal");
			float vert = Input.GetAxis("Vertical");
			//Vector3.MoveTowards(
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint (player.transform.position);
			transform.rotation = Quaternion.LookRotation (Vector3.forward, player.transform.position - transform.position);
			//this.transform.Rotate(Vector3.forward, vert * Time.deltaTime * turnSpeed);
			// Set the position of your left mutation
		//	leftSide.transform.position = body.transform.position - vector ;;
		} else {
		//	transform.position = player.transform.position;
		//	transform.localPosition = player.transform.localPosition;
		}
	}
}
