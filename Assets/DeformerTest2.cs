using UnityEngine;
using System.Collections;
using Medvedya.SpriteDeformerTools;

public class DeformerTest2 : MonoBehaviour
{


	public Sprite sprite;
	public Material material;
	SpriteDeformerStatic mySprite;
	private SpritePoint myPoint;
	public Vector2 zeOffset = new Vector2 (0.0f, 0.0f);
	public Vector2 anchorPoint = new Vector2 (0.5f, 0.5f);

	void Start ()
	{
		mySprite = gameObject.AddComponent<SpriteDeformerStatic> ();
		mySprite.sprite = sprite;
		mySprite.material = material;
		mySprite.SetRectanglePoints ();
		CreatePoint ();

	}

	void CreatePoint ()
	{
		myPoint = new SpritePoint (anchorPoint);
		mySprite.AddPoint (myPoint);


		mySprite.UpdateMeshImmediate ();
	}

	void Update ()
	{
		/*
		myPoint.offset2d = 
				new Vector2 (Mathf.Cos (Time.time) * 0.3f, 
			Mathf.Sin (Time.time) * 0.3f);
        */
		if (myPoint.spritePosition != anchorPoint) {
			mySprite.RemovePoint (myPoint);
			CreatePoint ();

		}
		mySprite.dirty_offset = true;
		myPoint.offset2d = zeOffset;
		myPoint.normal = Vector3.left;
		float t = Mathf.PingPong (Time.time, 1);
		//	myPoint.color = Color.blue;
		mySprite.dirty_color = true;
	}
}