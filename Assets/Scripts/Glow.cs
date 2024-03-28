using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour {
	public bool updateWhenParentChange = false;
	public bool _enabled = true;
	SpriteRenderer _renderer;
	private Vector3 parentLocalScale;
	private bool updated; 
	void Start () {
		if (transform.parent == null) {
			_enabled = false;

		} else {
		
		}
		_renderer = GetComponent<SpriteRenderer> ();
		_renderer.enabled = _enabled;
		if (enabled) {
			_renderer.color = new Color (1f, 1f, 1f,0.498f);
			transform.position = GetComponentInParent<SpriteRenderer> ().bounds.center;
			parentLocalScale = gameObject.gameObject.transform.parent.localScale;
			updated = false;

		}
	}
	
	// Update is called once per frame
	void Update () {
		_renderer.enabled = _enabled;
		if(enabled) {
			if (updateWhenParentChange == false && parentLocalScale != gameObject.transform.parent.localScale && updated == false) {

				gameObject.transform.localScale = new Vector3 (gameObject.transform.parent.localScale.x, gameObject.transform.parent.localScale.y, gameObject.transform.parent.localScale.z);
				updated = true;
			} else if (updateWhenParentChange) {
				gameObject.transform.localScale = gameObject.transform.parent.localScale;
					
			}
	
				}
	}
}
