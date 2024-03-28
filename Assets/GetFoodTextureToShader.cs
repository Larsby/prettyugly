using UnityEngine;
using System.Collections;

public class GetFoodTextureToShader : MonoBehaviour
{
	GameObject messure = null;
	MeshRenderer textureRenderer = null;

	bool materialSet = false;
	// Use this for initialization
	void Start ()
	{
		messure = GameObject.Find ("Messure");
		materialSet = false;

	}
	/*
	void OnLevelWasLoaded (int level)
	{
		if (messure != null && materialSet == false) {
			textureRenderer = GetComponent<MeshRenderer> ();
			Material[] materials = textureRenderer.materials;

			if (textureRenderer.material.mainTexture != null) {
				GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", Resources.Load ("Graphics/bg2") as Texture);
				materialSet = true;
			}
		}

	}
	*/
	// Update is called once per frame
	void Update ()
	{  
		
	}
}
