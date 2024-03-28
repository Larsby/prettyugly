using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TakeScreenShot : MonoBehaviour
{

	// Use this for initialization
	public bool capture = false;


	void Start ()
	{
		

	}

	void ShowUI ()
	{
		GameManager.instance.ShowUI ();
	}

	void Capture ()
	{
		GameManager.instance.HideUI ();
		int index = SceneManager.GetActiveScene ().buildIndex;
	//	Application.CaptureScreenshot (Application.dataPath + "/LevelIcons/" + index + ".png");
		Invoke ("ShowUI", 2);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (capture) {
			Capture ();
			capture = false;
		}
	}
}
