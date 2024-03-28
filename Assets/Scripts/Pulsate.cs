using UnityEngine;
using System.Collections;

public class Pulsate : MonoBehaviour
{
	public float breathOut = 1.0f;
	public float breathIn = 0.5f;
	public float shrinkScale = 0.97f;
	public float growScale = 1.0f;
	private float delay = 0;
	private float scale = 0;
	public bool wait;
	public bool animationStopped = false;
	Rigidbody2D body;
	Fatness fatComponent = null;
	float shrinkInRelative;
	float growItInRelative;

	public void Wait ()
	{
		wait = true;
	}

	public void Play ()
	{
		wait = false;
		if (scale == shrinkScale) {
			shrink ();
		} else {
			grow ();
		}

	}

	void shrink ()
	{
		return;
		scale = shrinkScale;
		float delay = 0;
		if (wait == false) {
			iTween.ScaleTo (this.gameObject, iTween.Hash ("time", breathOut, "x", scale, "y", scale, "z", scale, "id", "pulse", "delay", delay, "oncomplete", "grow", "easetype", iTween.EaseType.easeInOutQuad));
		}
	}

	void grow ()
	{
		delay = 0;
		scale = growScale;
		if (wait == false) {
			iTween.ScaleTo (this.gameObject, iTween.Hash ("time", breathIn, "x", scale, "y", scale, "z", scale, "id", "pulse", "delay", delay, "oncomplete", "shrink", "easetype", iTween.EaseType.easeInOutQuad));
		}
	}

	void getScales ()
	{
	}

	public IEnumerator StartBreathing ()
	{	 
		yield return new WaitForSeconds (Random.Range (0.2f, 1.5f));
		shrink ();
	}

	void Init ()
	{

		fatComponent = GetComponentInParent<Fatness> ();
		float defaultScale = 1.0f;
		if (fatComponent != null) {
			defaultScale = fatComponent.scale;
		}
		shrinkScale = defaultScale - shrinkInRelative;
		growScale = defaultScale + growItInRelative;

		body = GetComponentInParent<Rigidbody2D> ();
		delay = (float)Random.Range (0, 5);
		StopCoroutine ("StartBreathing");
		StartCoroutine (StartBreathing ());
	}



	// Use this for initialization
	void Start ()
	{	
		shrinkInRelative = 1.0f - shrinkScale;
		shrinkInRelative = Mathf.Abs (shrinkInRelative);
		growItInRelative = growScale - 1.0f;
		growItInRelative = Mathf.Abs (growItInRelative);
		Init ();
	}

	void Update ()
	{
		if (body == null) {
			body = GetComponentInParent<Rigidbody2D> ();
		}
		if (body == null)
			return;
		Vector2 vel = body.velocity;

		if (vel.x == 0 && vel.y == 0) {
			if (fatComponent != null) {
				fatComponent.enabled = false;
			}
			wait = false;
			if (animationStopped) {
				Init ();
				animationStopped = false;
			}
		} else {
			if (wait == false && fatComponent != null) {
				iTween.Stop (
					this.gameObject);
				if (fatComponent != null) {
					fatComponent.enabled = true;
					fatComponent.ReInit ();
				}
				animationStopped = true;

				
			}
			if (fatComponent != null) {
				wait = true;
			}
		}

		
	}

}