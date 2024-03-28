using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RenderImageTest : MonoBehaviour
{
	public Shader curShader;
	public float grayScaleAmount = 0.0f;
	private Material curMaterial;

	Material material {
		get {
			if (curMaterial == null) {
				curMaterial = new Material (curShader);
				curMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return curMaterial;
		}
	}
	// Use this for initialization
	void Start ()
	{
	
	}

	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{

		if (curShader != null) {
			material.SetFloat ("_Alpha", grayScaleAmount);
			Graphics.Blit (sourceTexture, destTexture);
		} else {
			Graphics.Blit (sourceTexture, destTexture);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
