using UnityEngine;
using System.Collections;

public class StartStopParticleSystem : MonoBehaviour, IWeight
{
	public float multiplier = 1;

	private ParticleSystem[] m_Systems;
	public bool toggle = false;
	private bool do_enabled = true;
	private bool started = false;

	void IWeight.ChangeWeight ()
	{
		Ignite ();
	}

	void IWeight.AlreadyEaten ()
	{
		Extinguish ();
	}

	private void Start ()
	{
		m_Systems = GetComponentsInChildren<ParticleSystem> ();
	}

	public void SetEnabled (bool do_enable)
	{
		if (do_enable && started == false) {
			gameObject.transform.GetChild (0).gameObject.SetActive (true);
			started = true;
			m_Systems = GetComponentsInChildren<ParticleSystem> ();
		}
		foreach (var system in m_Systems) {
			var emission = system.emission;
			emission.enabled = do_enable;
		}
	}

	public void Extinguish ()
	{
		SetEnabled (false);
	}

	public void Ignite ()
	{
		SetEnabled (true);
	}

	
	// Update is called once per frame
	void Update ()
	{ /*
		if (toggle) {
			toggle = false;
			do_enabled = !do_enabled;
			SetEnabled (do_enabled);
		}
		*/
	}
}
