using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetArms : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		PlayerController ctrl = GetComponentInParent<PlayerController>();
		ctrl.ResetArms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
