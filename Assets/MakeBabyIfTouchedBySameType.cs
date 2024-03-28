using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBabyIfTouchedBySameType : MonoBehaviour
{
	// Start is called before the first frame update
	SpringJoint2D[] joints;
    void Start()
    {

		joints = gameObject.GetComponents<SpringJoint2D>();
        
    }
	void OnCollisionEnter2D(Collision2D col){
		CompareTags(gameObject, col.gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		CompareTags(gameObject, col.gameObject);
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
				if (obj1.GetComponent<Sick>() == null && obj2.GetComponent<Sick>() == null)
				{
					CreateUniqueBaby baby = GetComponent<CreateUniqueBaby>();
					if (baby)
					{

						baby.CreateBaby("" + obj1.GetHashCode(), "" + obj2.GetHashCode(), obj2.transform.position);
					}
				}

			}

		}
	}


    // Update is called once per frame
    void Update()
    {
		
		
	}
}
