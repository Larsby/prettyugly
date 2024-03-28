using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUniqueBaby : MonoBehaviour
{
	static Dictionary<string, string> uniquehitName = new Dictionary<string, string>();


	 private static int count  = 0;
	private static int countAll = 0;
	public static int _MaxCount = 320;
	 static int totalChildCount = 0;
	private GameObject ob;
	Vector3 pos;

	string key1;
	public bool detachRigid = false;

	public void Start()
	{
		countAll++;
	}

	public IEnumerator grow() {
		yield return new WaitForSeconds(2.0f);
		Vector3 size = new Vector3(0.8f, 0.8f, 1.0f);
		if(ob != null) {


	
		ob.transform.localScale = size;
		var a = ob.GetComponent<DieIfSmallAndHitByPlayer>();
			count--;
			if (count < 0)
				count = 0;
		if (a)
		{
			Destroy(a);

		}
		var b = ob.GetComponent<Little>();
		if(b) {
			Destroy(b);
		}
		}
	}
	void OnDestroy()
	{
		Debug.Log("Totatl" + countAll);
	}
		     

	public IEnumerator Position() {
		yield return new WaitForSeconds(Random.Range(0.0f,3.0f));
		
		ob = Instantiate(gameObject);
	//	if(detachRigid)
	//		Destroy(ob.GetComponent<Rigidbody2D>());
	/*	if (gameObject.GetComponent<Little>() == null) {
			Joint2D[] joints = ob.GetComponents<Joint2D>();
			foreach (Joint2D j in joints)
			{
				Destroy(j);
			}
			*/
			ob.name = gameObject.name + "k" + key1;
			ob.AddComponent<DieIfSmallAndHitByPlayer>();
			ob.AddComponent<Little>();
			ob.tag = gameObject.tag;
			Vector3 size = new Vector3(0.5f, 0.5f, 0.5f);
			ob.transform.localScale = size;
		StartCoroutine(grow());	
		               ob.transform.position = pos;
		//}
	}

	IEnumerator DeleteKey(string tkey1) 
	{
		yield return new WaitForSeconds(2.0f);
		if (uniquehitName.TryGetValue(tkey1, out string bla)){
			uniquehitName.Remove(tkey1);

		}
	}
	public  void CreateBaby(string tkey1, string key2, Vector3 v2) {
		if (count < _MaxCount)
		{
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
				string key = tkey1 + "t" + key2;
				uniquehitName.Add(key, "braker");
				StartCoroutine(DeleteKey(key));

				pos = gameObject.transform.position;
				key1 = tkey1;

				StartCoroutine(Position());
				//		Invoke("Position", 0.5f);
				//instanitate new baby!
				Debug.Log("Make a baby!");

			}

			/*
				pos = gameObject.transform.position;
				Invoke("Position", 0.5f);
				//instanitate new baby!
				Debug.Log("Make a baby!");
				*/
			count++;
		}
	}



}
