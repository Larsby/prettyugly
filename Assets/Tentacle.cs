using UnityEngine;
using System.Collections;

public class Tentacle : MonoBehaviour
{

	// Use this for initialization
	public float scale = 2.0f;
	Sprite original = null;
	float originalScale = 0.0f;
	SpriteRenderer ren;
	public bool developMode = true;

	void Start ()
	{
		ren = GetComponent<SpriteRenderer> ();
		ren.enabled = true;
		SetChildren ();
		originalScale = scale;
		original = ren.sprite;
		GetComponent<SpriteRenderer> ().enabled = false;
		originalScale = scale;
		original = ren.sprite;
	}

	void SetChildren ()
	{
		foreach (Transform t in transform) {
			foreach (Transform child in t) {
				child.gameObject.GetComponent<SpriteRenderer> ().sprite = ren.sprite;
				child.gameObject.GetComponent<SpriteRenderer> ().color = ren.color;
				child.localScale = new Vector3 (scale, scale, 1.0f);
			}
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!developMode)
			return;
		bool dirty = false;
		if (ren.sprite != original) {
			original = ren.sprite;
			dirty = true;
		}
		if (scale != originalScale) {
			originalScale = scale;
			dirty = true;
		}
		if (dirty) {
			dirty = false;
			SetChildren ();
		}
	
	}
}
