using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]

public class CombineMeshes : MonoBehaviour
{
	public bool useCombine = false;
	public int limit = 600;
	GameObject currentMeshHolderObject;
	MeshFilter currentMeshFilter = null;
	private int combines = 0;
	public static  CombineMeshes instance = null;
	void Awake() {

		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);    

	//	DontDestroyOnLoad (gameObject);
	}
	void Start ()
	{
		currentMeshHolderObject = CreateMeshHolder ();
	}
	GameObject CreateMeshHolder() {
		
		GameObject obj = new GameObject ("MeshHolder");
		//obj.transform.parent = this.gameObject.transform;
	MeshRenderer rend = obj.AddComponent<MeshRenderer> ();
		obj.AddComponent<MeshFilter> ();
		//DontDestroyOnLoad (obj);
		//rend.enabled = false;
		return obj;
	}

	public void Clear ()
	{
		
	}
	public void SetMaterial(Material m) {
		if (currentMeshHolderObject == null) {
			currentMeshHolderObject = CreateMeshHolder ();
		}
		currentMeshHolderObject.GetComponent<MeshRenderer> ().material = m;
	}
	public MeshFilter GetCurrentMeshFilter() {
		if (currentMeshFilter == null) {
			if (currentMeshHolderObject == null) {
				currentMeshHolderObject = CreateMeshHolder ();
			}
			currentMeshFilter = currentMeshHolderObject.GetComponent<MeshFilter> ();
		}
		return currentMeshFilter;
	}
	public MeshFilter GetNewMeshFilter() {
		currentMeshHolderObject = CreateMeshHolder ();
		currentMeshFilter = null;
		return GetCurrentMeshFilter ();
	}




}