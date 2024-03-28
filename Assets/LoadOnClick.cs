using UnityEngine;
using System.Collections;



public class LoadOnClick : MonoBehaviour
{

	public GameObject loadingImage;

	public void LoadScene (int level)
	{
		//	loadingImage.SetActive(true);
		if (GameManager.instance != null)
			GameManager.instance.HideUI ();
		Application.LoadLevel (level);
		if (MessureArea.instance != null) {
			MessureArea.instance.Calculate ();
		}
	}
}

