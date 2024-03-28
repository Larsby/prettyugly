using UnityEngine;
using System.Collections;

public class GrowlIfHit : MonoBehaviour
{
	AudioSource[] growls;
	Vector3 currentScale;
	Vector3 scaleTo;
	Rigidbody2D body;

	void OnAnimComplete ()
	{
		Debug.Log ("anim complete");
		//	GetComponent<Fatness> ().enabled = true;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (this.enabled == false) return;
		if (col.gameObject.CompareTag ("Blob")) {
			int index = Random.Range (0, growls.Length); 
			growls [index].Play ();

		} else {
			//GetComponent<Fatness> ().enabled = false;

			//iTween.PunchScale (gameObject, iTween.Hash ("amount", scaleTo, "time", 1f, "oncomplete", "OnAnimComplete"));
			//iTween.PunchScale (gameObject, scaleTo, 2f);
		}
	}
	// Use this for initialization
	void Start ()
	{
		growls = GetComponents<AudioSource> ();
		currentScale = transform.localScale;
		scaleTo = new Vector3 (currentScale.x * 1.1f, currentScale.y * 0.8f, currentScale.z);
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
