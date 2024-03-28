using UnityEngine;
using System.Collections;

public class IgnorePhysicsOnLayers : MonoBehaviour
{
	public int layer1;
	public int layer2;
	// Use this for initialization
	void Start ()
	{
		Physics.IgnoreLayerCollision (layer1, layer2, true);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
