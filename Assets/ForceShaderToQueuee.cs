using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceShaderToQueuee : MonoBehaviour {

	public int Queue;
	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().material.renderQueue = Queue;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
