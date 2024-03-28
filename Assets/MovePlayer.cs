using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

	public bool move = false;
	private Rigidbody2D body;
	 Vector3 gameObjectSreenPoint;
	 Vector3 mousePreviousLocation;
	 Vector3 mouseCurLocation;
	 Vector3 force;
	 Vector3 objectCurrentPosition;
	 Vector3 objectTargetPosition;
	 float topSpeed = 1;
	private bool updateVelocity = true;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
			move = false;
			//body.AddForce (new Vector2(10, 2.0f));
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		move = false;
		updateVelocity = false;
	}

void OnMouseDown()
{
		force = Vector3.zero;
			

	//This grabs the position of the object in the world and turns it into the position on the screen
	gameObjectSreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
	//Sets the mouse pointers vector3
	mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
}


void OnMouseDrag()
{
	mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
	force = mouseCurLocation - mousePreviousLocation;//Changes the force to be applied
	mousePreviousLocation = mouseCurLocation;
		updateVelocity = true;
}

public void OnMouseUp()
{

	//Makes sure there isn't a ludicrous speed

	if (body.velocity.magnitude > topSpeed)
			force = (body.velocity.normalized) * topSpeed;
}

public void FixedUpdate()
{
		if (updateVelocity) {
			body.velocity = force;
		}
}
}