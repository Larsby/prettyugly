using UnityEngine;
using System.Collections;

public class BackgroundEat : MonoBehaviour
{
	private bool eaten = false;
	//	Collider2D collider;
	// Use this for initialization
	void Start ()
	{
		eaten = false;
		//collider = GetComponent<Collider2D> ();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.transform.CompareTag ("Untagged")) {
			GetComponent<SpriteRenderer> ().enabled = false; // remove dot if over a rock etc
		}
		if (col.transform.CompareTag ("PrettyBody")) {
			if (eaten == false) {
				//	col.gameObject.GetComponent<Pulsate> ().enabled = false;
				// register eaten event to game manager
				GetComponent<SpriteRenderer> ().enabled = true;
				GameManager.instance.addFood ();
				GameObject obj = col.transform.parent.gameObject;
				Fatness fat = obj.GetComponent<Fatness> ();
				if (fat != null) {
					fat.Grow ();
				}
				eaten = true;

			}
			{
				//col.gameObject.GetComponent<Pulsate> ().enabled = true;

				//col.gameObject.GetComponent<Pulsate> ().enabled = true;
			}	
		}
	}


	
	// Update is called once per frame
	void Update ()
	{
	}
}
