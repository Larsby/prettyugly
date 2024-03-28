using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakforce : MonoBehaviour
{
	Joint2D []joints;
	// Start is called before the first frame update
	RemoveStickiness stickiness;
	bool done = false;
    void Start()
    {
		joints = GetComponents<Joint2D>();
		foreach(Joint2D j in joints){
			j.breakForce = 100.01f;
		}
		stickiness = GetComponent<RemoveStickiness>();
    }

    // Update is called once per frame
    void Update()
    {
		if(joints[0] == null && done == false) {
			done = true;
			stickiness.DoIt(gameObject);
		}
        
    }
}
