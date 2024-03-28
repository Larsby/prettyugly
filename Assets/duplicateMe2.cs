using UnityEngine;
using UnityEngine.UI;

public class duplicateMe2 : MonoBehaviour
{
	[HideInInspector]
	public float initialWait = 0;
	[HideInInspector]
	public bool isBig = true;
	[HideInInspector]
	public bool isGrowing = false;

	private float growTime = 3f;
	private float coolDownTime = 0.2f;

	public GameObject buttonCanvas;

	public float multAreaSize = 5f;
	public float healAreaSize = 3f;
	public float growAreaSize = 2f;
	public float attractAreaSize = 7f;

	public GameObject multActiveArea;
	public GameObject multArea;
	public Button multButton;

	public GameObject healActiveArea;
	public GameObject healArea;
	public Button healButton;

	public GameObject growActiveArea;
	public GameObject growArea;
	public Button growButton;

	public Button explodeButton;

	public GameObject attractActiveArea;
	public GameObject attractArea;
	public Button attractButton;


	void Start() {
		multActiveArea.transform.localScale = Vector3.one * multAreaSize;
		multArea.transform.localScale = Vector3.one * multAreaSize;
		multButton.onClick.AddListener(Mult);

		healActiveArea.transform.localScale = Vector3.one * healAreaSize;
		healArea.transform.localScale = Vector3.one * healAreaSize;
		healButton.onClick.AddListener(Heal);

		growActiveArea.transform.localScale = Vector3.one * growAreaSize;
		//growArea.transform.localScale = Vector3.one * growAreaSize;
		growButton.onClick.AddListener(GrowLarge);

		explodeButton.onClick.AddListener(Explode);

		attractActiveArea.transform.localScale = Vector3.one * attractAreaSize;
		attractArea.transform.localScale = Vector3.one * attractAreaSize;
		attractButton.onClick.AddListener(Attract);
	}

	void Update()
    {
		if (initialWait > 0)
			initialWait -= Time.deltaTime;

		/*
		if (!isBig)
		{
			growTime -= Time.deltaTime;
			if (growTime <= 0)
			{
				Grow();
			}
		} */
    }


	public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, bool addTorque)
	{
		if (explosionRadius < Vector3.Distance(body.transform.position, explosionPosition))
			return;
		var dir = (body.transform.position - explosionPosition);
		float wearoff = 1 - (dir.magnitude / explosionRadius);
		body.AddForce(dir.normalized * explosionForce * wearoff);

		if (addTorque) body.AddTorque(Random.Range(-6, 6) * explosionRadius);
	}

	public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier, bool addTorque)
	{
		if (explosionRadius < Vector3.Distance(body.transform.position, explosionPosition))
			return;
		var dir = (body.transform.position - explosionPosition);
		float wearoff = 1 - (dir.magnitude / explosionRadius);
		Vector3 baseForce = dir.normalized * explosionForce * wearoff;
		body.AddForce(baseForce);

		float upliftWearoff = 1 - upliftModifier / explosionRadius;
		Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
		body.AddForce(upliftForce);

		if (addTorque) body.AddTorque(Random.Range(-6, 6) * explosionRadius);
	}

	void Explode()
	{
		GrowTo(2f, false);
		growActiveArea.transform.SetParent(null);
		Destroy(growActiveArea, 0.1f);
		Destroy(transform.parent.gameObject,0);
	}


	void GrowTo(float size, bool healZlimbies = true)
	{
		if (transform.localScale.x >= size)
			return;

		transform.localScale = new Vector3(size, size, 1f);
		isBig = true;
		initialWait = coolDownTime;

		growActiveArea.SetActive(true);
		Invoke("RestoreCirc", coolDownTime - 0.05f);
		isGrowing = healZlimbies;

		duplicateMe2[] dups = FindObjectsOfType<duplicateMe2>();
		foreach (duplicateMe2 d in dups) {
			if (d != this)
			{
				AddExplosionForce(d.GetComponent<Rigidbody2D>(), 15000, transform.position, 2.5f * size, true);
			}
		}
		//Debug.Break();
	}


	void Grow()
	{
		GrowTo(0.6f);
	}

	void GrowLarge()
	{
		PopContextMenu(false);
		GrowTo(0.9f);
	}


	void RestoreCirc()
	{
		growActiveArea.SetActive(false);
		isGrowing = false;
		//GetComponent<CircleCollider2D>().radius = 0.58f;
	}

	public static int maxClones = 250;

	//bool cloned = false;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		OnTriggerEnter2D(collision.collider);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (initialWait > 0)
			return;

		if (!isBig)
		{
			if (collision.gameObject.tag == "Blob")
				Destroy(gameObject);

			return;
		}

		//if (cloned)
		//	return;

		//print(collision.gameObject.name);

		duplicateMe2 other = collision.gameObject.GetComponent<duplicateMe2>();

		if (!multActiveArea.activeSelf)
			return;

		if (other && other.isBig == true && other.initialWait <= 0)
		{
			//cloned = true;

			if (maxClones <= 0)
				return;
			maxClones--;

			//initialWait = coolDownTime;

			//GameObject g = Instantiate(transform.parent.gameObject, transform.parent.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);

			Vector3 diff = other.transform.position - transform.position;
			Vector3 newPos = transform.position + diff / 2f;
			newPos = new Vector3(newPos.x, newPos.y, 0);
			GameObject g = Instantiate(transform.parent.gameObject, newPos, Quaternion.identity);

			duplicateMe2 dm2 = g.GetComponentInChildren<duplicateMe2>();
			dm2.initialWait = coolDownTime;
			dm2.isBig = false;
			dm2.PopContextMenu(false);
			dm2.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
			dm2.transform.localPosition = new Vector3(0,0,1);
		}
	}


	private void OnMouseDown()
	{
		//print("HIT " + gameObject.name);
		if (!isBig)
		{
			Grow();
			duplicateMe2[] dups = FindObjectsOfType<duplicateMe2>();
			foreach (duplicateMe2 d in dups)
				d.PopContextMenu(false);
			return;
		}

		if (buttonCanvas.activeSelf) {
			PopContextMenu(false);
		} else {
			transform.parent.position = new Vector3(transform.position.x, transform.position.y, 0);
			transform.localPosition = new Vector3(0, 0, 1);

			duplicateMe2[] dups = FindObjectsOfType<duplicateMe2>();
			foreach (duplicateMe2 d in dups)
				d.PopContextMenu(false);

			PopContextMenu(true);
		}
	}

	public void PopContextMenu(bool state) {
		buttonCanvas.SetActive(state);
		multArea.SetActive(state);
		multActiveArea.SetActive(false);
		healArea.SetActive(state);
		healActiveArea.SetActive(false);
		//growArea.SetActive(state);
		//growActiveArea.SetActive(false);
		attractArea.SetActive(state);
		attractActiveArea.SetActive(false);
	}

	void Mult()
	{
		PopContextMenu(false);
		multActiveArea.SetActive(true);
		Invoke("ResetMult", 0.5f);
	}
	void ResetMult()
	{
		multActiveArea.SetActive(false);
	}

	void Heal()
	{
		PopContextMenu(false);
		healActiveArea.SetActive(true);
		isGrowing = true;
		Invoke("ResetHeal", 0.5f);
	}
	void ResetHeal()
	{
		isGrowing = false;
		healActiveArea.SetActive(false);
	}

	void Attract()
	{
		SlimeStretchChecker2[] dups = FindObjectsOfType<SlimeStretchChecker2>();
		foreach (SlimeStretchChecker2 d in dups)
		{
			AddExplosionForce(d.GetComponent<Rigidbody2D>(), -1500, transform.position, attractAreaSize * 1.2f, true);
		}

		PopContextMenu(false);
		attractActiveArea.SetActive(true);
		Invoke("ResetAttract", 0.5f);
	}
	void ResetAttract()
	{
		attractActiveArea.SetActive(false);
	}

	private void LateUpdate()
	{
		if (transform.parent.position.z > 0 || transform.parent.position.z < 0)
			transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, 0);
	}

}
