using UnityEngine;
using System.Collections;

public class DontDestroyOnExit : MonoBehaviour
{
	void Awake ()
	{

		//if(transform.parent == null)
		DontDestroyOnLoad (gameObject);

	
	}

}
