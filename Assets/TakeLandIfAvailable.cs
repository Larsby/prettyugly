using UnityEngine;
using System.Collections;

public class TakeLandIfAvailable : MonoBehaviour
{
	SpriteRenderer ren;
	Vector2 currentPosition;
	Fatness fat;
	Bounds bounds;
	bool eat = false;

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

	Vector2 getCenterLeft (SpriteRenderer renderer)
	{
		return new Vector2 (transform.position.x - renderer.bounds.extents.x / 2, renderer.bounds.center.y);
	}

	Vector2 getCenterRight (SpriteRenderer renderer)
	{
		return new Vector2 (transform.position.x + renderer.bounds.extents.x / 2, renderer.bounds.center.y);
	}

	Vector2 getCenter (SpriteRenderer renderer)
	{


		return new Vector2 (renderer.bounds.center.x, renderer.bounds.center.y);

	}
	// Use this for initialization
	void Start ()
	{
		ren = GetComponent<Target> ().mainBodyTarget.GetComponent<SpriteRenderer> ();
		fat = GetComponent<Fatness> ();
		//CheckAllPoints ();
		currentPosition = transform.position;

	}

	void Eat ()
	{
		fat.Grow ();
		eat = true;
	}

	public IEnumerator StopParticleEmission (float waitTime)
	{	 
		yield return new WaitForSeconds (waitTime);
		if (eat == false) {
			IWeight weight = (IWeight)fat;
			weight.AlreadyEaten ();
		}

	}

	int frameCount = 0;
	void Update ()
	{

		if (GameManager.instance.IsReady () && MessureArea.instance && MessureArea.instance.HaveCalculatedPaths()) {
			frameCount++;
			if (frameCount % 5 != 0)
				return;
			if (frameCount > 100)
				frameCount = 0;
		}
		if (transform.position != new Vector3 (currentPosition.x, currentPosition.y, transform.position.z)) {
			
			currentPosition = transform.position;
			float left = getCenterLeft (ren).x;
			float right = getCenterRight (ren).x;
			bool ateAtAnyPoint = false;
			//	float topy = getTop (ren).y;
			//	float bottomy = getBottom (ren).y;
		
			//	MessureArea.instance.EatAt (transform.position, fat);
			//	currentPosition = transform.position;
		
			for (float x = left; x <= right; x += 0.1f) {
				//for (float y = topy; y > bottomy; y -= 0.1f) {
				if (MessureArea.instance != null) {
					bool success = MessureArea.instance.EatAt (new Vector2 (x, getCenterLeft (ren).y), fat);
					if (success) {
						ateAtAnyPoint = true;
						eat = true;
						StopCoroutine ("StopParticleEmission");
					}
				}
				//	}
		
		
			}

			if (ateAtAnyPoint == false) {
				eat = false;
				StopCoroutine ("StopParticleEmission");
				StartCoroutine (StopParticleEmission (2.0f));
					


			}

		

		
			//	MessureArea.instance.EatAt (getCenter (ren), fat);
		}
			
	}
}

