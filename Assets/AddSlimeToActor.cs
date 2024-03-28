using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddSlimeToActor : MonoBehaviour
{

	// Use this for initialization

	public List <GameObject> slimedObjects;

	void Start ()
	{
		slimedObjects = new List<GameObject> ();
	}

	bool ExistInList (GameObject target)
	{
		foreach (GameObject obj in slimedObjects) {
			if (obj == target)
				return true;
		}
		return false;
	}

	void AddSllimeToActor (GameObject parent, GameObject target, Vector2 anchor)
	{

		slimedObjects.Add (parent);
	
		SpringJoint2D joint = target.AddComponent<SpringJoint2D> ();
		//	joint.anchor = new Vector2 (transform.position.x, transform.position.y);
		//	joint.connectedAnchor = anchor;
		joint.autoConfigureDistance = false;
		joint.connectedBody = GetComponent<Rigidbody2D> ();
		joint.distance = 0.5f;
		GameObject slimeObject = (GameObject)Instantiate (Resources.Load ("klibb_horizontal"));
		Stretch stretch = slimeObject.GetComponent<Stretch> ();
		stretch.gameObject1 = parent;
		stretch.gameObject2 = gameObject;

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("PrettyBody") == true) {
			if (ExistInList (col.gameObject.transform.parent.gameObject) == false) {
				//	RaycastHit2D hit = Physics2D.Raycast (col.gameObject.transform, Vector2.zero);

				AddSllimeToActor (col.gameObject.transform.parent.gameObject, col.gameObject, col.gameObject.transform.position);
				//Debug.Log ("Point of contact: " + hit.point);


			}
		}
	}

}
