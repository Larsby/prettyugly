using UnityEngine;
using System.Collections;

public class DestroyOnLeaveScreen : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 viewPos = Camera.main.WorldToViewportPoint (transform.position);
		if ((viewPos.x < 0.0f || viewPos.x > 1.0f) || (viewPos.y < 0.0f || viewPos.y > 1.0f)) {
			Destroy (gameObject);
		}
	}
}
