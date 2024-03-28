using UnityEngine;
using System.Collections;

public class ShapeManipulator : MonoBehaviour
{
	public Component[] bends;
	public float angle;
	public Vector3 startAmount;
	public Vector3 endAmount;
	private float startTime;
	private float journeyLength;
	public float speed = 0.001F;
	public bool start = false;

	void Start ()
	{
		startAmount = new Vector3 (-60f, -20f, 0f);
		endAmount = new Vector3 (60f, 20f, 0f);

		startTime = Time.time;
		journeyLength = Vector3.Distance (startAmount, endAmount);

		bends = GetComponentsInChildren<MegaBulge> ();
		/*
		bends = new Component[childs.Length + 1];
		bends [0] = GetComponent<MegaBend> ();
		int i = 1;
		foreach (MegaBend bend in childs) {
			bends [i++] = bend;
		}
*/
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (start) {
			//start = false;
			startTime = Time.time;
			journeyLength = Vector3.Distance (startAmount, endAmount);
		
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;

			Vector3 lerp = Vector3.Lerp (startAmount, endAmount, fracJourney);
			//Debug.Log ("" + lerp.x);
			foreach (MegaBulge bend in bends) {
			
				bend.Amount = lerp;
	
			}
		}
	}
}
