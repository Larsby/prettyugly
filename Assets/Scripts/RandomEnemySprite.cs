using UnityEngine;
using System.Collections;

public class RandomEnemySprite : MonoBehaviour {

	public Sprite[] sprite_bodies;
	// Use this for initialization
	void Start () {
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = sprite_bodies[Random.Range (0, sprite_bodies.Length)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
