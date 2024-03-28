using UnityEngine;
using System.Collections;

public class FixPlanet : MonoBehaviour
{
	SpriteRenderer ren;
	Vector2 currentPosition;
	Fatness fat;
	Bounds bounds;

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
		ren = GetComponent<Target> ().mainBodyTarget.GetComponent<SpriteRenderer> ();
		fat = GetComponent<Fatness> ();
		//CheckAllPoints ();
		currentPosition = transform.position;

	}




	// Update is called once per frame
	void Update ()
	{
		if (transform.position != new Vector3 (currentPosition.x, currentPosition.y, transform.position.z)) {


			float left = getTopLeft (ren).x;
			float right = getTopRight (ren).x;
			float topy = getTop (ren).y;
			float bottomy = getBottom (ren).y;
			//	MessureArea.instance.EatAt (transform.position, fat);
			currentPosition = transform.position;
			for (float x = left; x <= right; x += 0.2f) {
				for (float y = topy; y > bottomy; y -= 0.2f) {

					MessureArea.instance.RepairAt (new Vector2 (x, y));

				}


			}





		}

	}

}
