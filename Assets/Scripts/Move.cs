using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
	private Vector3 startPosition;
	private Vector3 offset;
	private float threshold = 9.0f;
	// Use this for initialization
	private float rotationAngel = 0.0f;
	private float rotateNextFrame = 0.0f;

	void Start ()
	{

	}

	public float speed = 0.1F;
	private bool calculateRotation = false;


	void OnMouseDown ()
	{
		startPosition = Input.mousePosition;
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
	}



	void OnMouseDrag ()
	{
		Vector2 curScreenPoint = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) - offset;
		Vector3 distance = Input.mousePosition - startPosition;
		distance.Normalize ();
		var facing = Vector3.Dot (distance, Vector3.up);
		transform.position = curPosition;
		if (Vector3.Distance (startPosition, Input.mousePosition) < threshold) {
			//	Debug.Log ("Below threshold");
			return;
		}
		if (facing >= 0.5) {
			//	Debug.Log ("Facing up");

		} else if (facing <= -0.5) {
			//Debug.Log ("Down with you!");
		} else {
			facing = Vector3.Dot (distance, Vector3.right);
			if (facing >= 0.5) {
				//	Debug.Log ("Facing right");

			} else {
				//	Debug.Log ("You lefty!");
			}

		}

	}

}
