using UnityEngine;
using System.Collections.Generic;

public class AddStickyOnContact : MonoBehaviour
{
	private bool toldGameManager = false;
	public float breakForce = 1.0f;
	public float targetDistance = 3.5f;
	public GameObject slimePrefab;
	public bool stickToObstacle = false;
	public class StickyInfo
	{
		public GameObject target;
		public GameObject klibb;
		public GameObject slime;
	}

	List <StickyInfo> targets;
	public GameObject pointOfContact;
	public RopeControllerSimple rope;
	public bool die = false;
	// Use this for initialization
	void Start ()
	{
		targets = new List<StickyInfo> ();

	}

	void DisconnectSlime ()
	{
		foreach (StickyInfo info in targets) {
			GameObject klibb = info.klibb;
			Stretch slime = klibb.GetComponent<Stretch> ();
			slime.retract = true; 
			Destroy (info.target.GetComponent<SpringJoint2D> ());
		}
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 viewPos = Camera.main.WorldToViewportPoint (transform.position);
		if (GameManager.instance != null && GameManager.instance.IsReady ()) {
			if (toldGameManager == false) {
				if ((viewPos.x < 0.0f || viewPos.x > 1.0f) || (viewPos.y < 0.0f || viewPos.y > 1.0f)) {
					toldGameManager = true;
					die = true;
				}
			}
		}
		if (die) {
			die = false;
			DisconnectSlime ();
		}
	}

	bool TargetAlreadyExist (GameObject obj)
	{
		foreach (StickyInfo target in targets) {
			if (target.target == obj)
				return true;
		}
		return false;
	}

	void AddToTargetList (GameObject obj, Vector2 position)
	{  
		GameObject sticky =  CreateStickyOnTarget (obj, position);
		
			StickyInfo info = new StickyInfo ();
			info.target = obj;
			info.klibb = sticky;
			targets.Add (info);


	}

	GameObject CreateStickyOnTarget (GameObject target, Vector2 position)
	{
		
		GameObject spriteObject = (GameObject)Instantiate (Resources.Load ("klibb"));
		GameObject slime = Instantiate ((GameObject)slimePrefab);
	//	spriteObject.transform.parent = transform;
		Stretch stretch = spriteObject.GetComponent<Stretch> ();
		RopeControllerSimple rs = slime.GetComponent<RopeControllerSimple> ();
		rs.whatTheRopeIsConnectedTo = gameObject.transform;
		rs.whatIsHangingFromTheRope = target.transform;
		slime.transform.parent = transform;
		//stretch.sprite = spriteObject;
		SpringJoint2D spring = target.GetComponent<SpringJoint2D> ();
		//	spring = null;
		if (spring == null && target.CompareTag ("Obstacle") == false) {
			spring = target.AddComponent<SpringJoint2D> ();
		}
		Rigidbody2D body = GetComponent<Rigidbody2D> ();
		//RigidbodyConstraints2D constraints = new RigidbodyConstraints2D ();
		Rigidbody2D targetBody = target.GetComponent<Rigidbody2D> ();

		targetBody.constraints = RigidbodyConstraints2D.FreezeRotation;
		if (target.CompareTag ("Obstacle") == false) {
			spring.connectedBody = body;
			spring.enabled = true;
			spring.autoConfigureDistance = false;
		//	spring.breakForce = breakForce;
			spring.distance = targetDistance;
		}
		if (target.CompareTag ("Blob") || target.CompareTag ("Obstacle")) {
			stretch.gameObject1 = target;
			stretch.targetPos = position;
		} else {
			stretch.gameObject1 = target.GetComponent<Target> ().mainBodyTarget;
		}
//		stretch.gameObject2 = latestPretty.GetComponent<Target>().mainBodyTarget;
		SpringJoint2D newJoint = gameObject.AddComponent<SpringJoint2D>();
		newJoint.connectedBody = targetBody;
		//newJoint.breakForce = breakForce;
		newJoint.distance = targetDistance;
		stretch.gameObject2 = gameObject;
		return spriteObject;
	}

	

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.CompareTag ("Klibb") || col.gameObject.CompareTag ("Blob") || (stickToObstacle == false && col.gameObject.CompareTag ("Obstacle"))) {
			// we are not interested in klibb touching the sticky. it's a part of the sticky so just return
			return;
		}
		if (TargetAlreadyExist (col.gameObject) == false) {

			AddToTargetList (col.gameObject, new Vector2 (col.contacts [0].point.x, col.contacts [0].point.y));
		}
	}
}
