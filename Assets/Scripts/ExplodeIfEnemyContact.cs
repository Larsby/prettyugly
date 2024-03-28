using UnityEngine;
using System.Collections;

public class ExplodeIfEnemyContact : MonoBehaviour {
	Animator animator;
	Object boom;
	GameObject boomGameObject;
	bool checkIfAnimationEnded = false;
	bool triggerAnims = false;
	GameObject enemy = null;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();

	 boomGameObject = (GameObject)Instantiate (Resources.Load ("Boom"));
		animator = boomGameObject.GetComponent<Animator> ();
		boomGameObject.SetActive (false);

	}

	// Update is called once per frame
	void Update () {
		if (checkIfAnimationEnded) {
			
				
			if (animator.GetCurrentAnimatorStateInfo (0).length >
			    animator.GetCurrentAnimatorStateInfo (0).normalizedTime == false) {
						
				//animation is over.
				enemy.SetActive (false);

				boomGameObject.SetActive (false);
				checkIfAnimationEnded = false;
				this.enabled = false;
				gameObject.SetActive (false);

			} else {
				if (triggerAnims) {
					triggerAnims = false;
					iTween.ScaleTo (enemy, iTween.Hash ("scale", new Vector3 (1.20f, 1.20f, 1.0f), "time", 0.5f));
					iTween.ScaleTo (gameObject, iTween.Hash ("scale", new Vector3 (1.20f, 1.20f, 1.0f), "time", 0.5f));
					iTween.FadeTo (enemy, iTween.Hash ("alpha", 0.0f, "time", 0.3f,"delay",0.1f));
					iTween.FadeTo (gameObject, iTween.Hash ("alpha", 0.0f, "time", 0.3f,"delay",0.1f));


				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		// add check for triggers so we only trigger on enemy
		// probably use the coliders contact points too

		if (col.gameObject.CompareTag ("Pretty")) {
			if (triggerAnims == false) {
				Vector3 add = col.gameObject.transform.position + gameObject.transform.position;
				Vector3 middle = new Vector3 (add.x / 2, add.y / 2, 0.0f);
				if (boomGameObject != null) {
					boomGameObject.transform.position = middle;
					boomGameObject.SetActive (true);
					animator.SetTrigger ("Explode");
					checkIfAnimationEnded = true;
					//col.gameObject.SetActive (false);
					//gameObject.SetActive (false);
					enemy = col.gameObject;
					enemy.GetComponent<Animator> ().enabled = false;
					triggerAnims = true;
				}
			}
		}
	}
}
