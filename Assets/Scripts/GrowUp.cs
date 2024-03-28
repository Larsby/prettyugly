using UnityEngine;
using System.Collections;

public class GrowUp : MonoBehaviour
{


	public float transitionTime = 1.0f;
	public Sprite grownBody;
	public GameObject target;
	private GameObject spriteObject;
	Rigidbody2D body;


	public void doChange ()
	{
		spriteObject = (GameObject)Instantiate (Resources.Load ("Pretty_Unlit"));
		//spriteObject.GetComponent <GoFromNearestHole> ().done = true;
		//spriteObject.GetComponent <GoFromNearestHole> ().enabled = false; 
		spriteObject.transform.position = target.transform.position;
		spriteObject.GetComponent<Rigidbody2D> ().velocity = target.GetComponent<Rigidbody2D> ().velocity;
		//	spriteObject.transform.rotation = target.transform.rotation;
		spriteObject.GetComponent<SetBodySprite> ().SetSprite (grownBody);
	
		//spriteObject.transform.localPosition = gameObject.transform.parent.localPosition;

		spriteObject.SetActive (true);
		gameObject.transform.parent.gameObject.SetActive (false);
		SpriteSpawn.latestPretty = spriteObject;
	}

	void Start ()
	{
		
		//		SetBodySprite = spriteObject.GetComponent<SetBodySprite> ();

		spriteObject.SetActive (false);

		//spriteObject.transform.localPosition = gameObject.transform.localPosition;
	


	}

}
