using UnityEngine;
using System.Collections;

public class PurrWhenTouched : MonoBehaviour
{

	HideHead hide;
	Vector3 original;
	// Use this for initialization
	void Start ()
	{
		hide = GetComponentInChildren<HideHead> ();
		original = transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnMouseUp ()
	{   

		iTween.ScaleTo (gameObject, new Vector3 (original.x, original.y, original.z), 1f);
		hide.Show ();
		
	}

	void OnMouseDown ()
	{  
		iTween.ScaleTo (gameObject, new Vector3 (original.x, 1.0f, original.z), 1f);

		hide.Hide ();
	}
}
