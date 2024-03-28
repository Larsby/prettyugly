using UnityEngine;
using System.Collections;

public class AddSpeed : MonoBehaviour
{
	Rigidbody2D body;
	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody2D> ();
		body.AddForce (new Vector2 (1.0f, 0.0f));
		body.velocity = new Vector2 (1.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
