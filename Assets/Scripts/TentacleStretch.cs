using UnityEngine;
using System.Collections;

public class TentacleStretch : MonoBehaviour {
	public GameObject target;
	private Vector3 initialPosition;
	private Renderer renderer = null;
	public int index;
	// Use this for initialization
	void Start () {
	
	}
	public void StrechObject(Vector3 initialPosition, Vector3 finalPosition, bool mirrorZ) {
		Vector3 centerPos = (initialPosition + finalPosition)/2;
	//	gameObject.transform.position = centerPos;
		Vector3 direction = finalPosition - initialPosition;
		direction = Vector3.Normalize(direction);
		//gameObject.transform.right = direction;
		 gameObject.transform.right *= -1f;
		Vector3 scale = new Vector3(1,1,1);
		scale.y = Vector3.Distance (initialPosition, finalPosition)*1.9f;
		if (scale.y > 3.5f) {
			scale.y = 3.5f;
		}
		transform.rotation = Quaternion.LookRotation (Vector3.forward,  transform.position - target.transform.localPosition);

		transform.localScale = scale;
	}

	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 position = target.transform.localPosition;
			if (renderer == null) {
				renderer = target.GetComponent<SpriteRenderer> ();
				//position = renderer.bounds.center;
			}
			position = renderer.bounds.center;
			//position.x = position.x- (0.3f * index);
			StrechObject (transform.localPosition,position , true);
		}
	}
}
