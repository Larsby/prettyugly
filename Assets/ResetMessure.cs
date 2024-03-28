using UnityEngine;
using System.Collections;

public class ResetMessure : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GetComponent<MessureArea> ().Clear ();
		GetComponent<MessureArea> ().Calculate ();
	}

	void OnLevelWasLoaded (int level)
	{
		GetComponent<MessureArea> ().Clear ();
		GetComponent<MessureArea> ().Calculate ();

	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
