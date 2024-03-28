using UnityEngine;
using System.Collections;

public class SetBodySprite : MonoBehaviour {
	public GameObject target;
	public Sprite[] sprite_bodies;
	private SpriteRenderer _renderer;
	// Use this for initialization
	void Start () {
		_renderer = target.GetComponent<SpriteRenderer> ();
		_renderer.sprite = sprite_bodies[Random.Range (0, sprite_bodies.Length)];
	}
	public void SetSprite(Sprite sprite) {
		_renderer = target.GetComponent<SpriteRenderer> ();
		_renderer.sprite = sprite;
	}
	
}
