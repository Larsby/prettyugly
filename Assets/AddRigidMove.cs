using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRigidMove : MonoBehaviour
{
	public GameObject[] addMoveRigidTo;
	public float moveUpspeed = 0;
	public float rotateZspeed = 0;
	public bool disableIfNoJoints = false;

	bool wasUsed = false;

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (wasUsed)
			return;

		if (collision.collider.gameObject.tag == "Blob")
		{
			wasUsed = true;

			foreach (GameObject g in addMoveRigidTo)
			{
				MoveRigid mr = g.AddComponent<MoveRigid>();
				mr.moveUpspeed = moveUpspeed;
				mr.rotateZspeed = rotateZspeed;
				mr.disableIfNoJoints = disableIfNoJoints;
			}

			// print("mAJS");
		}
	}

	/*
	private void OnTriggerEnter2D(Collider2D collision)
	{
	} */
}
