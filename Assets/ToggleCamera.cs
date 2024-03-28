using UnityEngine;
using System.Collections;

public class ToggleCamera : MonoBehaviour
{
	public GameObject obj1;
	public GameObject obj2;
	public bool toggle = false;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (toggle) {
			obj1.SetActive (false);
			obj2.SetActive (true);
		} else {
			obj1.SetActive (true);
			obj2.SetActive (false);
				
		}
	}
}
