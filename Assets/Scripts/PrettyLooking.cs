using UnityEngine;
using System.Collections;

public class PrettyLooking : MonoBehaviour
{
	GameObject[] uglies;
	// Use this for initialization
	void FindClosestUglie ()
	{		/*
		foreach (GameObject ugly in uglies) {
	
			Vector3 uglyPosition = ugly.GetComponent<Target> ().GetComponent<Collider2D> ();
			float uglyDistance = Vector3.Distance (uglyPosition, currentPosition);

			if (uglyDistance < distance) {
				distance = holeDistance;
				destinationPosition = holePosition;
				name = hole.name;
			}
		}
		*/
	}

	void Start ()
	{
		uglies = GameObject.FindGameObjectsWithTag ("Ugly");
	
	}
	/*
	 *
GameObject[] holes = GameObject.FindGameObjectsWithTag ("Hole");
		float distance = float.MaxValue;
		string name = "";
		foreach (GameObject hole in holes) {

			Vector3 holePosition = hole.GetComponent<Collider2D> ().bounds.center;
			float holeDistance = Vector3.Distance (holePosition, currentPosition);

			if (holeDistance < distance) {
				distance = holeDistance;
				destinationPosition = holePosition;
				name = hole.name;
			}
		}
	 */

	// Update is called once per frame
	void Update ()
	{
		//Vector3 position = objectToLookAt.transform.position;
		//transform.rotation = Quaternion.LookRotation (Vector3.forward, position - transform.position);
	}
}
