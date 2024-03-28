using UnityEngine;
using System.Collections;

public class MoveToPositionAndReset : MonoBehaviour
{

	// Use this for initialization
	public Transform target;
	private Vector3 endPosition;
	private Vector3 originalPosition;
	private bool moved = false;
	private float speed = 1.0f;
	Light light;

	void Start ()
	{
		
		originalPosition = transform.position;
		endPosition = target.position;
		light = GetComponent<Light> ();
	}

	int frames = 0;

	void Update ()
	{
		if (transform.position == endPosition) {
			transform.position = originalPosition;

		}
		frames++;
		if (frames >= 1000) {
			frames = 0;
		}
		if (frames % 10 == 0) {
			
			float intensity = light.intensity;
			float newIntensity = Random.Range (intensity - 0.09f, intensity + 0.09f);
			light.intensity = newIntensity;
		}
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, endPosition, step);
	

	}
}
