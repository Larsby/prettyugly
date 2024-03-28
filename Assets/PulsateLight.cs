using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsateLight : MonoBehaviour
{
	public float fromValue = 1.1f;
	public float toValue = 1.4f;
	public float timePlus = 0.5f;
	private float val;
	float time;
	Light MyLight;
	bool dim = false;
	// Use this for initialization
	void Start ()
	{
		MyLight = GetComponent<Light> ();
		val = fromValue;
		time = Time.deltaTime;
		StartCoroutine (StartDim (Random.Range (0.0f, 1.0f)));
	}


	IEnumerator StartDim (float delay)
	{
		yield return new WaitForSeconds (delay);
		dim = true;
	}

	
	// Update is called once per frame
	void Update ()
	{

		if (dim) {
			
			time += timePlus * Time.deltaTime;
			MyLight.intensity = Mathf.Lerp (fromValue, toValue, time);

			if (time > 1.0f) {
				var t = fromValue;
				fromValue = toValue;
				toValue = t;
				time = 0.0f;
			}
		}
	}

}
