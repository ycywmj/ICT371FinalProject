using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

    Vector3 offset;

	void Start()
	{
		offset = transform.position - target.position;
	}

    void LateUpdate()
    {
        // calculate the desired movement position and rotation angle
        Vector3 desired_pos = target.transform.position + offset;
        float desired_angle = target.transform.eulerAngles.y;
        // calculate the actual movement and rotation
        Quaternion rot = Quaternion.Euler(70, desired_angle, 0);
        // set the camera
        transform.position = target.transform.position - (rot * offset);
        transform.LookAt(target.transform);
    }

}
