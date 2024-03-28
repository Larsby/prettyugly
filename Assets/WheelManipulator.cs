using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManipulator : MonoBehaviour
{
	WheelJoint2D joint2D;
    // Start is called before the first frame update
    void Start()
    {
		joint2D = GetComponent<WheelJoint2D>();
		Invoke("Flip", 1.0f);
        
    }
	void Flip() {
		JointSuspension2D suspension2D = joint2D.suspension;
		float f = Random.Range(joint2D.suspension.frequency - 0.2f, joint2D.suspension.frequency + 0.2f);
		if (f <= 0) f = 0.3f;
		if (f > 0.8f) f = 0.6f;
		suspension2D.frequency = f;
		joint2D.suspension = suspension2D;
		Invoke("Flip", Random.Range(1.0f,2.0f));
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
