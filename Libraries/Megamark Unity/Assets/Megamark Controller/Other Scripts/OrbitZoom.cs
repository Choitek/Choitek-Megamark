using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitZoom : MonoBehaviour {

	public Transform orbit;
	public Transform cam;

	public float orbitSpeed;
	public float zoomSpeed;
	public float orbitMin;
	public float orbitMax;
	public float zoomMin;
	public float zoomMax;

	private Vector3 angle;
	private float zoom;

	void Start () {
		angle = orbit.eulerAngles;
		zoom = -Vector3.Distance (cam.position, orbit.position);
	}

	void Update () {
		// Zoom 
		zoom += Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;
		if (zoom < zoomMin) { zoom = zoomMin; }
		if (zoom > zoomMax) { zoom = zoomMax; }
		cam.localPosition = new Vector3 (0, 0, zoom);
		// Orbit
		if (Input.GetMouseButton (0)) {
			angle.x -= Input.GetAxis ("Mouse Y") * orbitSpeed;
			angle.y += Input.GetAxis ("Mouse X") * orbitSpeed;
			if (angle.x < orbitMin) { angle.x = orbitMin; }
			if (angle.x > orbitMax) { angle.x = orbitMax; }
			orbit.eulerAngles = angle;
		}
	}
}
