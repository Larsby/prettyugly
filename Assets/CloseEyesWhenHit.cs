using UnityEngine;
using System.Collections;

public class CloseEyesWhenHit : MonoBehaviour
{

	// Use this for initialization
	public GameObject leftEye;
	public GameObject rightEye;

	void Start ()
	{
	
	}



	void OnCollisionEnter2D (Collision2D col)
	{
		leftEye.GetComponent<SpriteRenderer> ().enabled = true;
		rightEye.GetComponent<SpriteRenderer> ().enabled = true;
	}

	void OnCollisionExit2D (Collision2D col)
	{
		leftEye.GetComponent<SpriteRenderer> ().enabled = false;
		rightEye.GetComponent<SpriteRenderer> ().enabled = false;
	}

}
