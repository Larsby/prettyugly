using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjectWithTag : MonoBehaviour
{
	// Start is called before the first frame update
	public static int intances;
	public static bool counted = false;
	public string tagToMatch;
	public static int index=0;
	public static bool won = false;

	void Start()
	{
		if (!counted)
		{
			GameObject[] temp = GameObject.FindGameObjectsWithTag(gameObject.tag);
			counted = true;
			intances = temp.Length;
			print("oh, " + intances);
		}

	}

	// Update is called once per frame
	void Update()
	{

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.CompareTag(tagToMatch))
		{
		index++;
			if(index>=intances){
			print("you won");
				won = true;
			}
		}
	}
	void OnTriggerExitr2D(Collider2D col)
	{
		if (col.gameObject.CompareTag(tagToMatch))
		{
			index--;
			if (index <0)
			{
				index = 0;
			
			}
		}
	}


}