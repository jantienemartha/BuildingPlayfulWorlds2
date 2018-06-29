using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour{

	public Transform target;
	public Vector3 offset;

	void Update(){
		Camera.main.transform.LookAt(target.position + offset);
		Camera.main.transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X"));
		Camera.main.transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel"));
	}
}