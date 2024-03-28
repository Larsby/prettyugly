using System.Collections.Generic;
using UnityEngine;

public class SlimeStretchChecker2 : MonoBehaviour
{
	public float breakDistance = 9f;

	public GameObject lineRenderPrefab;
	public SpringJoint2D s2Prefab;
	public GameObject cutiePrefab;

	[System.Serializable]
	public class Connection
	{
		public GameObject other;
		public LineRenderer lr;
		public SpringJoint2D s2d;
	}

	public Connection [] connections;

	[HideInInspector]
	public float coolDown = 0f;

	private List<Connection> links = new List<Connection>();

	private bool breakSlimeOnGrow = true; // seems to be working but do we want this?

	void Start()
	{
		Application.targetFrameRate = 60;

		if (connections.Length > 0)
			foreach (Connection c in connections)
				links.Add(c);

		if (PlayerController.connectWithMouseClick)
			breakDistance = 100000;
	}

	void Update()
	{
		for (int i = links.Count - 1; i >= 0; i--) {
			Connection c = links[i];

			float dist = Vector3.Distance(transform.position, c.other.transform.position);
			//print(dist);

			if (dist > breakDistance)
			{
				c.lr.enabled = false;
				c.lr.gameObject.SetActive(false);
				c.s2d.enabled = false;
				links.RemoveAt(i);

				if (links.Count == 0)
				{
					Vector2 velo = GetComponent<Rigidbody2D>().velocity;
					float avelo = GetComponent<Rigidbody2D>().angularVelocity;
					GameObject cutie = Instantiate(cutiePrefab, transform.position, transform.rotation);
					cutie.GetComponent<Rigidbody2D>().velocity = velo;
					cutie.GetComponent<Rigidbody2D>().angularVelocity = avelo;
					cutie.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
					gameObject.SetActive(false);
					//Destroy(gameObject);
				}

				continue;
			}

			float lw = (breakDistance - dist) / 6f;
			if (lw > 0.7f) lw = 0.7f;

			//lr.widthMultiplier = dist;
			AnimationCurve curve = new AnimationCurve();
			curve.AddKey(0.0f, lw);
			curve.AddKey(1.0f, lw);

			c.lr.widthCurve = curve;


			c.lr.startWidth = lw;
			c.lr.endWidth = lw;

			if (coolDown > 0)
			{
				coolDown -= Time.deltaTime;

				if (coolDown <= 0)
					GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		OnTriggerEnter2D(collision.collider);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject g = collision.gameObject;
		AttachCutieToSlime(g);

		duplicateMe dm = g.GetComponent<duplicateMe>();
		duplicateMe2 dm2 = null; 
		if (g.transform.parent != null) dm2 = g.transform.parent.GetComponent<duplicateMe2>();
		if (breakSlimeOnGrow && ((dm && dm.isGrowing) || (dm2 && dm2.isGrowing)))
		{
			Vector2 velo = GetComponent<Rigidbody2D>().velocity;
			float avelo = GetComponent<Rigidbody2D>().angularVelocity;
			GameObject cutie = Instantiate(cutiePrefab, transform.position, dm2 == null? transform.rotation : Quaternion.identity);
			cutie.GetComponentInChildren<Rigidbody2D>().velocity = velo;
			cutie.GetComponentInChildren<Rigidbody2D>().angularVelocity = avelo;

			foreach (Connection c in links) {
				c.lr.enabled = false;
				c.lr.gameObject.SetActive(false);
				c.s2d.enabled = false;
				c.other.GetComponent<SlimeStretchChecker2>().RemoveMe(gameObject);
			}

			gameObject.SetActive(false);
			//Destroy(gameObject);

			return;
		}
	}


	public static GameObject firstClick = null;

	private void OnMouseDown()
	{
		if (!PlayerController.connectWithMouseClick)
			return;

			if (firstClick == null)
		{
			firstClick = gameObject;
		}
		else
		{
			AttachSlimeToSlime(firstClick);
			firstClick = null;
		}
	}


	private void AttachCutieToSlime(GameObject other)
	{
		duplicateMe dm = other.GetComponent<duplicateMe>();
		duplicateMe2 dm2 = other.GetComponent<duplicateMe2>();
		if (((dm && dm.isBig) || (dm2 && dm2.isBig)) && links.Count < 3 && coolDown <= 0)
		{
			GameObject ny = Instantiate(gameObject, other.transform.position, other.transform.rotation);
			if (dm) Destroy(dm.gameObject);
			if (dm2) Destroy(dm2.transform.parent.gameObject);
			ny.transform.parent = transform.parent;

			transform.parent.gameObject.name = (int.Parse(transform.parent.gameObject.name) + 1) + "";

			GameObject nyLR = Instantiate(lineRenderPrefab, transform.parent);
			AttachLineRenderer alr = nyLR.GetComponent<AttachLineRenderer>();
			alr.from = transform;
			alr.to = ny.transform;

			SpringJoint2D nysj = gameObject.AddComponent<SpringJoint2D>();
			nysj.dampingRatio = s2Prefab.dampingRatio;
			nysj.frequency = s2Prefab.frequency;
			nysj.connectedAnchor = s2Prefab.connectedAnchor;
			nysj.autoConfigureDistance = s2Prefab.autoConfigureDistance;
			nysj.enableCollision = s2Prefab.enableCollision;
			nysj.connectedBody = ny.GetComponent<Rigidbody2D>();
			nysj.distance = 3.5f;

			Connection newConn = new Connection();
			newConn.lr = nyLR.GetComponent<LineRenderer>();
			newConn.s2d = nysj;
			newConn.other = ny;
			links.Add(newConn);


			SlimeStretchChecker2[] killUs1 = ny.GetComponents<SlimeStretchChecker2>();
			for (int i = killUs1.Length - 1; i >= 0; i--) Destroy(killUs1[i]);

			SpringJoint2D[] killUs2 = ny.GetComponents<SpringJoint2D>();
			for (int i = killUs2.Length - 1; i >= 0; i--) Destroy(killUs2[i]);

			SlimeStretchChecker2 nyssc2 = ny.AddComponent<SlimeStretchChecker2>();
			nyssc2.breakDistance = breakDistance;
			nyssc2.lineRenderPrefab = lineRenderPrefab;
			nyssc2.s2Prefab = s2Prefab;
			nyssc2.coolDown = 5f;
			nyssc2.cutiePrefab = cutiePrefab;

			nyssc2.connections = new Connection[1];

			ny.GetComponent<SpriteRenderer>().color = Color.green;

			Connection newConn2 = new Connection();
			newConn2.lr = nyLR.GetComponent<LineRenderer>();
			newConn2.s2d = nysj;
			newConn2.other = gameObject;
			nyssc2.connections[0] = newConn2;


			coolDown = 0.2f;
		}
	}


	private void AttachSlimeToSlime(GameObject other)
	{
		foreach (Connection conn in links)
			if (conn.other == other)
				return;

		transform.parent.gameObject.name = (int.Parse(transform.parent.gameObject.name) + 1) + "";

		GameObject nyLR = Instantiate(lineRenderPrefab, transform.parent);
		AttachLineRenderer alr = nyLR.GetComponent<AttachLineRenderer>();
		alr.from = transform;
		alr.to = other.transform;

		SpringJoint2D nysj = gameObject.AddComponent<SpringJoint2D>();
		nysj.dampingRatio = s2Prefab.dampingRatio;
		nysj.frequency = s2Prefab.frequency;
		nysj.connectedAnchor = s2Prefab.connectedAnchor;
		nysj.autoConfigureDistance = s2Prefab.autoConfigureDistance;
		nysj.enableCollision = s2Prefab.enableCollision;
		nysj.connectedBody = other.GetComponent<Rigidbody2D>();

		//nysj.distance = 3.5f;

		nysj.distance = Vector3.Distance(other.transform.position, transform.position) / 4f;

		Connection newConn = new Connection();
		newConn.lr = nyLR.GetComponent<LineRenderer>();
		newConn.s2d = nysj;
		newConn.other = other;
		links.Add(newConn);

		SlimeStretchChecker2 nyssc2 = other.GetComponent<SlimeStretchChecker2>();

		Connection newConn2 = new Connection();
		newConn2.lr = nyLR.GetComponent<LineRenderer>();
		newConn2.s2d = nysj;
		newConn2.other = gameObject;
		nyssc2.AddConnection(newConn2);

		coolDown = 0.2f;
	}

	public void AddConnection(Connection conn)
	{
		links.Add(conn);
	}

	public void RemoveMe(GameObject g)
	{
		for (int i = links.Count - 1; i >= 0; i--)
		{
			Connection c = links[i];
			if (c.other == g)
			{
				links.RemoveAt(i);
				return;
			}
		}
	}

}
