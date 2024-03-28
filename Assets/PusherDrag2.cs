using UnityEngine;
using System.Collections;

public class PusherDrag2 : MonoBehaviour
{

	Vector3 tap;
	Rigidbody2D body;
	SpriteRenderer renderer;
	private Vector3 offset;
	private GameObject[] children = null;
	Transform parent = null;
	GameObject spawn = null;
	LegStretcher stretchGroup;
	public float radius1 = 0.2f;
	public float radius2 = 0.4f;
	GameObject recycle = null;
	GameOverForPusher gameOver;
	private bool mouseUp = false;
	private Vector3 savePosition;
	GameObject pusherPosition = null;
	float mass;
	private bool firstDragMotion = false;
	private float angularDrag;
	private float drag;
	Vector3 startDebug;
	Vector3 endDebug;
	public GameObject CenterColiderObject;
	public GameObject Aimer;
	private AimLine aimLine;
	//	private CircleCollider2D touchArea;
	private int fingerID;
	TouchState touchState;
	KeyState keyState;
	Vector2 position;
	AudioSource[] audio;

	private enum KeyState
	{
		IDLE = 1,
		DRAG = 2,
		RELEASE
	}

	private enum TouchState
	{
		IDLE = 1,
		TOUCH_DOWN = 2,
		TOUCH_DRAG,
		TOUCH_RELEASE}

	;


	public bool IsAiming ()
	{

		return !mouseUp;
	}

	public void EnableCentralColider ()
	{
	//	CenterColiderObject.GetComponent<Collider2D> ().isTrigger = false;
	}
	// Use this for initialization
	void Start ()
	{
		Debug.Log("Hallon!");
		aimLine = Aimer.GetComponent<AimLine> ();
		body = gameObject.GetComponent<Rigidbody2D> ();
		angularDrag = body.angularDrag;
		drag = body.drag;
		pusherPosition = (GameObject)Instantiate (Resources.Load ("PusherPoint"));
		touchState = TouchState.IDLE;
		keyState = KeyState.IDLE;
		fingerID = -1;	
		renderer = gameObject.GetComponent < SpriteRenderer> ();
		children = new GameObject[ transform.childCount ];
		int i = 0;  
		spawn = (GameObject)Instantiate (Resources.Load ("LegStretcher"));
		spawn.SetActive (false);
		spawn.GetComponent<LegStretcher> ().pusher = gameObject;
		stretchGroup = spawn.GetComponent<LegStretcher> ();
		stretchGroup.target = gameObject;
		stretchGroup.radius1 = radius1;
		stretchGroup.radius2 = radius2;
		stretchGroup.SetDrag (this);
		gameOver = gameObject.GetComponent<GameOverForPusher> ();
		//	touchArea = gameObject.AddComponent<CircleCollider2D> ();
		//	touchArea.radius = 1.8f;
		audio = GetComponents<AudioSource> ();
		mass = body.mass;
	}


	void OnMouseUp ()
	{
		#if UNITY_IOS
		// allow unity player to work in iphone mode without using a remote but real ios wont do doulbe calls.
		if (Input.touchCount > 0)
			return;
		#endif
		Vector2 currentPosition = getPosition ();



		OnUp (currentPosition);
	}

	void OnMouseDown ()
	{  
		#if UNITY_IOS
		if (Input.touchCount > 0)
			return;
		#endif
		Vector2 currentPosition = getPosition ();

		OnDown (currentPosition);
	}

	void OnMouseDrag ()
	{
		#if UNITY_IOS
		if (Input.touchCount > 0)
			return;
		#endif


		Vector2 currentPosition = getPosition ();


		OnDrag (currentPosition);
	}


	void RunTouchStateMachine ()
	{
		#if UNITY_IOS
		//	if (Input.touchCount > 0) {
		foreach (Touch touch in Input.touches) {
			var temp = Camera.main.ScreenPointToRay (touch.position);
			var ray = new Vector3 (temp.origin.x, temp.origin.y, 1.0f);
			if (touchState == TouchState.IDLE && fingerID == -1) {


				if (touch.phase == TouchPhase.Began) {


					RaycastHit2D hit = Physics2D.Raycast (ray, Vector2.zero);
					if (hit != null) {
						if (hit.transform != null && hit.transform.gameObject == gameObject) {
							// make sure that the finger id we save is actually touching the pusher. Ignore all other down events.
							fingerID = touch.fingerId;
							touchState = TouchState.TOUCH_DOWN;
							OnDown (new Vector2 (ray.x, ray.y));

							return;
						} 
					}
				}
			}
			if ((touchState == TouchState.TOUCH_DOWN || touchState == TouchState.TOUCH_DRAG) && fingerID == touch.fingerId && touch.phase == TouchPhase.Moved) {
				touchState = TouchState.TOUCH_DRAG;
				OnDrag (new Vector2 (ray.x, ray.y));
				//Debug.Log ("On Drag from state machine");
				return;
			}
			if (touchState != TouchState.IDLE && fingerID == touch.fingerId && touch.phase == TouchPhase.Ended) {
				touchState = TouchState.IDLE;
				fingerID = -1;
				OnUp (new Vector2 (ray.x, ray.y));
				//	Debug.Log ("On Up");
				return;
			}

		}
		//	}

		#endif
	}

	void Update ()
	{
		var	h = Input.GetAxis ("Horizontal"); 
		var v = Input.GetAxis ("Vertical"); 
		var f = Input.GetButtonDown ("Jump");

		if (h != 0) {
			h = h * 0.6f;
		}
		if (v != 0) {
			v = v * 0.6f;
		}
		if (f) {

		}
		if (keyState == KeyState.IDLE && (h != 0 || v != 0 || f)) {
			//	OnDown (transform.position);
			position = new Vector2 (transform.position.x + h, transform.position.y + v);
			keyState = KeyState.DRAG;
		}
		if (keyState == KeyState.DRAG && (h != 0 || v != 0)) { 
			position = new Vector2 (transform.position.x + h, transform.position.y + v);
			OnDrag (position);
		}
		if (keyState == KeyState.DRAG && f) {
			OnUp (position);
			keyState = KeyState.IDLE;
		}
		//	transform.position = new Vector2 (transform.position.x + h, transform.position.y + v);
		//	Debug.Log ("h =" + h + " v =" + v + " f = " + f);

	}

	void FixedUpdate ()
	{
		var	h = Input.GetAxis ("Horizontal"); 
		var v = Input.GetAxis ("Vertical"); 
		var f = Input.GetButtonDown ("Jump");

		if (h != 0) {
			h = h * 1;
		}
		if (v != 0) {
			v = v * 1;
		}
		if (f) {

		}
		if (keyState == KeyState.IDLE && (h != 0 || v != 0 || f)) {
			OnDown (transform.position);
			position = new Vector2 (transform.position.x + h, transform.position.y + v);
			keyState = KeyState.DRAG;
		}
		#if UNITY_IOS

		RunTouchStateMachine ();
		#endif

		if (body.velocity.x < 0.05 || body.velocity.y < 0.05) {
			body.angularDrag = 1.0f;
			body.drag = 1.0f;
			//		Debug.Log ("************************************");
		} else {
			body.angularDrag = angularDrag;
			body.drag = drag;
			//	Debug.Log ("##################################");
		}
		//	Debug.DrawLine (startDebug, endDebug);
	}


	void SetLegsActiveStatus (bool active)
	{
		foreach (Transform t in transform) {
			if (t.CompareTag ("PusherLeg")) {
				if (active) {
					//		t.gameObject.GetComponent<Stretcher> ().StartStretch ();
				} else {
					//		t.gameObject.GetComponent<Stretcher> ().StopStretch ();
				}
				//		t.gameObject.SetActive (active);			
			}
		}
	}

	IEnumerator setupGameOverListener ()
	{
		yield return new WaitForSeconds (2.0f);
		gameOver.enabled = true;
	}

	void  OnCollisionEnter2D (Collision2D col)
	{
		audio [1].Play ();
	}

	void OnUp (Vector2 touchPosition)
	{
		
		audio [0].Play ();
		//	Debug.Log ("OnMouseUp");
		//	pusherPosition.SetActive (true);
		pusherPosition.GetComponent<Collider2D> ().enabled = true;

		//CenterColiderObject.GetComponent<Collider2D> ().isTrigger = true;
		spawn.SetActive (false);
		spawn.transform.parent = transform;
		//SetLegsActiveStatus (true);
		int i = 0;
		//gameObject.GetComponent<PlacePusherLegs> ().Place ();
		Rigidbody2D body = GetComponent<Rigidbody2D> ();
		Vector3 position = pusherPosition.GetComponent<Collider2D> ().bounds.center;
		float distance = Vector2.Distance (transform.position, position);

		float powerDivider = 600f;
		/*if (distance <= 2.2f) {
			powerDivider = 800f;
		}
		if (distance <= 1.2f) {
			powerDivider = 1000f;
		}
		if (distance <= 0.8f) {
			powerDivider = 1200f;
		}
		if (distance <= 0.5f) {
			powerDivider = 1400f;
		}
		if (distance <= 0.3f) {
			powerDivider = 1600f;
		}
		if (aimLine.willHitObstacle && distance > 2.0f) {
			powerDivider = 1800;
		}*/
		powerDivider = 600f;
		//powerDivider += 400f;
		//	Debug.Log ("distance is " + distance + " powerDivider" + powerDivider);
		float forceFactor = distance / powerDivider;
		//	Debug.Log ("ForceFactor" + forceFactor);
		//	position.x = tap.x-((tap.x - position.x )*ex);
		//	position.y = tap.y-((tap.y - position.y )*ex);
		//body.velocity = new Vector3 ((tap.x - savePosition.x) * (forceFactor * 6), (tap.y - savePosition.y) * (forceFactor * 6), 1.0f);
		Vector2 velocity = new Vector2 (((position.x * 1.0f) - transform.position.x) * 1, ((position.y * 1.0f) - transform.position.y) * 1);
		//RaycastHit2D hit = Physics2D.Raycast (startDebug, endDebug, 10f);


		body.velocity = velocity;
		//Debug.Log ("" + body.velocity.x + ", " + body.velocity.y);
		//Debug.Log ("Force" + body.velocity.normalized * forceFactor);

		//body.velocity = new Vector3 (distance * 3.0f, distance * 3.0f, 0.0f);
		//spawn.transform.position = tap;

		spawn.GetComponent<LegStretcher> ().target = gameObject;
		Vector2 forceVec = body.velocity.normalized * forceFactor;

		//	Debug.Log ("Force" + forceVec.x + " " + forceVec.y);
		body.AddForce (forceVec, ForceMode2D.Impulse);
		//Debug.Log ("vec" + forceVec.x + " " + forceVec.y);
		body.mass = mass;

		if (body.velocity.x == 0.0f && body.velocity.y == 0.0f) {
			pusherPosition.GetComponent<Collider2D> ().enabled = true;
		}
		StartCoroutine (setupGameOverListener ());
		mouseUp = true;
		//	GetComponent<PlacePusherLegs> ().radius = 0.4f;
		//GetComponent<PlacePusherLegs> ().Place ();

	}



	void OnDown (Vector2  inputPosition)
	{
		//gameObject.GetComponent<Collider2D> ().isTrigger = true;
		//	Debug.Log ("OnDown");
		startDebug = new Vector3 (inputPosition.x, inputPosition.y, 0.0f);
		endDebug = startDebug;
		//SetLegsActiveStatus (true);
		body.mass = 0.0f;
		firstDragMotion = true;
		pusherPosition.GetComponent<Collider2D> ().enabled = false;

		mouseUp = false;
		gameOver.enabled = false;


		tap = inputPosition;
		//	tap = GetComponent<Renderer> ().bounds.center;
		pusherPosition.transform.position = transform.position;
		;
		body.rotation = 0;
		body.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
		if (recycle == null) {
			//	recycle = new GameObject ();
		}
		//recycle.transform.position = tap;
		//	spawn.GetComponent<CreateSpritesIn_Circle> ().target = recycle;
		savePosition = tap;
		//GetComponent<PlacePusherLegs> ().radius = 1.0f;
		//GetComponent<PlacePusherLegs> ().Place ();
		//	if (firstDragMotion) {
		//we don't set this up in onMouseDown because it can look visually bad. That's why we do it here.
		//	SetLegsActiveStatus (false);
		spawn.transform.position = transform.position;
		spawn.SetActive (true);
		spawn.transform.parent = null;
		firstDragMotion = false;



		//}

	}

	void OnDrag (Vector2 inputPosition)
	{
		//	Debug.Log ("OnMouseDrag ");
		//if (GetComponent<PlacePusherLegs> ().legsDone == true) {
		Vector3 curPosition = new Vector3 (inputPosition.x, inputPosition.y, 0.0f);



		//	float angleDegrees = GetAngleOfLineBetweenTwoPoints (curPosition, tap);
		//	int i = 0;
		float distance = Vector2.Distance (tap, curPosition) * 100;
		if (distance > 0.05) {

			//SetLegsActiveStatus (false);
		}

		//	distance = Vector2.Distance (tap, getPosition ());
		//	Debug.Log ("distance = " + distance);



		//	float ex2 = distance * 0.01f;
		float b = distance % 1000f;
		//Debug.Log ("BBB " + b);
		if (distance > 300.0f) {
			float ex = 300.0f / distance;

			curPosition.x = tap.x - ((tap.x - curPosition.x) * ex);
			curPosition.y = tap.y - ((tap.y - curPosition.y) * ex);
			radius1 = 0.2f;
		}
		savePosition = curPosition;
		// make sure that the user can't drag the pusher outside of the screen
		Vector3 viewPos = Camera.main.WorldToViewportPoint (curPosition);

		if ((viewPos.x < 0.0f || viewPos.x > 0.995f) || (viewPos.y < 0.05f || viewPos.y > 0.995f)) {
			distance = 0.0f;
		}
		if (distance > 0) {
			transform.position = curPosition;
		}
		//}
		startDebug = curPosition;

		endDebug = tap;
		//RaycastHit2D hit = Physics2D.Raycast (new Vector2 (endDebug.x, endDebug.y), new Vector2 (startDebug.x, startDebug.y), 3f);
		//	Debug.Log ("Hit" + hit.collider);
	}

	Vector2 getPosition ()
	{

		Vector2 curScreenPoint = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
		return curPosition;
	}

	void setScaleY (Transform child, float scale)
	{
		Vector3 localScale = child.localScale;
		localScale.y = scale;
		localScale.x = scale;
		child.localScale = localScale;
	}

	float GetAngleOfLineBetweenTwoPoints (Vector2 p1, Vector2 p2)
	{
		float xDiff = p2.x - p1.x;
		float yDiff = p2.y - p1.y;

		return Mathf.Atan2 (yDiff, xDiff) * (180.0f / 3.14f);
	}

	void setRotation (Transform child, float angle)
	{
		//child.eulerAngles = new Vector3 (0, 0, angle);
	}






}


