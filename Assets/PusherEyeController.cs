using UnityEngine;
using System.Collections;

public class PusherEyeController : MonoBehaviour
{
	public GameObject[] eyelids;
	public bool blink = false;
	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("StartBlinking");
	}

	public IEnumerator StartBlinkingWithRange (float maxRange)
	{
		yield return new WaitForSeconds (Random.Range (2.0f, maxRange));
		StartCoroutine ("Blink", (Random.Range (0, eyelids.Length)));

	
	}

	public IEnumerator StartBlinking ()
	{
		return StartBlinkingWithRange (4.0f);
	}

	public void OpenAll ()
	{
		StopCoroutine ("Blink");
		StopCoroutine ("StartBlinking");
		SetRenderState (false);
		StartCoroutine ("StartBlinking");
	}

	public void SetRenderState (bool state)
	{
		foreach (GameObject lid in eyelids) {
			lid.GetComponent<Renderer> ().enabled = state;
		}	
	}



	public void CloseAll ()
	{
		SetRenderState (true);
		StopCoroutine ("Blink");

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (blink) {
			blink = false;
			CloseAll ();

		}
	}
	/*
	public IEnumerator Open (GameObject eyelid)
	{
	  
	}
*/
	public IEnumerator Blink (int lidIndex)
	{
		yield return new WaitForSeconds (Random.Range (0.0f, 4.2f));
		eyelids [lidIndex].GetComponent<Renderer> ().enabled = true;
		yield return new WaitForSeconds (Random.Range (0.2f, 0.6f));
		eyelids [lidIndex].GetComponent<Renderer> ().enabled = false;	

		StartCoroutine ("Blink", (Random.Range (0, eyelids.Length)));
	}

}
