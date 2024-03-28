using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateSpritesIn_Circle : MonoBehaviour
{
	public GameObject target;
	public GameObject original;
	public Sprite verticalLeg;
	public Sprite horizontalLeg;
	public int numberOfSprites = 8;
	public float radius1 = 2.0f;
	public float radius2 = 0.5f;
	public float angleToFace = 45.0f;
	public bool faceOneDirection = false;
	private GameObject[] sprites;
	private LegSprite[] legSprites;
	public GameObject pusher;
	private Vector3 offset;
	private Vector3 tapVector;
	private GameObject root;
	// Use this for initialization
	private PusherDrag drag = null;
	List <Vector2> hingeJointAnchorOffsettList = new List<Vector2> ();
	List <Vector2> hingeJointconnectionOffsettList = new List<Vector2> ();

	public class LegSprite
	{
		public Sprite sprite;
		public Vector3 rotation;
	}

	void SetupJoints ()
	{
		hingeJointAnchorOffsettList.Add (new Vector2 (0.59f, 0.0f));
		hingeJointAnchorOffsettList.Add (new Vector2 (-0.40f, 0.0f));
		hingeJointAnchorOffsettList.Add (new Vector2 (-0.36f, 0.0f));
		hingeJointAnchorOffsettList.Add (new Vector2 (-0.36f, 0.0f));
		hingeJointAnchorOffsettList.Add (new Vector2 (0.374f, 0.0f));
		hingeJointAnchorOffsettList.Add (new Vector2 (-0.36f, 0.0f));
		hingeJointAnchorOffsettList.Add (new Vector2 (-0.2794f, 0.0f));
		hingeJointAnchorOffsettList.Add (new Vector2 (-0.36f, 0.0f));

		hingeJointconnectionOffsettList.Add (new Vector2 (0.56f, 0.0f));
		hingeJointconnectionOffsettList.Add (new Vector2 (0.3767f, -0.3767f));
		hingeJointconnectionOffsettList.Add (new Vector2 (0.0f, -0.5327f));
		hingeJointconnectionOffsettList.Add (new Vector2 (-0.3767f, 0.3767f));
		hingeJointconnectionOffsettList.Add (new Vector2 (-0.49f, 0.0f));
		hingeJointconnectionOffsettList.Add (new Vector2 (-0.3767f, 0.3767f));
		hingeJointconnectionOffsettList.Add (new Vector2 (0.0f, 0.598f));
		hingeJointconnectionOffsettList.Add (new Vector2 (0.3767f, 0.3767f));

	}

	Vector3 getPoint (Vector3 origin, float angle, float rad)
	{
		float a = angle * Mathf.PI / 180.0f;
		return new Vector3 (origin.x + rad * Mathf.Cos (a), origin.y + rad * Mathf.Sin (a), 1.0f);
	
	}

	public void ResetToNormal ()
	{
	
	}

	public void SetDrag (PusherDrag d)
	{
		drag = d;
	}

	void SetupLegs ()
	{
		float degrees = 360.0f / numberOfSprites;
		//	Vector3 center = GetComponent<SpriteRenderer> ().bounds.center;
		Vector3 startPosition = new Vector3 (transform.position.x, transform.position.y, 1.0f);
		sprites = new GameObject[ numberOfSprites];
		legSprites = new LegSprite[numberOfSprites];
		//for (int i = 0; i < numberOfSprites; i++) {
		int i = 0;
		foreach (Transform child in pusher.transform) {

			if (child.CompareTag ("PusherLeg")) {
				legSprites [i] = new LegSprite ();
				GameObject spriteObject = child.gameObject;
				legSprites [i].sprite = verticalLeg;
				spriteObject.GetComponent<SpriteRenderer> ().sprite = horizontalLeg;

				float angle = 360.0f - (degrees * i);
				startPosition = spriteObject.transform.position;

				spriteObject.transform.position = getPoint (startPosition, angle, radius1);
				//spriteObject.transform
				//	//	Debug.Log ("angle = " + angle);
				//spriteObject.GetComponent<SpriteRenderer>().sprite = 
				//spriteObject.transform.eulerAngles = new Vector3 (0, 0, 270);
				//	spriteObject.name = "Sprite" + i + " angle" + angle;
				// the circle is created from right and then down. The first sprite needs to be at angle 270 
				// (the foot needs to be rotated that much to begin with)
				if (faceOneDirection == true) {
					if (spriteObject.GetComponent<Stretcher> () == null) {
						spriteObject.AddComponent<Stretcher> ();
					}
					spriteObject.GetComponent<Stretcher> ().gameObject1 = gameObject;
					spriteObject.GetComponent<Stretcher> ().gameObject2 = target;
					spriteObject.GetComponent<Stretcher> ().angle1 = angle;
					spriteObject.GetComponent<Stretcher> ().angle2 = angle;
					spriteObject.GetComponent<Stretcher> ().SetDrag (drag);
					legSprites [i].rotation = spriteObject.transform.eulerAngles;
					//spriteObject.transform.eulerAngles = new Vector3 (0, 0, 270 - (angleToFace));
					//spriteObject.transform.eulerAngles = new Vector3 (0, 0, 270 - (45 * i));
					spriteObject.transform.eulerAngles = new Vector3 (0, 0, 270 - (45 * i));
				} else {
					spriteObject.transform.eulerAngles = new Vector3 (0, 0, 270 - (45 * i));
				}
				//		spriteObject.transform.parent = gameObject.transform;
		
				sprites [i] = gameObject;

				i++;
			}

		}
	}

	void CreateSprites ()
	{
		float degrees = 360.0f / numberOfSprites;
		//	Vector3 center = GetComponent<SpriteRenderer> ().bounds.center;
		Vector3 startPosition = new Vector3 (transform.position.x + 2.5f, transform.position.y + 2.5f, 1.0f);

		sprites = new GameObject[ numberOfSprites];
		for (int i = 0; i < numberOfSprites; i++) {
			GameObject spriteObject = (GameObject)Instantiate (original);
			float angle = 360.0f - (degrees * i);
			//	spriteObject.transform.position = getPoint (startPosition, angle, radius1);


		

		


			//	Debug.Log ("angle = " + angle);
				
			spriteObject.name = "Sprite" + i + " angle" + angle;
			// the circle is created from right and then down. The first sprite needs to be at angle 270 
			// (the foot needs to be rotated that much to begin with)
			if (faceOneDirection == true) {
				spriteObject.AddComponent<Stretcher> ();
				spriteObject.GetComponent<Stretcher> ().gameObject1 = gameObject;
				spriteObject.GetComponent<Stretcher> ().gameObject2 = target;
				spriteObject.GetComponent<Stretcher> ().angle1 = angle;
				spriteObject.GetComponent<Stretcher> ().angle2 = angle;
				spriteObject.GetComponent<Stretcher> ().SetDrag (drag);
				//spriteObject.transform.eulerAngles = new Vector3 (0, 0, 270 - (angleToFace));
				spriteObject.transform.eulerAngles = new Vector3 (0, 0, 270 - (45 * i));
			} else {
				spriteObject.transform.eulerAngles = new Vector3 (0, 0, 270 - (45 * i));
			}
			spriteObject.transform.parent = gameObject.transform;
			HingeJoint2D hinge = spriteObject.GetComponent<HingeJoint2D> ();
			hinge.autoConfigureConnectedAnchor = false;
			hinge.connectedBody = target.GetComponent<Rigidbody2D> ();
			hinge.connectedAnchor = hingeJointconnectionOffsettList [i];
			hinge.anchor = new Vector2 (-0.40f, 0.0f);

			SpringJoint2D spring = spriteObject.GetComponent<SpringJoint2D> ();
			spring.autoConfigureConnectedAnchor = false;
			spring.connectedBody = target.GetComponent<Rigidbody2D> ();
			sprites [i] = gameObject;	
			spriteObject.SetActive (true);

		}
	}

	public void Reset ()
	{
		float degrees = 360.0f / numberOfSprites;
		Vector3 startPosition = new Vector3 (transform.position.x, transform.position.y, 1.0f);
		int i = 0;
		foreach (Transform t in transform) {
			if (t.tag == "PusherLeg") {
				float angle = 360.0f - (degrees * i);
				GameObject spriteObject = t.gameObject;
				spriteObject.transform.position = getPoint (startPosition, angle, radius1);
				spriteObject.transform.eulerAngles = new Vector3 (0, 0, 270 - (45 * i));
				//spriteObject.transform.SetParent (gameObject.transform);
				spriteObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
				i++;
			}
		}

	}

	public void Reset (Vector3 startPosition, float radius, float angles)
	{
		float degrees = 360.0f / numberOfSprites;
		int i = 0;
		foreach (GameObject c in sprites) {
			Transform t = c.transform;
			if (t.tag == "PusherLeg") {
				float angle = 360.0f - (degrees * i);
				GameObject spriteObject = c;
				spriteObject.transform.position = getPoint (startPosition, angle, radius);
				spriteObject.transform.eulerAngles = new Vector3 (0, 0, 360.0f);
				//spriteObject.transform.parent = null;
				spriteObject.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
				i++;
			}
		}

	}

	float GetAngleOfLineBetweenTwoPoints (Vector3 p1, Vector3 p2)
	{
		float xDiff = p2.x - p1.x;
		float yDiff = p2.y - p1.y;

		return Mathf.Atan2 (yDiff, xDiff) * (180.0f / 3.14f);
	}

	void Start ()
	{
		root = new GameObject ();
		SetupJoints ();
		CreateSprites ();
		//SetupLegs ();
	}

	void setAngleFromMousePosition ()
	{
		float angle = GetAngleOfLineBetweenTwoPoints (tapVector, getPosition ());
		//	Debug.Log ("Angle is " + angle);
		int i = 0;
		foreach (Transform t in pusher.transform) {

			if (t.tag == "PusherLeg") {
				
				GameObject spriteObject = t.gameObject;
				spriteObject.transform.eulerAngles = new Vector3 (0, 0, angle);
				i++;
			}
		}
	}


	Vector2 getPosition ()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		Vector2 curScreenPoint = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		Vector2 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) - offset;
		return curPosition;
	}


}
