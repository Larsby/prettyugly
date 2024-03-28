using UnityEngine;
using System.Collections;

public class BouncerCollision : MonoBehaviour {
	public GameObject scriptSourceObject;
	private BouncerMainBody anim;
	// Use this for initialization
	void Start () {
		anim = scriptSourceObject.GetComponent<BouncerMainBody> ();
	}
	void OnCollisionEnter2D(Collision2D col) {
		anim.DoAnimation ();
	}
	void  OnTriggerEnter2D(Collider2D col) {
		anim.DoAnimation ();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
