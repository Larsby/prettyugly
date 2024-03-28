using UnityEngine;
using System.Collections;
using System.Linq;

[ExecuteInEditMode]
public class Boxcaster : MonoBehaviour
{
	public Rect Box;
	public Vector2 Direction;
	public float Distance;
	public RaycastHit2D[] hits;
	public GameObject[] hitGO;
	public RaycastHit2D FirstHit;
	public LayerMask mask;
	float updateRatePerSec = 1.0f;
	float deltaTime = 0.0f;
	bool started = false;

	void OnDrawGizmos ()
	{
		if (hits != null) {
			foreach (var h in hits) {
				Gizmos.color = Color.white;
				Gizmos.DrawCube (h.collider.transform.position, Vector2.one * 0.16f);            
			}
		}
		if (FirstHit.collider != null) {
			Gizmos.color = Color.cyan;
			Gizmos.DrawCube (FirstHit.collider.transform.position, Vector2.one * 0.16f);
		}
		Gizmos.color = Color.green;
		Gizmos.matrix = Matrix4x4.TRS ((Vector2)this.transform.position, this.transform.rotation, Vector3.one);
		Gizmos.DrawWireCube (Vector2.zero, Box.size);
		Gizmos.matrix = Matrix4x4.TRS ((Vector2)this.transform.position + (Direction.normalized * Distance), this.transform.rotation, Vector3.one);
		Gizmos.DrawWireCube (Vector2.zero, Box.size);
		Gizmos.color = Color.cyan;
		Gizmos.matrix = Matrix4x4.TRS ((Vector2)this.transform.position, Quaternion.identity, Vector3.one);
		Gizmos.DrawLine (Vector2.zero, Direction.normalized * Distance);
	}

	void Start ()
	{
		started = false;
		hitGO = new GameObject[0]{ };
	
	}

	IEnumerator Scan ()
	{
		yield return new WaitForSeconds (0.3f);
		hits = Physics2D.BoxCastAll (
			(Vector2)gameObject.transform.position, 
			Box.size, 
			this.transform.eulerAngles.z, 
			Direction, 
			Distance, 
			mask);


		hitGO = hits.Where (x => x.collider != null).Select (x => x.collider.gameObject).ToArray ();
		StartCoroutine (Scan ());
	}

	void Update ()
	{
		if (GameManager.instance != null && GameManager.instance.IsReady () && started == false) {
			started = true;
			StartCoroutine (Scan ());
		}
	}
}

