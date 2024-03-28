using UnityEngine;
using System.Collections;



public interface IWeight
{

	void ChangeWeight ();

	void AlreadyEaten ();
}

/**
 * Mkay, this script messes with the hierarchy. We want the position of all the limbs and face to follow the body that we will scale up
 * we do this by first saving a copy of all the children of the main Pretty game object THEN we detactch em from the Pretty game object
 * and then we attatch them to the body and we scale the body so the limbs get the correct position (and wrong scale)
 * so then we detatch the sprite limbs from the body sprite and set localscale to 1.0 then we reattach them back to the main Pretty game object.
 * 
 * **/

public class Fatness : MonoBehaviour, IWeight
{

	public GameObject body;
	private  Transform[] children;
	private Transform[] childrenOfBody;
	public int timeInSecondsToGrow = 20;
	public float scale = 0.8f;
	Rigidbody2D rigid;
	private bool doneHatch = false;
	public  bool isBaby = false;
	float pulseScale;
	bool downPulse = true;
	public GameObject particleSystem;
	private IWeight eatParticleSystem;
	private bool doShrink = false;
	private bool inactive = false;
	void IWeight.ChangeWeight ()
	{
		if (inactive) return;
		Grow ();
		if (eatParticleSystem != null) {
			eatParticleSystem.ChangeWeight ();
		}
	}

	void IWeight.AlreadyEaten ()
	{
		if (inactive) return;
		if (eatParticleSystem != null) {
			eatParticleSystem.AlreadyEaten ();
		}
	
	}

	public void ReInit ()
	{
		int i = 0;
		if (children == null) return;
		foreach (Transform t in transform) {
			if (t == null) inactive = true;
			if (inactive) return;
			children [i++] = t;
		}

		i = 0;  
		childrenOfBody = new Transform[body.transform.childCount];
		foreach (Transform t in body.transform) {
			childrenOfBody [i++] = t;
		}

		DetatchAllChildrenExceptBoxCaster (transform);
		//	float delay = (float)Random.Range (5, 10);
		Grow ();
	}

	void DetatchAllChildrenExceptBoxCaster (Transform trans)
	{
		foreach (Transform t in trans) {
			if (t.tag.Equals ("BoxCaster") == false) {
				t.parent = null;
			}
		}
	}

	void Start ()
	{
		rigid = GetComponent<Rigidbody2D> ();
		children = new Transform[ transform.childCount ];
		eatParticleSystem = particleSystem.GetComponent<StartStopParticleSystem> ();
		ReInit ();

		//	InvokeRepeating ("Grow", delay, timeInSecondsToGrow);
	}



	void doSpawn ()
	{
		GameObject spriteObject = (GameObject)Instantiate (Resources.Load ("PrettyBaby"));
	
		//	Vector3 randomRotation = new Vector3 (0.0f, 0.0f, Random.Range (1, 280));
		float randomx = Random.Range (0.05f, 0.25f);
		float randomy = Random.Range (0.05f, 0.25f);
		spriteObject.GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity;
		if (Random.Range (0, 1) == 0) {
			spriteObject.transform.position = new Vector3 (body.transform.position.x + randomx, body.transform.position.y - randomy, 0.0f);
		} else {
			spriteObject.transform.position = new Vector3 (body.transform.position.x - randomx, body.transform.position.y + randomy, 0.0f);
				
		}
	}

	IEnumerator Spawn (float seconds)
	{

		yield return new WaitForSeconds (seconds);
		doSpawn ();
	}

	public void Hatch ()
	{
		if (inactive) return;
		float numberOfBabies = Random.Range (1.0f, 2.0f);
		doShrink = true;
		for (float i = 0; i <= numberOfBabies; i++) {
			StartCoroutine (Spawn (Random.Range (0.1f, 0.3f)));
		}
	
	


	}

	public void ManipulateScale ()
	{
		if (this.enabled == false) return;
		if (children == null)
			return;
		if (inactive) return;
		foreach (Transform t in children) {
			if (t != body.transform) {
				if (t.tag.Equals ("BoxCaster") == false) {
					t.parent = body.transform;
				}

			}
		}
		body.transform.localScale = new Vector3 (scale, scale, 1.0f);

		body.transform.DetachChildren ();

		gameObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);        
		foreach (Transform t in children) {  
			if (t != body.transform) {
				if (t.tag.Equals ("BoxCaster") == false) {
					t.localScale = new Vector3 (1.0f, 1.0f, 1.0f);   
					t.parent = gameObject.transform;
				}
			}
		}
		body.transform.parent = gameObject.transform;

		foreach (Transform t in childrenOfBody) {
			t.parent = body.transform;
		}
		body.transform.parent = gameObject.transform;
	}

	public void Shrink ()
	{
		if (inactive) return;
		if (doShrink) {
			ManipulateScale ();
			if (scale > 1.0f) {
				scale -= 0.004f;
			} else {
				doShrink = false;
			}
		}

	}

	public void Grow ()
	{
		if (this.enabled == false) return;

		if (inactive) return;
		if (doneHatch)
			return;
		ManipulateScale ();
		
		if (scale > 2.0f) {
			if (doneHatch == false) {

				if (isBaby) {
					body.GetComponent<GrowUp> ().enabled = true;
					body.GetComponent<GrowUp> ().doChange ();
				} else {
					Hatch ();
				}
				doneHatch = true;
				scale = 1.0f;
				rigid.angularDrag = scale;
				rigid.drag = scale;

			}
			;
		} else {
			if (isBaby) {
				scale += 0.004f;

			} else {
				scale += 0.002f;

			}
			//	rigid.angularDrag = scale;
			//	rigid.drag = scale;

		}


	}
	// Update is called once per frame
	void Update ()
	{ 			if (this.enabled == false) return;

		if (inactive) return;
		if (doShrink) {
			Shrink ();
		}
		//	body.transform.localScale = new Vector3 (body.transform.localScale.x + body.transform.localScale.x * 0.1f, body.transform.localScale.y + body.transform.localScale.y * 0.1f, 1.0f);
	
	}
}
