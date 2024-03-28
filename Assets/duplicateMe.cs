using UnityEngine;

public class duplicateMe : MonoBehaviour
{
	[HideInInspector]
	public float initialWait = 0;
	[HideInInspector]
	public bool isBig = true;
	[HideInInspector]
	public bool isGrowing = false;

	private float growTime = 3f;
	private float coolDownTime = 0.2f;

    void Start() {}

    void Update()
    {
		if (initialWait > 0)
			initialWait -= Time.deltaTime;

		if (!isBig)
		{
			growTime -= Time.deltaTime;
			if (growTime <= 0)
			{
				transform.localScale = new Vector3(0.6f, 0.6f, 1f);
				isBig = true;
				initialWait = coolDownTime;

				GetComponent<CircleCollider2D>().radius = 1f;
				Invoke("RestoreCirc", coolDownTime - 0.05f);
				isGrowing = true;
			}
		}
    }

	void RestoreCirc()
	{
		isGrowing = false;
		GetComponent<CircleCollider2D>().radius = 0.58f;
	}

	public static int maxClones = 250;

	//bool cloned = false;

	private void OnCollisionEnter2D(Collision2D collision)
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

		duplicateMe other = collision.gameObject.GetComponent<duplicateMe>();

		if (other && other.isBig == true && other.initialWait <= 0)
		{
			//cloned = true;

			if (maxClones <= 0)
				return;
			maxClones--;

			initialWait = coolDownTime;

			GameObject g = Instantiate(gameObject, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
			g.GetComponent<duplicateMe>().initialWait = coolDownTime;
			g.GetComponent<duplicateMe>().isBig = false;
			g.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
		}
	}

}
