using UnityEngine;
using System.Collections;

public class GrowAsYouTravel : MonoBehaviour
{
	Rigidbody2D body;
	public int ticksToGrow = 10;
	public float increment = 0.1f;
	public float threashold = 0.1f;
	public float maxScale = 2.0f;
	private int ticks;
	private Vector3 scale;

	// Use this for initialization
	void Start ()
	{
		body =	GetComponent<Rigidbody2D> ();
		ticks = 0;
		scale = transform.localScale;
	}

	void FixedUpdate ()
	{
		Vector2 velocity = body.velocity;
		if (velocity.x > threashold || velocity.y > threashold) {
			ticks++;	
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if (ticks > ticksToGrow) {
			ticks = 0;
			// slopy grow method. Change fatt script and use that
			scale = new Vector3 (scale.x + increment, scale.y + increment, 0.0f);
			if (scale.x > maxScale || scale.y > maxScale) {
			} else {
				transform.localScale = scale;
			}
		}
	}
}
