using UnityEngine;
using System.Collections;

public class RandomMoveEmbryo : MonoBehaviour {
	public GameObject embryo;
	public GameObject splash;
	private GameObject spriteObject;
	// Use this for initialization
	void Start () {
		doCrawl ();
	}
	void swithToProper() {
		spriteObject = (GameObject)Instantiate(Resources.Load("Ugly"));
		spriteObject.SetActive (false);
		spriteObject.transform.position = embryo.transform.position;
		spriteObject.transform.rotation = embryo.transform.rotation;
		spriteObject.SetActive (true);
		splash.SetActive (false);
	}
	void popUgly() {
		//splash.SetActive (false);
	//	embryo.transform.parent = null;
		swithToProper();
		gameObject.SetActive (false);
	}
	void doCrawl() {
		iTween.MoveBy (embryo, iTween.Hash("time", 5.0f, "y", 0.3f,"eastyp",iTween.EaseType.easeInOutElastic));
	//	gameObject.transform.Rotate(new Vector3(1.0f,1.0f,(float)Random.Range(0,360)));
		int rotate = Random.Range (0, 2);
		float z = 0.1f;
		if (rotate == 1) {
			z = -0.1f;
		}
		iTween.RotateBy (embryo, iTween.Hash ("time", 4.0f, "z",z,"delay",1.0f));
		iTween.ScaleTo(splash,iTween.Hash("time", 5.0f,"scale",new Vector3(1.0f,1.0f,1.0f)));
		iTween.ScaleTo(gameObject,iTween.Hash("time", 1.0f,"scale",new Vector3(2.0f,2.0f,2.0f),"delay",3.0f,"oncomplete","popUgly"));
		iTween.ScaleTo (embryo, iTween.Hash ("time", 1.0f, "scale", new Vector3 (1.5f, 1.5f, 1.5f), "delay", 1.6f,"oncomplete","swithToProper"));
		splash.transform.parent = null;
	}
	// Update is called once per frame
	void Update () {
			
	}
}
