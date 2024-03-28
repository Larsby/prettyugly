using UnityEngine;
using System.Collections;

public class TurnAround : MonoBehaviour
{


	bool move = false;
	Vector3 target;
	Rigidbody2D body;
	Character pretty;
	Character pink;
	Character currentTarget;
	public Boxcaster visibleCaster;
	public GameObject targetObj = null;
	bool randomLooking = false;

	void Reset ()
	{
		randomLooking = false;
	}

	void Start ()
	{
		body = GetComponent<Rigidbody2D> ();
	}

	void RandomlyLookForSomethingToLookAt ()
	{
		
		if (body.velocity == Vector2.zero && randomLooking == false) {
			//Debug.Log ("Started to look");
			//iTween.RotateAdd (gameObject, new Vector3 (0.0f, 0.0f, 360f), Random.Range (0.5f, 2.5f), "oncomplete", "Reset");
			randomLooking = true;
		}
	}

	void FixedUpdate ()
	{
		if (body.velocity != Vector2.zero) {
			return;
		}
	
		if (GameManager.instance != null && GameManager.instance.IsReady () == false) {

			return;
		}
		float minDistance = Mathf.Infinity;
		move = false;
	
		foreach (GameObject obj in visibleCaster.hitGO) {
			float distance = Vector2.Distance (transform.position, obj.transform.position);
			if (obj.tag.Equals ("Pretty")) {
				move = true;
				if (distance < minDistance) {
					minDistance = distance;
					target = obj.transform.position;
		
				
				}
			}
	
		}

		if (move && target != Vector3.zero) {
			Quaternion q = Quaternion.Inverse (Quaternion.LookRotation (Vector3.forward, transform.position - target));

			//q.eulerAngles = new Vector3 (0.0f, 0.0f, q.eulerAngles.z);
			body.MoveRotation (Quaternion.Slerp (transform.rotation, q, Time.deltaTime * 5f).eulerAngles.z);

		} else if (randomLooking == false) {
			RandomlyLookForSomethingToLookAt ();
			randomLooking = true;
		}
	}
}
