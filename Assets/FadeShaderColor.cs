using UnityEngine;
using System.Collections;

public class FadeShaderColor : MonoBehaviour
{
	public Color startColor;
	public Color endColor;
	public float duration;
	private Material mat;
	public float speed = 1.0F;
	private float startTime;
	private bool enabled;
	int objectCount = 0;
	 const int UINT16_MAX =  65535;
	bool combine = true;
	Color color;
	static public bool firstCombine = true;
	private bool returned = false;
	bool CombineMeshesFunc ()
	{
		if (transform.parent == null)
			return false;
	//	CombineMeshes meshManager = transform.parent.gameObject.GetComponent<CombineMeshes> ();
		//meshManager = CombineMeshes.instance;
		mat.color = endColor;
		if(firstCombine) {
			firstCombine = false;
		CombineMeshes.instance.SetMaterial (mat);

		}
		Matrix4x4 transformMatrix = transform.parent.transform.worldToLocalMatrix;
		var meshFilter = CombineMeshes.instance.GetCurrentMeshFilter ();
			

		var backMeshFilter = GetComponent<MeshFilter> ();

		CombineInstance[] combine = new CombineInstance[2];

		combine [1].mesh = meshFilter.sharedMesh;
		if (combine [1].mesh == null) {
			//combine [1].mesh = meshFilter.mesh;
		}

		combine [1].transform = transformMatrix * meshFilter.transform.localToWorldMatrix;

		combine [0].mesh = backMeshFilter.mesh;
		combine [0].transform = transformMatrix * backMeshFilter.transform.localToWorldMatrix;

		long meshCount = combine [0].mesh.vertexCount + meshFilter.mesh.vertexCount;
		if (meshCount > UINT16_MAX) {
			// due to a bug in Unity the core breaks if the number of verticies exceed UINT16_MAX
			// so we need to create a new meshHolder and transfer our mesh there. 
			meshFilter = CombineMeshes.instance.GetNewMeshFilter ();
			combine = new CombineInstance[2];

			combine [1].mesh = meshFilter.sharedMesh;

			combine [1].transform = transformMatrix * meshFilter.transform.localToWorldMatrix;

			combine [0].mesh = backMeshFilter.mesh;
			combine [0].transform = transformMatrix * backMeshFilter.transform.localToWorldMatrix;
			meshCount = combine [0].mesh.vertexCount + meshFilter.mesh.vertexCount;


		} 
	

		Mesh newMesh = new Mesh ();

		meshFilter.mesh = newMesh;


		//transform.parent.gameObject.GetComponent<MeshFilter> ().mesh = newMesh;
		//if (meshFilter.mesh != null && combine [0].mesh != null && combine [1].mesh != null) {
			meshFilter.mesh.CombineMeshes (combine, true, true);
		//}
	
	
		return true;
		//transform.gameObject.active = true;
			
	}

	void Start ()
	{
		Init ();
	}

		
	public	void Init() {
		mat = GetComponent<MeshRenderer> ().material;
		mat.color = startColor;
		color = startColor;
		startTime = Time.time;
		combine = true;
		returned = false;
	}
	
	// Update is called once per frame
	void Update ()
	{ 
		if (mat.color != endColor) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / duration;
			mat.color = Color.Lerp (startColor, endColor, distCovered);
			//	color = Color.Lerp (startColor, endColor, distCovered);
		} else {
			
			if (Time.time > startTime + duration && combine) {
				//		mat.color = endColor;
				if ( CombineMeshesFunc ()  ) {
					
					combine = false;
					PooledObject p = GetComponent<PooledObject> ();
					if (p != null) {
						p.ReturnToPool ();
					} else {
						Debug.Log ("!!" + gameObject.name);
					}

				
				}
			//	combine = false;
			}
		}

	}
}
