using UnityEngine;
using System.Collections;


public class CutoutTest : MonoBehaviour
{
	public GameObject target;
	public GameObject targetRenderObject;
	private MeshRenderer targetRenderer;

	Vector2 getBottom (SpriteRenderer renderer)
	{

		return new Vector2 (transform.position.x, transform.position.y - (renderer.bounds.extents.y / 2));
	}

	Vector2 getTop (SpriteRenderer renderer)
	{

		return new Vector2 (transform.position.x, transform.position.y + (renderer.bounds.extents.y / 2));
	}

	Vector2 getTopLeft (SpriteRenderer renderer)
	{

		Vector2 max = getTop (renderer);
		return new Vector2 (transform.position.x - renderer.bounds.extents.x / 2, max.y);
	}

	Vector2 getTopRight (SpriteRenderer renderer)
	{
		Vector2 max = getTop (renderer);
		return new Vector2 (transform.position.x + renderer.bounds.extents.x / 2, max.y);

	}

	Vector2 getBottomRight (SpriteRenderer renderer)
	{
		Vector2 min = getBottom (renderer);

		return new Vector2 (transform.position.x + renderer.bounds.extents.x / 2, min.y);

	}

	Vector2 getBottomLeft (SpriteRenderer renderer)
	{
		Vector2 min = getBottom (renderer);
		return new Vector2 (transform.position.x - renderer.bounds.extents.x / 2, min.y);

	}

	Vector2 getCenter (SpriteRenderer renderer)
	{


		return new Vector2 (transform.position.x + renderer.bounds.center.x, transform.position.y + renderer.bounds.center.y);

	}
	// Use this for initialization
	void Start ()
	{
		//targetRenderer = targetRenderObject.GetComponent<MeshRenderer> ();

	}

	void Update ()
	{

	
	}

}