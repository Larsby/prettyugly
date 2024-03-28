using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Medvedya.SpriteDeformerTools;

public class DeformCollisionTest : MonoBehaviour
{
	Rigidbody2D rigidBody;
	public GameObject bodyObject;
	public GameObject gelObject;
	public bool animate = false;
	public float dir = 90;
	MegaBend bend;
	MegaBend bend2;
	float target;
	float angle;
	MegaAxis axis;
	MoveState moveState;

	enum MoveState
	{
		IDLE,
		START_MOVE,
		END_MOVE,
		BOUNCE_START,
		BOUNCE_END
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		animate = false;
		if (rigidBody == null)
			return;
		if (rigidBody.velocity != Vector2.zero) {
			Vector2 normal = col.contacts [0].normal;
			angle = Vector2.Angle (rigidBody.velocity, -normal);
			Vector2 heading = rigidBody.velocity;
			float x = Mathf.Abs (heading.x);
			float y = Mathf.Abs (heading.y);
			Vector2 heading2 = new Vector2 (x, y);
			Vector2 direction = col.contacts [0].point - new Vector2 (transform.position.x, transform.position.y);
			direction = -direction.normalized;
			axis = direction.x > direction.y ? MegaAxis.X : MegaAxis.Z;
			bend.angle = angle;
			bend2.angle = bend.angle;
			bend.axis = axis;
			bend2.axis = axis;
			bend.dir = dir;
			bend2.dir = dir;
			moveState = MoveState.BOUNCE_END;
			animate = true;		

			//	Debug.Log ("direction " + direction);

		}

			
	}

	void GetDependencies ()
	{
		bend = bodyObject.GetComponent<MegaBend> ();
		bend2 = gelObject.GetComponent<MegaBend> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Start ()
	{
		GetDependencies ();

		animate = false;
	}



	void OnCollisionExit2D (Collision2D col)
	{
		animate = true;
	}

	void Update ()
	{
		
		if (animate) {
			if (moveState == MoveState.BOUNCE_START) {
				if (bend.angle < angle - 10f) {
					
				} else {
					moveState = MoveState.BOUNCE_END;	
					bend.enabled = true;
					bend2.enabled = true;
				}
			}
			if (moveState == MoveState.BOUNCE_END) {
				if (bend.angle > 10) {

					bend.angle = bend.angle - 10f;
					bend2.angle = bend.angle;
					bend.axis = axis;
					bend2.axis = axis;
					bend.dir = dir;
					bend2.dir = dir;
				} else {
					moveState = MoveState.IDLE;
					bend.enabled = false;
					bend2.enabled = false;


				}
			}
		}
	}
}
		

