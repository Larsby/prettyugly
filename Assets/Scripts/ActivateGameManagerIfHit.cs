using UnityEngine;
using System.Collections;

public class ActivateGameManagerIfHit : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		{
			if (col.transform.CompareTag ("Blob")) {
				GetComponent<TellGameMangerImOut> ().enabled = true;
				//	GetComponent<GoFromNearestHole> ().enabled = false;

			}
			if (col.transform.CompareTag ("Ugly")) {
				GetComponent<TellGameMangerImOut> ().enabled = true;
				//	GetComponent<GoFromNearestHole> ().enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
