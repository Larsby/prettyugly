using UnityEngine;
using System.Collections;

public class StretcherController : MonoBehaviour
{
	private PusherDrag drag;


	private bool active = false;
	public bool toggle;
	public GameObject pusher;
	public GameObject trashContainer;
	private GameObject[] sprites;
	private Transform[] copyOfChildren;
	private bool stretchToggle = false;
	public bool test = false;

	public bool Toggle {
		get {
			return this.toggle;
			
		}
		set {

			this.toggle = value; 
			if (this.toggle) {
				active = false;
				stretchPhase = StretchPhase.STRETCH;
			} else {
				active = false;
				stretchPhase = StretchPhase.STOP_STRETCH;
		
			}
		}
	}

	private StretchPhase stretchPhase = StretchPhase.IDLE;
	// Use this for initialization
	void Start ()
	{
		CopyChildren ();

	}

	public void CollapseLegs ()
	{
		foreach (Transform t in copyOfChildren) {
			t.GetComponent<HingeJoint2D> ().enabled = false;
			t.GetComponent<SpringJoint2D> ().enabled = false;
			t.GetComponent<Stretcher> ().enabled = t;
			SpringJoint2D[] joints = t.GetComponents<SpringJoint2D> ();
			joints [1].enabled = false;
			t.GetComponent<Stretcher> ().test = true;
			//t.GetComponent<Stretcher> ().SetStretchPhase (StretchPhase.STRETCH_ANIM);
		}
	}


	void CopyChildren ()
	{
		copyOfChildren = new Transform[transform.childCount];
		int i = 0;
		foreach (Transform t in transform) {

			copyOfChildren [i++] = t;
		}
	}

	public void SetDrag (PusherDrag d)
	{
		drag = d;

		foreach (Transform t in transform) {

		
			t.GetComponent<Stretcher> ().SetDrag (d);
		}
	}

	public void TEST ()
	{
		foreach (Transform t in copyOfChildren) {
			StretchPhase phase = t.GetComponent<Stretcher> ().GetStretchPhase ();
			if (phase == StretchPhase.IDLE) {
				t.GetComponent<Stretcher> ().SetStretchPhase (StretchPhase.DONE);
				t.GetComponent<Stretcher> ().Reset ();

				t.GetComponent<Stretcher> ().enabled = false;
				t.GetComponent<HingeJoint2D> ().enabled = false;
				t.GetComponent<SpringJoint2D> ().enabled = false;

			}
		}
	}

	public void SetStrainActive (bool strain)
	{
		foreach (Transform t in copyOfChildren) {
			SpringJoint2D[] joints = t.GetComponents<SpringJoint2D> ();
			joints [0].enabled = strain;
		}
	}

	void SetStretchActiveState (bool active)
	{
		foreach (Transform t in copyOfChildren) {
			t.GetComponent<Stretcher> ().enabled = active;
			t.GetComponent<Stretcher> ().SetStretchPhase (stretchPhase);
			t.GetComponent<HingeJoint2D> ().enabled = !active;
		
			SpringJoint2D[] joints = t.GetComponents<SpringJoint2D> ();
			foreach (SpringJoint2D joint in joints) {
				joint.enabled = !active;
			}
		}
	}

	void DisableStretchWhenDone ()
	{
		foreach (Transform t in copyOfChildren) {
			t.GetComponent<Stretcher> ().SetStretchPhase (stretchPhase);
		
		}
	
	}

	void AnimateState (bool active)
	{
		foreach (Transform t in copyOfChildren) {
			t.GetComponent<Stretcher> ().enabled = active;
			t.GetComponent<HingeJoint2D> ().enabled = !active;

		}
	}

	public void DisableStretch ()
	{

		stretchPhase = StretchPhase.STOP_STRETCH;
	}

	public void DisableStretchOnChild (GameObject child)
	{
		child.GetComponent<Stretcher> ().SetStretchPhase (StretchPhase.DONE);
		child.GetComponent<Stretcher> ().enabled = false;
		child.GetComponent<HingeJoint2D> ().enabled = true;
		SpringJoint2D[] joints = child.GetComponents<SpringJoint2D> ();
		joints [1].enabled = true;
	}

	void DisableStretchOnrChildren ()
	{
		
		foreach (Transform t in copyOfChildren) {
			StretchPhase phase = t.GetComponent<Stretcher> ().GetStretchPhase ();
			if (phase == StretchPhase.IDLE) {
				t.GetComponent<Stretcher> ().SetStretchPhase (StretchPhase.DONE);
				t.GetComponent<Stretcher> ().enabled = false;
				t.GetComponent<HingeJoint2D> ().enabled = true;
				SpringJoint2D[] joints = t.GetComponents<SpringJoint2D> ();
				joints [1].enabled = true;
				foreach (SpringJoint2D joint in joints) {
					//joint.enabled = true;
				}

			}
		}

	}

	StretchPhase GetChildrenStretchPhase ()
	{
		int childCount = copyOfChildren.Length;
		int completeCount = 0;
		foreach (Transform t in copyOfChildren) {
			t.GetComponent<Stretcher> ().EndStretch ();
			StretchPhase phase = t.GetComponent<Stretcher> ().GetStretchPhase ();
			if (phase == StretchPhase.IDLE) {
				completeCount++;
			}
		}
		if (completeCount == childCount) {

			transform.parent = pusher.transform;
		
			return StretchPhase.DONE;
		}
		return StretchPhase.STOP_STRETCH;

	}

	private float nextActionTime = 0.0f;
	public float period = 0.5f;


	void Update ()
	{
		if (stretchPhase == StretchPhase.STRETCH_ANIM) {
			if (Time.time > nextActionTime) {
				nextActionTime += period;
				//AnimateState (stretchToggle);
				stretchToggle = !stretchToggle;
			}

		}

	}

	public void ShowStretch ()
	{
		if (stretchPhase == StretchPhase.STRETCH_INIT) {
			foreach (Transform t in copyOfChildren) {
				
				t.gameObject.active = true;
			}
			stretchPhase = StretchPhase.STRETCH;
		}
	}

	public void EnableStretch ()
	{
		stretchPhase = StretchPhase.STRETCH;



	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (test) {
			test = false;
			CollapseLegs ();
		}
	
		if (stretchPhase == StretchPhase.STRETCH) {
			if (active == false) {
				active = true;
				SetStretchActiveState (active);
			}
		} else {
			
			if (active != false) {
				active = false;
				DisableStretchWhenDone ();

			}
			if (active == false && stretchPhase == StretchPhase.DONE) {
				//	DisableStretchOnrChildren ();
				stretchPhase = StretchPhase.STRETCH_ANIM;
			}
			if (active == false && stretchPhase == StretchPhase.STOP_STRETCH) {
				stretchPhase = GetChildrenStretchPhase ();
			}
		
		}
	}
}
