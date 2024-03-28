using UnityEngine;
using System.Collections;

public class VelocityAnimaton : MonoBehaviour
{
	Rigidbody2D body;
	Vector3 originalScale;
	Vector3 currentScale;
	Vector2 currentVelocity;
	Vector2 prevVelocity;
	float factor = 0.1f;
	float max = 1.75f;
	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody2D> ();
		currentScale = originalScale = gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
	{   
		currentVelocity = body.velocity;
		prevVelocity = currentVelocity;

		if (currentVelocity.x > prevVelocity.x) {
			currentScale.x += factor;
		}
		if (currentVelocity.x < prevVelocity.x) {
			currentScale.x -= factor;
		}
		if (currentScale.x <= max || currentScale.x >= originalScale.x) {
			transform.localScale = currentScale;
		}

	}
}
