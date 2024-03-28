using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject obj = col.gameObject;
		GameObject root = col.gameObject;
		//Debug.Log("tag" + root.tag + "name" + root.name);
		if (root.CompareTag("Blob")) {
			if (GameManager.instance.IsPlayerAiming() == false)
			{
				GameManager.instance.GameOver();
			}
		}
			
		}
	}
