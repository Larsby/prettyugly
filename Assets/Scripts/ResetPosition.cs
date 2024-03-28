using UnityEngine;
using System.Collections;

public class ResetPosition : MonoBehaviour {
	private Transform initialTransform;
	Vector3 localPosition;
	Vector3 localRotation;
	Vector3 localScale;
	Vector3 smallScale;
	// Use this for initialization
	void Start () {
		initialTransform = transform;
		localPosition = transform.localPosition;
		localRotation = new Vector3 (transform.rotation.x, transform.rotation.y, transform.rotation.z);
		localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
		Reset (transform.parent);

	}
	public void Reset(Transform parent, bool resetAngles) {
		transform.SetParent (parent);

		transform.position = parent.position;
	
		if (resetAngles) {
			transform.localScale = localScale;
			transform.eulerAngles = localRotation;
		 }

		transform.localPosition = localPosition;
	}
	public void Reset(Transform parent) {
		Reset (parent, false);
	}
	public void setSmallScale() {
		float scalex = 0.4f;
		float scaley = 0.4f;
		Vector3 smallScale = new Vector3 (localPosition.x * scalex, localPosition.y * scaley, localPosition.z);
		transform.localPosition = smallScale;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
