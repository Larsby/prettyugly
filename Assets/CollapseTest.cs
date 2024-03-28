using UnityEngine;
using System.Collections;

public class CollapseTest : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.localScale.x > 0.0f) {
			transform.localScale = new Vector3 (transform.localScale.x - 0.1f, transform.localScale.y, transform.localScale.z);
			transform.position = new Vector3 (transform.position.x - 0.05f, transform.position.y, transform.position.z);
		}	
	}
}
