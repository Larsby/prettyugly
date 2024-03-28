using UnityEngine;
using System.Collections;

public class EyeMovements : MonoBehaviour
{
	public GameObject leftEye;
	public GameObject RightEye;
	Vector3 leftEyeStandardPos;
	Vector3 rightEyeStandardPos;
	// Use this for initialization
	enum MovementState
	{
		LEFT,
		RIGHT,
		NORMAL,
		UP}

	;

	MovementState state;

	void Start ()
	{
		leftEyeStandardPos = leftEye.transform.localPosition;
		rightEyeStandardPos = RightEye.transform.localPosition;
		state = MovementState.NORMAL;
		//	MoveToTop ();
	}

	public void MoveToLeft ()
	{
		if (state != MovementState.LEFT) {
			iTween.MoveTo (RightEye, iTween.Hash ("position", new Vector3 (-0.17f, 0.061f, 0.0f), "time", 1.0f, "islocal", true));
			iTween.MoveTo (leftEye, iTween.Hash ("position", new Vector3 (0.27f, 0.073f, 0.0f), "time", 1.0f, "islocal", true));
			state = MovementState.LEFT;
		}
	}

	public void MoveToRight ()
	{
		if (state != MovementState.RIGHT) {
			iTween.MoveTo (RightEye, iTween.Hash ("position", new Vector3 (-0.3f, 0.061f, 0.0f), "time", 1.0f, "islocal", true));

			iTween.MoveTo (leftEye, iTween.Hash ("position", new Vector3 (0.13f, 0.073f, 0.0f), "time", 1.0f, "islocal", true));	
			state = MovementState.RIGHT;
		}
	}

	public void MoveToNormal ()
	{
		if (state != MovementState.NORMAL) {
			iTween.MoveTo (RightEye, iTween.Hash ("position", rightEyeStandardPos, "time", 1.0f, "islocal", true));

			iTween.MoveTo (leftEye, iTween.Hash ("position", leftEyeStandardPos, "time", 1.0f, "islocal", true));
			state = MovementState.NORMAL;
		}
	}

	public void MoveToTop ()
	{
		if (state != MovementState.UP) {
			iTween.MoveTo (RightEye, iTween.Hash ("position", new Vector3 (rightEyeStandardPos.x, 0.03f, 0.0f), "time", 1.0f, "islocal", true));

			iTween.MoveTo (leftEye, iTween.Hash ("position", new Vector3 (leftEyeStandardPos.x, 0.03f, 0.0f), "time", 1.0f, "islocal", true));	
			state = MovementState.UP;
		}
	}


}
