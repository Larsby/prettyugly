using UnityEngine;
using System.Collections;

public class TrailsortOrder : MonoBehaviour
{
	public TrailRenderer renderer;
	// Use this for initialization
	void Start ()
	{

		renderer.sortingLayerName = "Background";
		renderer.sortingOrder = -2;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
