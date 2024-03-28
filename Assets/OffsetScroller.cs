using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {

	public float scrollSpeed;
	public string TextureName = "_EmitMap";
	private Vector2 savedOffset;

	Renderer ren;

	void Start () {
		ren = GetComponent<Renderer> ();

		savedOffset = ren.sharedMaterial.GetTextureOffset( TextureName);
		}

	void Update () {
		float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (x, savedOffset.y);
		ren.sharedMaterial.SetTextureOffset (TextureName, offset);
	}

	void OnDisable () {
		ren.sharedMaterial.SetTextureOffset (TextureName, savedOffset);
	}
}