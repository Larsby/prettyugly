using UnityEngine;
using System.Collections;

public class DoSphoreWhenHit : MonoBehaviour
{

	public float multiplier = 1;

	private ParticleSystem[] m_Systems;
	public bool toggle = false;
	private bool do_enabled = true;
	private bool started = false;

	

	private void Start ()
	{
		m_Systems = GetComponentsInChildren<ParticleSystem> ();
		SetEnabled (false);
	}

	public void SetEnabled (bool do_enable)
	{
		if (do_enable && started == false) {
			gameObject.transform.GetChild (0).gameObject.SetActive (true);
			started = true;
			m_Systems = GetComponentsInChildren<ParticleSystem> ();
		}
		//	m_Systems = GetComponentsInChildren<ParticleSystem> ();
		foreach (var system in m_Systems) {
			//	system.loop = false;

			var emission = system.emission;
		
			emission.enabled = do_enable;
		}
	}

	IEnumerator Stop ()
	{
		yield return new WaitForSeconds (2f);
		SetEnabled (false);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		SetEnabled (true);
		StartCoroutine ("Stop");
		Stop ();
	}

	void OnCollisionExit2D (Collision2D col)
	{
		//SetEnabled (false);
	}

}
