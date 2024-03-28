using UnityEngine;

public class SlimeStretchChecker : MonoBehaviour
{
	public GameObject other;
	public float breakDistance = 9f;
	float realBreakDistance = -1.0f;
	public LineRenderer lr;
	public SpringJoint2D s2d;
	private bool done = false;
    void Start() {

		float dist = Vector3.Distance(transform.position, other.transform.position);
		realBreakDistance = dist * 2.8f;
		Debug.Log("bre" + realBreakDistance);
	}

	GameObject FindGameObjectWithTag(GameObject root, string tag)
	{
		foreach (Transform t in root.transform)
		{
			GameObject o = t.gameObject;
			if (o.CompareTag(tag))
			{
				return o;
			}
		}
		return null; ;
	}

    void Update()
    {
		if (done)
			return;

		if (other == null) {
			print("other object destroyed, deleted slimestretcher");
			Destroy(this);
		}
		float dist = Vector3.Distance(transform.position, other.transform.position);
		//print(dist);

		if (dist > realBreakDistance)
		{
			done = true;
			lr.enabled = false;
			s2d.enabled = false;
			//Destroy(lr);
			//Destroy(s2d);
			Sick sick = gameObject.GetComponent<Sick>();
			if (sick)
			{
				Destroy(sick);
				GameObject body = FindGameObjectWithTag(gameObject, "PrettyBody");

				if (body)
				{
					SpriteRenderer rd = body.GetComponent<SpriteRenderer>();
					rd.color = Color.white;
				}
				else
				{
					SpriteRenderer rd = gameObject.GetComponent<SpriteRenderer>();
					if (rd)
					{
					//	rd.color = Color.white;

					}
				}

			}
		}

		float lw = (realBreakDistance - dist) / 6f;
		if (lw > 0.7f) lw = 0.7f;

		//lr.widthMultiplier = dist;
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(0.0f, lw);
		curve.AddKey(1.0f, lw);

		lr.widthCurve = curve;


		lr.startWidth = lw;
		lr.endWidth = lw;
		if(done) {
			Destroy(this);
		}

	}
}
