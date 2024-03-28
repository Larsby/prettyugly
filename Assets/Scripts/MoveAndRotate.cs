using UnityEngine;
using System.Collections;

public class MoveAndRotate : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	// Use this for initialization
	private float rotationAngel = 0.0f;
	private float rotateNextFrame = 0.0f;
	void Start () {

	}

	public float speed = 0.1F;
	private bool calculateRotation = false;


	void OnMouseDown()
	{
		screenPoint =  new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

		Rigidbody2D body = GetComponent<Rigidbody2D> ();
		body.velocity = new Vector2 (0.0f, 0.0f);
	}



	void OnMouseDrag()
	{
		Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		Vector2 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);

		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.x = mousePosition.x * -1;
		//mousePosition.x = mousePosition.y * -1;
		mousePosition.y = mousePosition.y * -1;
		mousePosition.z = gameObject.transform.position.z;
		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position );
		transform.position = curPosition;

	}


}
