using UnityEngine;
using System.Collections;

public class DetatchFromParentTransform : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		transform.SetParent (null);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
