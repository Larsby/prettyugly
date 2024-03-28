using UnityEngine;
using System.Collections;

public class DetatchWhenJointBreak : MonoBehaviour
{
	Joint2D joint;
	bool initilized = false;
	public GameObject klibb;
	// Use this for initialization
	void Start ()
	{
		joint = GetComponent<Joint2D> ();
		if (joint != null) {
			initilized = true;
		}
			
	}

	void OnJointBreak (float breakForce)
	{
		Debug.Log ("A joint has just been broken!, force: " + breakForce);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (initilized) {
			if (joint == null && klibb != null) {
				Stretch s = klibb.GetComponent<Stretch> ();
				if (s != null) {
					s.retract = true;
				}
				//	Destroy (this);
			}
		}
	}
}
