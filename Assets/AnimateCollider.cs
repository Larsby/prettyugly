using UnityEngine;
using System.Collections;

public class AnimateCollider : MonoBehaviour
{
	CircleCollider2D collider;
	float originalRadius;

	float max = 0.1f;
	// Use this for initialization
	void Start ()
	{
		collider = GetComponent<CircleCollider2D> ();
		originalRadius = collider.radius;
		max = originalRadius + 0.01f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (collider.radius == originalRadius) {
			collider.radius = max;
			return;
		}
		if (collider.radius == max) {
			collider.radius = originalRadius;

		}
	}
}
