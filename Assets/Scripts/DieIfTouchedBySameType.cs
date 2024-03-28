using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieIfTouchedBySameType : MonoBehaviour
{
	// Start is called before the first frame update
	SpringJoint2D[] joints;
    void Start()
    {
		GameManager.instance.RegisterBall();
		joints = gameObject.GetComponents<SpringJoint2D>();
        
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

				GameManager.instance.DeRegisterBall();

				Destroy(obj1, 0.01f);
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
		}else if (obj2.CompareTag("Obstacle"))
		{
			return;
		}
		else {
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

	void OnCollisionEnter2D(Collision2D col) {
		if (joints != null)
		{
			foreach (Joint2D j in joints)
			{
				if (j && j.connectedBody == null)
				{
					Destroy(j);
				}
			}
		}
		CompareTags(gameObject, col.gameObject);
			
	}

    // Update is called once per frame
    void Update()
    {
		
		
	}
}
