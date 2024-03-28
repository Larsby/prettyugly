using UnityEngine;
using System.Collections;

public class Stretch : MonoBehaviour
{

	public GameObject gameObject1;
	// Reference to the first GameObject
	public GameObject gameObject2;
	// Reference to the second GameObject
	public GameObject sprite;
	public Vector2 targetPos = Vector2.zero;
	// Line Renderer
	private Vector3 sizeGameObject1;
	private Vector3 sizeGameObject2;
	private SpriteRenderer renderer1;
	private SpriteRenderer renderer2;
	public float myscale = 0.4f;
	public bool retract = false;
	private bool shrinkAnimationStarted = false;

	Vector3 getCenter (SpriteRenderer renderer)
	{
		return renderer.bounds.center;
	}

	Vector3 getSize (GameObject child)
	{
		SpriteRenderer renderer = child.GetComponent<SpriteRenderer> ();
		if (renderer == null) {
			// ugly assumption that 
			renderer = child.GetComponent<Target> ().mainBodyTarget.GetComponent<SpriteRenderer> ();

		}
		Vector3 sizer = renderer.bounds.size;
		Vector3 res = new Vector3 (sizer.x, sizer.y / 2, sizer.z / 2);
		float width = res.x;
		float height = res.y;
		return res;

	}


	// Use this for initialization
	void Start ()
	{
		if (gameObject1 != null && gameObject2 != null) {
			sizeGameObject1 = getSize (gameObject1);
			sizeGameObject2 = getSize (gameObject2);
			renderer1 = gameObject1.GetComponent<SpriteRenderer> ();
			if (renderer1 == null) {
				// ugly assumption that 
				renderer1 = gameObject1.GetComponent<Target> ().mainBodyTarget.GetComponent<SpriteRenderer> ();

			}
			renderer2 = gameObject2.GetComponent<SpriteRenderer> ();

			if (renderer2 == null) {
				// ugly assumption that 
				renderer2 = gameObject2.GetComponent<Target> ().mainBodyTarget.GetComponent<SpriteRenderer> ();

			}
		}
	}

	public void StrechObject (GameObject sprite, Vector3 initialPosition, Vector3 finalPosition, bool mirrorZ)
	{
		Vector3 centerPos = (initialPosition + finalPosition) / 2f;
		sprite.transform.position = centerPos;
		Vector3 direction = finalPosition - initialPosition;
		direction = Vector3.Normalize (direction);
		sprite.transform.right = direction;
		if (mirrorZ)
			sprite.transform.right *= -1f;
		Vector3 scale = new Vector3 (1, sprite.transform.localScale.y, sprite.transform.localScale.z);
		scale.x = Vector3.Distance (initialPosition, finalPosition) * myscale;
		sprite.transform.localScale = scale;
	}

	IEnumerator DieAt(float delay) {
		yield return new WaitForSeconds(delay);
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update ()
	{
     
		if (gameObject1 != null && gameObject2 != null) {

			if (retract == false) {
				if (targetPos != Vector2.zero) {
					StrechObject (gameObject, new Vector3 (targetPos.x, targetPos.y, 0.0f), getCenter (renderer2), false);
				} else {
					StrechObject (gameObject, getCenter (renderer1), getCenter (renderer2), false);
				}
			} else {
				if (shrinkAnimationStarted == false) {
					iTween.ScaleTo (gameObject, iTween.Hash ("x", 0.0f, "time", 1.5f));
					iTween.MoveTo (gameObject, getCenter (renderer2), 1.0f);
						
					shrinkAnimationStarted = true;
					StartCoroutine(DieAt(1.5f));
				}
				StrechObject (gameObject, getCenter (renderer2), getCenter (renderer1), true);
			}
           
		}
	
	

	}
}
