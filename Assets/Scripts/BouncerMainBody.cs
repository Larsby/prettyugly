using UnityEngine;
using System.Collections;

public class BouncerMainBody : MonoBehaviour {

	public float breathOut = 1.0f;
	public float breathIn = 0.5f;
	public float shrinkScale = 0.97f;
	public float growScale = 1.0f;
	public float shrinkAgainScale = 1.0f;
	public float shrinkAgainTime = 0.45f;
	private Bouncer pbody0;
	private Bouncer pbody1;
	public GameObject object0;
	public GameObject object1;
	public GameObject blinkObject;
	private SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
		pbody0 = getBouncer (object0);
		pbody1 = getBouncer (object1);
		renderer = blinkObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {

	}
	Bouncer getBouncer(GameObject sprite) {
		return sprite.GetComponent<Bouncer> ();
	}

	void shrink() {
		float scale = shrinkScale;

		float delay = 0;
		iTween.ScaleTo (this.gameObject,iTween.Hash("time",breathOut,"x",scale,"y",scale,"z",scale,"id","1", "delay",delay,"oncomplete", "grow","easetype",iTween.EaseType.easeInOutQuad));
	}
	void grow() {
		float delay = 0;
		float scale = growScale;

		iTween.ScaleTo (this.gameObject,iTween.Hash("time",breathIn,"x",scale,"y",scale,"z",scale,"id","2", "delay",delay,"oncomplete", "shrinkAgain","easetype",iTween.EaseType.easeInOutQuad));
	}
	void shrinkAgain() {
		float scale = shrinkAgainScale;

		float delay = 0;
		iTween.ScaleTo (this.gameObject,iTween.Hash("time",shrinkAgainTime,"x",scale,"y",scale,"z",scale,"id","3", "delay",delay,"easetype",iTween.EaseType.easeInOutQuad));
	}
	IEnumerator blink() {
		
		yield return new WaitForSeconds(0.25f);
		renderer.enabled = false;
	}

public void DoAnimation() {
		renderer.enabled = true;
		StartCoroutine (blink ());
		pbody0.DoAnimation ();
		pbody1.DoAnimation ();
		shrink ();


	}
}
