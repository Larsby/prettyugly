using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPrintLayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("My layer is " + gameObject.layer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
