using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowIfTouchedBySameType : MonoBehaviour
{// Start is called before the first frame update
	SpringJoint2D[] joints;
	static Dictionary<string, string> uniquehitName = new Dictionary<string, string>();
	int hash = 0;
	void Start()
	{
		GameManager.instance.RegisterBall();
		joints = gameObject.GetComponents<SpringJoint2D>();
		hash = gameObject.GetHashCode();
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		//	CompareTags(gameObject, col.gameObject);
	}

	void CompareTags(GameObject obj1, GameObject obj2)
	{
		//Debug.Log("Obj1" + obj1.name + " obj2 " + obj2.name);
		if (obj1.CompareTag(obj2.tag))
		{
			if (obj1.CompareTag("Obstacle"))
			{
				return;
			}
			if (obj1.GetComponent<Little>() == null && obj2.GetComponent<Little>() == null)
			{
				string tkey1 = ""+obj1.GetHashCode();
				string key2 = ""+obj2.GetHashCode();
				if (uniquehitName.TryGetValue(tkey1 + "t" + key2, out string bla))
				{
					
					return;
				}
				else if (uniquehitName.TryGetValue(key2 + "t" + tkey1, out string bla2))
				{

					return;
				}
				else
				{

					uniquehitName.Add(tkey1 + "t" + key2, "braker");
					//	GameManager.instance.DeRegisterBall();

					gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x+obj2.transform.localScale.x,gameObject.transform.localScale.y +obj2.transform.localScale.y, 1.0f);
					GameObject ob = Instantiate(gameObject);
					ob.GetComponent<Rigidbody2D>().rotation = obj1.GetComponent<Rigidbody2D>().rotation;

					ob.GetComponent<Rigidbody2D>().velocity = obj1.GetComponent<Rigidbody2D>().velocity;

					Destroy(obj2);
					Destroy(obj1);
				}
			}
			else
			{
				Debug.Log("Faramba!");
			}
		}
		else if (obj1.CompareTag("Blob"))
		{
			//Debug.Break();
			return;
		}
		else if (obj2.CompareTag("Blob"))
		{
			//			Debug.Break();
			return;
		}
		else if (obj1.CompareTag("Obstacle"))
		{
			return;
		}
		else if (obj2.CompareTag("Obstacle"))
		{
			return;
		}
		else
		{
			//Debug.Log("yeah");
			//	Debug.Break();
			if (obj1.GetComponent<Little>() == null && obj2.GetComponent<Little>() == null)
			{
				CreateUniqueBaby baby = GetComponent<CreateUniqueBaby>();
				if (baby)
				{

					baby.CreateBaby("" + obj1.GetHashCode(), "" + obj2.GetHashCode(), obj2.transform.position);
				}
			}

		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		foreach (Joint2D j in joints)
		{
			if (j && j.connectedBody == null)
			{
				Destroy(j);
			}
		}
		CompareTags(gameObject, col.gameObject);

	}

	// Update is called once per frame
	void Update()
	{


	}
}


