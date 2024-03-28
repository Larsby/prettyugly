using UnityEngine;
using System.Collections;

public class DetectStonesAndConnectStretch : MonoBehaviour
{

	// Use this for initialization
	private GameObject target1;
	private GameObject target2;
	private bool initilized = false;

	void Start ()
	{
		target1 = null;
		target2 = null;

	}

	void OnTriggerStay2D (Collider2D col)
	{
	}

	void OnCollisionStay2D (Collision2D col)
	{
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (initilized == false) {
			if (other.CompareTag ("Obstacle")) {
				if (target1 == null) {
					target1 = other.gameObject;
					return;
				}
				if (target1 != null && target1.gameObject != other.gameObject) {
					if (target2 == null) {
						target2 = other.gameObject;
						initilized = true;
						Stretcher s = gameObject.GetComponent<Stretcher> ();
						s.gameObject1 = target1;
						s.gameObject2 = target2;
						gameObject.GetComponent<SpriteRenderer> ().sprite = s.renderSprite;
					}
				}	
			}
		}
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
