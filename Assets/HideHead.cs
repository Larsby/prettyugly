using UnityEngine;
using System.Collections;

public class HideHead : MonoBehaviour
{

	Collider2D coll;
	// Use this for initialization
	bool animationInProgress;
	bool hide;
	bool prevState;
	Rigidbody2D body;

	void Start ()
	{
		coll = GetComponent<Collider2D> ();
		animationInProgress = false;
		hide = false;
		body = GetComponentInParent<Rigidbody2D> ();
	}

	void HideLogic (Collider2D col)
	{
		string name = this.name;

		if (col.gameObject.CompareTag ("Ugly"))
			return;
		if (col.gameObject.CompareTag ("Aimer"))
			return;
		if (col.gameObject.CompareTag ("Obstacle")) {
			if (body == null || (body != null && body.velocity != Vector2.zero)) {
				SetHideState ();
			} else {
				return;
			}
		}
		if (col.gameObject.CompareTag ("Klibb") == false) {
			SetHideState ();
		}
	}


	void OnTriggerStay2D (Collider2D col)
	{
		HideLogic (col);
	}

	void SetHideState ()
	{
		prevState = hide;
		hide = true;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		HideLogic (col);
	}

	public void Hide ()
	{
		SetHideState ();
	}

	public void Show ()
	{
		prevState = hide;
		hide = false;
	}

	void OnTriggerExit2D (Collider2D col)
	{
		Show ();
	}

	void ExitAnimation ()
	{
		animationInProgress = false;
	}

	void Update ()
	{
		if (animationInProgress == false && (prevState != hide)) {
			animationInProgress = true;
			float x = -0.32f; // non hiden head value;
			if (hide) {
				x = 0.0f;
			}
			iTween.MoveTo (gameObject, iTween.Hash ("position", new Vector3 (x, transform.localPosition.y, transform.localPosition.z), "duration", 0.5f, "oncomplete", "ExitAnimation", "islocal", true));
		}

	}
}
