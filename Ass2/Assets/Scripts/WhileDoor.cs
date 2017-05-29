using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileDoor : MonoBehaviour {

	Vector3 pivot;
	float ang = 0;

	// Use this for initialization
	void Start () {
		pivot = new Vector3(2.298f, 0, 4.525f);
	}

	// Update is called once per frame
	void Update () {
		if (ang < 90 && GameVariables.p2DoorOpen)
		{
			transform.RotateAround(pivot, Vector3.up, 0.3f);
			ang += 0.3f;
		}
	}
}
