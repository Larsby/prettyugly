using UnityEngine;
using System.Collections;

public class SortingLayerByScale : MonoBehaviour
{
	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float order = transform.localScale.x * 1000f;
		float orderLayer = Mathf.Ceil (order / 10);
		spriteRenderer.sortingOrder = (int)orderLayer - 100;
	}
}
