using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowSlimeIfTouchedBySick : MonoBehaviour
{
	public GameObject slime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	GameObject FindGameObjectWithTag(GameObject root, string tag) {
		foreach(Transform t in root.transform){
			GameObject o = t.gameObject;
			if(o.CompareTag(tag)) {
				return o;
			}
		}
		return null;;
	}

	IEnumerator AddStretchChecker(GameObject ob2,GameObject obj,SpringJoint2D joint,LineRenderer rd) {
		yield return new WaitForSeconds(1.5f);
		print("boooojj");
		SlimeStretchChecker checker = ob2.AddComponent<SlimeStretchChecker>();
		checker.lr = rd;
		checker.other = obj;
		checker.s2d = joint;
		checker.breakDistance = 6.0f;
		Destroy(this);
	}

	void OnCollisionEnter2D(Collision2D col) {
		GameObject obj = col.gameObject;
		if (obj.GetComponent<Sick>())
		{
			GameObject slimeInstance = Instantiate(slime);
			RopeControllerSimple ropeControllerSimple = slimeInstance.AddComponent<RopeControllerSimple>();
			ropeControllerSimple.whatTheRopeIsConnectedTo = obj.transform;
			ropeControllerSimple.whatIsHangingFromTheRope = gameObject.transform;
			//SpringJoint2D firstJoint = obj.AddComponent<SpringJoint2D>();
			//firstJoint.connectedBody = gameObject.GetComponent<Rigidbody2D>();
			SpringJoint2D joint = gameObject.AddComponent<SpringJoint2D>();
			joint.connectedBody = obj.GetComponent<Rigidbody2D>();

			joint.distance = 2.0f;
			joint.dampingRatio = 0.1f;
			joint.frequency = 0.4f;
			joint.autoConfigureDistance = false;

		

			StartCoroutine(AddStretchChecker(gameObject,obj, joint, slimeInstance.GetComponent<LineRenderer>()));

			GameObject body = FindGameObjectWithTag(gameObject, "PrettyBody");

			if(body) {
				SpriteRenderer rd = body.GetComponent<SpriteRenderer>();
				rd.color = Color.green;
			}
			gameObject.AddComponent<Sick>();

		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		print("jodo");
	}
}
