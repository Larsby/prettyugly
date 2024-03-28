using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachLineRenderer : MonoBehaviour
{
	LineRenderer rd;
	// Start is called before the first frame update
	public Transform from;
	public Transform to;

    void Start()
    {
	rd  =	GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
		rd.SetPosition(0, from.position);
		rd.SetPosition(1, to.position);
    }
}
