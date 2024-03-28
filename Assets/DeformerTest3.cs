using UnityEngine;
using System.Collections;
using Medvedya.SpriteDeformerTools;

public class DeformerTest3 : MonoBehaviour
{
	struct BlendVal
	{
		int index;
		float value;
	}

	BlendVal current;
	BlendVal old;
	SpriteDeformerBlendShape blender;
	public int index = 0;
	// Use this for initialization
	void Start ()
	{
		blender = GetComponent<SpriteDeformerBlendShape> ();
		current = new BlendVal ();

		old = new BlendVal ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//float v = Mathf.Lerp (0.01f, 1.0f, 5f);
		blender.SetBlendShapeWeight (index, 1.0f);
	}
}
