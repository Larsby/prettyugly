using UnityEngine;
using System.Collections;

public class Stretcher : MonoBehaviour
{
	public GameObject gameObject1;
	// Reference to the first GameObject
	public GameObject gameObject2;

	public float angle1;
	public float angle2;
	public float rad1;
	public float rad2;

	// Reference to the second GameObject
	public GameObject sprite;
	public Sprite renderSprite;
	// Line Renderer
	private Vector3 sizeGameObject1;
	private Vector3 sizeGameObject2;
	private SpriteRenderer renderer1;
	private SpriteRenderer renderer2;
	private bool stretch = true;
	private PusherDrag drag = null;
	private PusherDrag2 drag2 = null;
	public bool useVectorPoints = false;
	private Rigidbody2D body = null;
	private bool finishWhenScaleIsComplete;
	public bool test = false;
	Vector3 originalPos;
	Quaternion originalRotation;
	Vector3 originalScale;
	bool dont = false;
	StretchPhase stretchPhase;
	GameObject originalGameObject1;
	GameObject originalGameObject2;
	private float mulitiplier = 1.0f;
	private Transform original;
	bool first = true;
	bool animate = true;
	public GameObject trashContainer;
	public GameObject targetContainer;
	public GameObject stretchControlerObject;
	private bool endStretch = false;
	float myscale = 0.0f;
	public float targetScale = 1.7f;
	// Use this for initialization
	public enum State
	{
		IDLE,
		PASSED,
		DONE
	}

	State s;

	public StretchPhase GetStretchPhase ()
	{
		return stretchPhase;
			
	}

	public void SetStretchPhase (StretchPhase phase)
	{
		stretchPhase = phase;
		if (stretchPhase == StretchPhase.STOP_STRETCH) {
			StopStretch ();
		} else if (stretchPhase == StretchPhase.STRETCH) {
			s = State.IDLE;
			StartStretch ();
		}
	}

	public void StopStretch ()
	{
		originalGameObject1 = gameObject1;
		GameObject obj = GameObject.FindGameObjectWithTag ("PusherPointer");
		transform.parent.transform.position = obj.transform.position;
		mulitiplier = 1.0f;
		s = State.IDLE;
		stretch = false;
	}

	public void StartStretch ()
	{
		gameObject1 = originalGameObject1;
		stretch = true;
		mulitiplier = 1.0f;

	}

	public void SetDrag (PusherDrag d)
	{
		drag = d;
	}

	public void SetDrag2 (PusherDrag2 d)
	{
		drag2 = d;
	}

	Vector3 getBottom (Renderer renderer)
	{
		return renderer.bounds.min;
	}

	Vector3 getTop (Renderer renderer)
	{
		return renderer.bounds.max;
	}

	Vector3 getTopLeft (Renderer renderer)
	{
		Vector3 min = getBottom (renderer);
		Vector3 max = getTop (renderer);
		return new Vector3 (min.x, max.y, 0.0f);
	}

	Vector3 getTopRight (SpriteRenderer renderer)
	{
		Vector3 min = getBottom (renderer);
		Vector3 max = getTop (renderer);
		return new Vector3 (max.x, max.y, 0.0f);
	
	}

	Vector3 getBottomRight (SpriteRenderer renderer)
	{
		Vector3 min = getBottom (renderer);
		Vector3 max = getTop (renderer);
		return new Vector3 (max.x, min.y, 0.0f);

	}

	Vector3 getBottomLeft (SpriteRenderer renderer)
	{
		Vector3 min = getBottom (renderer);
		Vector3 max = getTop (renderer);
		return new Vector3 (min.x, min.y, 0.0f);

	}

	Vector3 getCenter (Renderer renderer)
	{
		Vector3 result;

		return renderer.bounds.center;

	}



	Vector3 getSize (Transform child)
	{
		SpriteRenderer renderer = child.GetComponent<SpriteRenderer> ();
		if (renderer) {
			Vector3 sizer = renderer.bounds.size;
			Vector3 res = new Vector3 (sizer.x / 2, sizer.y / 2, sizer.z / 2);
			float width = res.x;
			float height = res.y;
			return res;
		}
		return Vector3.right;

	}

	public void Reset ()
	{
		transform.localPosition = originalPos;
		transform.localRotation = originalRotation;
		transform.localScale = originalScale;
	}

	// Use this for initialization
	void Start ()
	{
		if (gameObject != null && gameObject2 != null) {
			originalGameObject1 = gameObject1;
			sizeGameObject1 = getSize (gameObject1.transform);
			sizeGameObject2 = getSize (gameObject2.transform);
			renderer1 = gameObject1.GetComponent<SpriteRenderer> ();
			renderer2 = gameObject2.GetComponent<SpriteRenderer> ();

		}
		original = transform;
		originalPos = transform.position;
		originalRotation = transform.localRotation;
		originalScale = transform.localScale;
		endStretch = false;
		s = State.IDLE;
	}

	private bool tempTest = false;

	public void EndStretch ()
	{
		endStretch = true;
	}

	void SetSortingOrder (GameObject sprite, float scale)
	{
		float order = scale * 100f;
		float orderLayer = Mathf.Ceil (order / 10);
		sprite.GetComponent<SpriteRenderer> ().sortingOrder = (int)orderLayer;
	}

	public void StrechObject (GameObject sprite, Vector3 initialPosition, Vector3 finalPosition, bool mirrorZ)
	{
		Vector3 centerPos = (initialPosition + finalPosition) / 2.0f;
		sprite.transform.position = centerPos;
		Vector3 direction = finalPosition - initialPosition;
		direction = Vector3.Normalize (direction);
		sprite.transform.right = direction;
		if (mirrorZ)
			sprite.transform.right *= -1f;
		Vector3 scale = new Vector3 (0.8f, 1, 1);
	
		scale.x = Vector3.Distance (initialPosition, finalPosition) * mulitiplier;

		if (stretchPhase == StretchPhase.STOP_STRETCH) {
			if (s == State.PASSED && transform.localScale.x >= targetScale) {
				s = State.DONE;
				stretchPhase = StretchPhase.IDLE;
				stretchControlerObject.GetComponent<StretcherController> ().DisableStretchOnChild (gameObject);
				return;
			}
			if (myscale > transform.localScale.x) {
				s = State.PASSED;
			}
		}
	
		myscale = sprite.transform.localScale.x;
		sprite.transform.localScale = scale;
		//SetSortingOrder (sprite, scale.x);

		if (first) {
			originalPos = transform.position;

			originalScale = transform.localScale;
			//original = sprite.transform;
			first = false;
		}
	}

	Vector3 getPoint (Vector3 origin, float angle, float rad)
	{
		float a = angle * Mathf.PI / 180.0f;
		return new Vector3 (origin.x + rad * Mathf.Cos (a), origin.y + rad * Mathf.Sin (a), 0.0f);

	}


	// Update is called once per frame
	void FixedUpdate ()
	{

		if (test) {
			if (dont)
				return;
			if (dont == false) {
				GetComponent<Rigidbody2D> ().isKinematic = true;
				//transform.localScale = new Vector3 (0.6f, 0.6f, 1f);
				//	transform.rotation = Quaternion.RotateTowards (transform.rotation, targetContainer.transform.rotation, 1f);
				if (animate) {
					animate = false;
					iTween.MoveTo (gameObject, iTween.Hash ("position", targetContainer.transform.position, "time", 0.8, "islocal", false));
					iTween.ScaleTo (gameObject, targetContainer.transform.localScale, 0.8f);
					//	iTween.ScaleTo (gameObject, iTween.Hash("scale", new Vector3(0.0f,0.0f,1.0f), "delay",1.0f,"time",0.3));
					iTween.RotateTo (gameObject, iTween.Hash ("rotation", targetContainer.transform, "time", 0.8));
				}

				/*if (animate) {
					animate = false;
					iTween.ScaleTo (gameObject, iTween.Hash ("x", 0.0f, "time", 5.5f));
					//iTween.MoveTo (gameObject, iTween.Hash ("position", originalPos, "time", 4.0f, "islocal", true));
					iTween.MoveTo (gameObject, gameObject2.GetComponent<Renderer> ().bounds.center, 4.0f);
					//dont = true;
				}
				*/
				return;

			}
		}
		if (drag != null) {
			rad1 = drag.radius1;
			rad2 = drag.radius2;
		}
		if (drag2 != null) {
			rad1 = drag2.radius1;
			rad2 = drag2.radius2;
		}
		if (drag == null)
			return;
		if (gameObject1 != null && gameObject2 != null) {	
			if (body == null) {
				body = gameObject2.GetComponent<Rigidbody2D> ();
			}

		
			if (stretchPhase != StretchPhase.IDLE) {
				Vector3 startPos = getPoint (gameObject1.transform.position, angle1, rad1);
				Vector3 endtPos = getPoint (gameObject2.transform.position, angle2, rad2);
				StrechObject (gameObject, startPos, endtPos, true);

			}
		} else if (stretchPhase == StretchPhase.STRETCH_ANIM) {
			test = true;
		}




	}


}

	
	
