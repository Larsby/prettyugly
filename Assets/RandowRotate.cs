using UnityEngine;
using System.Collections;

public class RandowRotate : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 359.0f));	
	}
	

}
