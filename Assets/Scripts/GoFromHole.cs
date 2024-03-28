using UnityEngine;
using System.Collections;

public class GoFromHole : MonoBehaviour, IWeight
{
	
	// Use this for initialization
	private Vector3 initialPosition;
	private Vector3 destinationPosition;
	private Vector3 currentPosition;
	private bool moved = false;
	private float speed = 100.0f;
	public bool done = false;

	void Start ()
	{
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

		initialPosition = transform.position;
		currentPosition = initialPosition;

	}

	bool TryMove (Vector3 pos, Vector3 dest)
	{
		float step = 0.1f;
		Vector3 newPosition = Vector3.MoveTowards (pos, dest, step);
		Collider2D collider = Physics2D.OverlapCircle (newPosition, 0.2f);
		if (collider != null && collider.gameObject.CompareTag ("Obstacle")) {

			return false;
		}
		currentPosition = newPosition;
		return true;
	}



	void Move ()
	{
		if (TryMove (currentPosition, destinationPosition) == false) {

			//todo do distance checks
			bool success = TryMove (new Vector3 (currentPosition.x, currentPosition.y - 0.1f, currentPosition.z), destinationPosition);
			if (success == false) {
				success = TryMove (new Vector3 (currentPosition.x, currentPosition.y + 0.1f, currentPosition.z), destinationPosition);

			}
			if (success == false) {
				success = TryMove (new Vector3 (currentPosition.x - 0.1f, currentPosition.y + 0.1f, currentPosition.z), destinationPosition);

			}
			if (success == false) {
				success = TryMove (new Vector3 (currentPosition.x - 0.1f, currentPosition.y - 0.1f, currentPosition.z), destinationPosition);

			}
			if (success == false) {
				success = TryMove (new Vector3 (currentPosition.x + 0.1f, currentPosition.y + 0.1f, currentPosition.z), destinationPosition);

			}
			if (success == false) {
				success = TryMove (new Vector3 (currentPosition.x + 0.1f, currentPosition.y - 0.1f, currentPosition.z), destinationPosition);

			}
			if (success == false) {

				Debug.Log ("could not move!!!");
				return;
			}


		}
		if (currentPosition == destinationPosition) {
			moved = true;
			Destroy (this);

		}
		if (moved == false) {
			
			MessureArea.instance.LandAvailable (new Vector2 (currentPosition.x, currentPosition.y));
		}
	}

	void Update ()
	{
		if (GameManager.instance.IsReady ()) {
			if (done == false) {
				for (int i = 0; i < 1600; i++) {
					Move ();
				}
				done = true;
			}
		}
	}

	void IWeight.ChangeWeight ()
	{

	}

	void IWeight.AlreadyEaten ()
	{
	}


	

}
