using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountMoves : MonoBehaviour {

	public int maxMoves = 10;
	// Use this for initialization
	private int counter;
	void Start () {
		counter = maxMoves;
	}

	public int GetMoves() {
		return counter;
	}
public	bool CountDown() {
		if (counter > 0) {
			counter--;
			return true;
		}
		return false;
	}
	public bool MovesAvailable() {
		return counter == 0 ? false : true;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
