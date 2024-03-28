using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRigid : MonoBehaviour
{
	Rigidbody2D rb;

	public float moveUpspeed =  0;
	public float rotateZspeed = 0;
	public bool disableIfNoJoints = true;

	float angleTimer = 0;
	float orgRot;

	SpringJoint2D[] joints;

	void Awake()
    {
		rb = GetComponent<Rigidbody2D>();
		orgRot = transform.rotation.eulerAngles.z;

		joints = GetComponentsInChildren<SpringJoint2D>();
    }

    void Update()
    {
		if (moveUpspeed < -0.01 || moveUpspeed > 0.01)
		{
			if (rb)
				rb.AddForce(rb.transform.up * Time.deltaTime * moveUpspeed);
			else
				transform.position += (transform.up * Time.deltaTime * moveUpspeed / 1000);
		}

		if (rotateZspeed < -0.01 || rotateZspeed > 0.01)
		{
			angleTimer += Time.deltaTime;
			if (rb)
				rb.MoveRotation(orgRot + angleTimer * rotateZspeed);
			else
				transform.rotation = Quaternion.Euler(0,0, orgRot + angleTimer * rotateZspeed);
		}

		if (disableIfNoJoints)
		{
			bool isConnected = false;
			for (int i = 0; i < joints.Length; i++)
				if (joints[i] != null)
					if (joints[i].connectedBody != null)
						isConnected = true;

			if (!isConnected)
			{
				moveUpspeed = rotateZspeed = 0;
			}
		}

	}
}
