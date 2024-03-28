using UnityEngine;
using System.Collections;

public class Eat : MonoBehaviour {
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
	
		animator.SetTrigger ("EatEnemy");
		col.gameObject.SetActive (false);


	}
}
