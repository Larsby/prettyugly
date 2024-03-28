using UnityEngine;
using System.Collections;

public class SpriteSpawn : MonoBehaviour {
	public static GameObject latestPretty = null;
	public static GameObject latestUgly = null;
	string[] uglies = {"pusher_body_blob","Ugly","Ugly","Ugly","thorny"};
//	string[] uglies = {"ugly_1","ugly_2","ugly_3","ugly_4"};

	string[]  pretties = {"Pretty","Pretty","PrettyBaby"};    
	// Use this for initialization
	void Start () {
	}
	void CreateRandomPretty() {
		latestPretty  = CreateSprite (pretties);
	
	}

	GameObject CreateSprite(string [] list) {
		string name = list [Random.Range (0, list.Length )];

		GameObject spriteObject = (GameObject)Instantiate(Resources.Load(name));
		Vector3 randomRotation = new Vector3(0.0f,0.0f, Random.Range(1,280));
		spriteObject.transform.rotation = Quaternion.LookRotation(Vector3.forward,randomRotation );
	//	spriteObject.SetActive (false);
		GameObject body = spriteObject.GetComponent<Target>().mainBodyTarget;
		SpriteRenderer r = body.GetComponent<SpriteRenderer> ();

		bool loop = true;

		Vector2 pointA = new Vector2 (r.bounds.min.x/2, r.bounds.min.y/2);
		Vector2 pointB = new Vector2 (r.bounds.max.x/2, r.bounds.max.y/2);
		int i = 0;

		while (loop) {
			Collider2D collider = Physics2D.OverlapArea (pointA, pointB);
			if (collider != null) {
				Debug.Log ("You hit something!");
				pointA = new Vector2 (pointA.x /2 - r.bounds.min.x/2, pointA.y / 2- r.bounds.min.y/2);
				pointB = new Vector2 (pointB.x /2 - r.bounds.min.x/2, pointB.y/2- r.bounds.min.y/2);

			} else {
				loop = false;
				spriteObject.transform.position = new Vector3 (pointA.x/2, pointA.y/2, 0.0f);
				//gameObject.transform.position = pointB;
			}
			i++;
			if(i>10) {
				loop = false;
				Debug.Log("Your algorithm sucks more than you moma");
			}
		
		
		}
	
		return spriteObject;
	}
	void connectSticky(GameObject src) {
	
	}

	void CreateSticky() {
		if(latestPretty != null && latestUgly != null) {
			GameObject spriteObject = (GameObject)Instantiate(Resources.Load("klibb"));
			Stretch stretch = spriteObject.GetComponent<Stretch>();
			SpringJoint2D spring = latestUgly.GetComponent<SpringJoint2D> ();
			spring = null;
			if (spring == null) {
				spring = latestUgly.AddComponent<SpringJoint2D> ();
			}
			Rigidbody2D body = latestPretty.GetComponent<Rigidbody2D> ();
			//RigidbodyConstraints2D constraints = new RigidbodyConstraints2D ();
			Rigidbody2D uglyBody = latestUgly.GetComponent<Rigidbody2D>();
			uglyBody.constraints = RigidbodyConstraints2D.FreezeRotation;
			spring.connectedBody = body;
			spring.enabled = true;
			spring.autoConfigureDistance = false;
			spring.distance = 3.5f;

			stretch.gameObject1 = latestUgly.GetComponent<Target>().mainBodyTarget;
			stretch.gameObject2 = latestPretty.GetComponent<Target>().mainBodyTarget;
			//latestUgly = null;
		//	latestPretty = null;
		
		}
	}

	void CreateRandomUgly() {
		 latestUgly = CreateSprite(uglies);
		//if(sprite.name.StartsWith("pusher_body_blob") == false) {

		//	}
	}
	// Update is called once per frame
	void Update () {
			
	}
}
