using UnityEngine;
using System.Collections;

public class RandomMeshFilter : MonoBehaviour
{
	public Mesh[] list;
	// Use this for initialization
	void Start ()
	{
		GetComponent<MeshFilter> ().mesh = list [(int)Random.Range (0.0f, (float)list.Length)];
	}
	

}
