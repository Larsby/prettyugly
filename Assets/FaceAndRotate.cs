using UnityEngine;
using System.Collections;

public class FaceAndRotate : MonoBehaviour
{
	// rules are if pusher is active then face pusher if pusher is less than 1/3 of the screen size away
	// if not it will face the closest pinkie and if no pinkie is available it will look at the closest eater.
	public float DistanceToPusherDividedByScreenSize = 0.75f;
	public float TurningTimeSpeed = 6.5f;
	float maxDistance = 0.0f;
	bool approximate = false;
	float approximateAngle;
	Rigidbody2D body;
	public Boxcaster visibleCaster;
	bool randomLooking = false;
	Character currentTarget = null;
	EyeMovements moveEyes = null;
	float previousAngle = 0.0f;
	float time;
	Vector3 previousPosition;
	bool moving = false;
	bool startRandomLook = false;

	void Start ()
	{
		body =	GetComponent<Rigidbody2D> ();
		moveEyes = GetComponent<EyeMovements> ();
		moving = false;
	}

	void FaceTarget (Vector3 target)
	{
		CancelInvoke ("RandomlyLookForSomethingToLookAt");
		Quaternion q = Quaternion.LookRotation (Vector3.forward, target - transform.position);

		if (approximate == false) {
			approximateAngle = Random.Range (0.88f, 1.0f);
			approximate = true;	
		
		}
		q.eulerAngles = new Vector3 (0.0f, 0.0f, approximateAngle * q.eulerAngles.z);
		if (previousAngle != 0.0f && previousAngle < q.eulerAngles.z) {
			if (moveEyes != null) {
				moveEyes.MoveToLeft ();
			}

		} else {
			if (moveEyes != null) {
				moveEyes.MoveToRight ();
			}

		}

		previousAngle = q.eulerAngles.z;

		body.MoveRotation (Quaternion.Slerp (transform.rotation, q, Time.deltaTime * (TurningTimeSpeed * approximateAngle)).eulerAngles.z);

	}

	void TurnToVelocityDirection ()
	{
		CancelInvoke ("RandomlyLookForSomethingToLookAt");

		Quaternion q = Quaternion.LookRotation (Vector3.forward, new Vector3 (body.velocity.x, body.velocity.y, 1.0f));
		body.MoveRotation (Quaternion.Slerp (transform.rotation, q, Time.deltaTime * TurningTimeSpeed).eulerAngles.z);
	}

	float randomAngleToLookAt;

	IEnumerator RandomlyLookForSomethingToLookAt ()
	{
		randomLooking = false;
		yield return new WaitForSeconds (Random.Range (1, 4));
		if (moving == false && randomLooking == false) {
			randomAngleToLookAt = Random.Range (-360f, 360f);
			//Debug.Log ("Started to look");
			//	iTween.RotateAdd (gameObject, new Vector3 (0.0f, 0.0f, Random.Range (-45f, 45f)), Random.Range (0.5f, 2.5f));
			randomLooking = true;
			startRandomLook = false;
		}
	}
	int frameCount = 0;
	void Update ()
	{
		frameCount++;
		if (frameCount % 5 != 0)
			return;
		if (frameCount > 100)
			frameCount = 0;
		Vector3 target = Vector3.zero;
		Character chosenCharacter = null;
		if (GameManager.instance.IsReady () == false) {

			return;
		}
		if (Vector3.Distance (previousPosition, transform.position) > 0.002) {
			time = Time.time + 1;
		}
		if (time > Time.time) {
			moving = true;
			previousPosition = transform.position;
			TurnToVelocityDirection ();
			return;
		} else {
			moving = false;
		}
		previousPosition = transform.position;
		/*
		if (body.velocity != Vector2.zero) {
			TurnToVelocityDirection ();
			return;

		}*/

		if (GameManager.instance.IsPlayerAiming () == true) {
			//if (true) {		

			foreach (GameObject obj in visibleCaster.hitGO) {
				if (obj.tag.Equals ("Blob")) {
					target = obj.transform.position;
					break;
				}
			}
			if (target != Vector3.zero) {
				
				FaceTarget (target);

			}
		} else {
			if (moving == false && randomLooking) {
				Quaternion q = Quaternion.LookRotation (Vector3.forward, new Vector3 (body.velocity.x, body.velocity.y, 1.0f));
				body.MoveRotation (Quaternion.Slerp (transform.rotation, q, Time.deltaTime * (TurningTimeSpeed * randomAngleToLookAt)).eulerAngles.z);
			}
			Character pinky = null;
			Character eater = null;
			foreach (GameObject obj in visibleCaster.hitGO) {
				float distance = Vector2.Distance (transform.position, obj.transform.position);
				if (obj.tag.Equals ("Ugly")) {
					if (pinky == null) {
						pinky = new Character ();
						pinky.distance = distance;
						pinky.target = obj.transform.position;
						pinky.target_obj = obj;
					} else if (distance < pinky.distance) {
						pinky.distance = distance;
						pinky.target = obj.transform.position;
						pinky.target_obj = obj;
					}
				} else if (obj.tag.Equals ("Pretty")) {
					if (eater == null) {
						eater = new Character ();
						eater.distance = distance;
						eater.target = obj.transform.position;
						eater.target_obj = obj;
					} else if (distance < eater.distance) {
						eater.distance = distance;
						eater.target = obj.transform.position;
						eater.target_obj = obj;
					}
				}


			}
			if (pinky != null) {
				chosenCharacter = pinky;
			} else if (eater != null) {
				chosenCharacter = eater;
			}
			if (chosenCharacter != null) {
				randomLooking = false;
				/*
				 * 
				 * lägg till en current target och byt inte förrän en annan target är närmmare. nu går jag hem.
				 * 
				 * 	*/

				if (currentTarget == null) {
					currentTarget = chosenCharacter;
				}
				if (currentTarget.target_obj != chosenCharacter.target_obj && Vector2.Distance (transform.position, currentTarget.target_obj.transform.position) < Vector2.Distance (transform.position, chosenCharacter.target_obj.transform.position)) {
					currentTarget = chosenCharacter;
				}

				FaceTarget (currentTarget.target_obj.transform.position);
			} else {
				if (startRandomLook == false) {
					startRandomLook = true;
					StartCoroutine ("RandomlyLookForSomethingToLookAt");
				}

			
			}
		

		}

	}


}
