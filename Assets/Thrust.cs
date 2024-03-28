using UnityEngine;
using System.Collections;

public class Thrust : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Rigidbody2D body = GetComponent<Rigidbody2D> ();
		body.AddForce (new Vector2 (0.1f, 2), ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
