using UnityEngine;
using System.Collections;

public class Bouncer : MonoBehaviour {
	public float breathOut = 1.0f;
	public float breathIn = 0.5f;
	public float shrinkScale = 0.97f;
	public float growScale = 1.0f;

	// Use this for initialization
	void Start () {
	//	shrink ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void shrink() {
		float scale = shrinkScale;

		float delay = 0;
		iTween.ScaleTo (this.gameObject,iTween.Hash("time",breathOut,"x",scale,"y",scale,"z",scale,"id","pulse", "delay",delay,"oncomplete", "grow","easetype",iTween.EaseType.easeInOutQuad));
	}
	void grow() {
		float delay = 0;
		float scale = growScale;

		iTween.ScaleTo (this.gameObject,iTween.Hash("time",breathIn,"x",scale,"y",scale,"z",scale,"id","pulse", "delay",delay,"easetype",iTween.EaseType.easeInOutQuad));
	}


	public void DoAnimation() {
	
		shrink ();
	
	
	}

}
