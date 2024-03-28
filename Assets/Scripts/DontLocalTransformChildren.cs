using UnityEngine;
using System.Collections;

public class DontLocalTransformChildren : MonoBehaviour {
	public float s = 1.0f;
	private Transform[] children = null;
	// Use this for initialization

	void Start () 
	{
		children = new Transform[ transform.childCount ];
		int i = 0;  
		foreach (Transform t in transform) {
			children [i++] = t;
		}
	}

	void Update () 
	{
		if( s>transform.localScale.x )
		{
			transform.DetachChildren();                     
			transform.localScale = new Vector3( s, s, s );         
			foreach (Transform t in children) {         
				t.parent = transform;
			}
		}
	}

}
