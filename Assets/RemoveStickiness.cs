using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveStickiness : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		targets = new Dictionary<int, Transform>();
	}
	public bool activateOnCollision = false;
	// Update is called once per frame
	void Update()
	{

	}
	Dictionary<int, Transform> targets = new Dictionary<int, Transform>();


	public void DoIt(GameObject connectedObj) {
		string tag = connectedObj.tag;
		if (tag.StartsWith("Size"))
		{

			Transform parent = connectedObj.transform.parent;

			foreach (Transform child in parent)
			{
				// do whatever you want with child transform object here
				GameObject ch = child.gameObject;
				if (ch.layer == 14)
				{
					// hardcoded, we know slime is on layer 14.
					RopeControllerSimple ropeControllerSimple = ch.GetComponent<RopeControllerSimple>();
					if (ropeControllerSimple)
					{
						bool remove = false;
						bool add = true;
						if (ropeControllerSimple.whatTheRopeIsConnectedTo == connectedObj.transform)
						{
							remove = true;
						}
						if (ropeControllerSimple.whatTheRopeIsConnectedTo == transform)
						{
							remove = true;
						}
						if (ropeControllerSimple.whatIsHangingFromTheRope == connectedObj.transform)
						{
							remove = true;
						}
						if (ropeControllerSimple.whatIsHangingFromTheRope == transform)
						{
							remove = true;
						}
						if (remove)
						{
							Destroy(ch);
						}
						if (!targets.TryGetValue(ropeControllerSimple.whatIsHangingFromTheRope.gameObject.GetHashCode(), out Transform bla))
						{
							targets.Add(ropeControllerSimple.whatIsHangingFromTheRope.gameObject.GetHashCode(), ropeControllerSimple.whatIsHangingFromTheRope);
						}
						if (!targets.TryGetValue(ropeControllerSimple.whatTheRopeIsConnectedTo.gameObject.GetHashCode(), out Transform bla2))
						{
							targets.Add(ropeControllerSimple.whatTheRopeIsConnectedTo.gameObject.GetHashCode(), ropeControllerSimple.whatTheRopeIsConnectedTo);
						}

					}
				}
			}

			SpringJoint2D[] springs = connectedObj.GetComponents<SpringJoint2D>();
			Debug.Log("Here should all the targets be listed");
			foreach (var item in targets)
			{
				Transform transform = item.Value;
				if (transform.gameObject != connectedObj)
				{
					Debug.Log("" + transform.gameObject.name);

					SpringJoint2D[] spriiing = transform.gameObject.GetComponents<SpringJoint2D>();
					foreach (SpringJoint2D s2 in spriiing)
					{
						if (s2.attachedRigidbody == connectedObj.GetComponent<Rigidbody2D>())
						{
							Destroy(s2);
						}
					}
				}


			}
			foreach (SpringJoint2D s in springs)
			{

				Destroy(s);
			}





		}
	
	void OnTriggerEnter2D(Collider2D col)
		{

			if (activateOnCollision)
			{
				GameObject obj = col.gameObject;
				DoIt(obj);
			}
		}
	}
}
