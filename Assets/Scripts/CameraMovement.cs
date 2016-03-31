using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	private enum RotationAxes 
	{ 
		MouseXAndY, 
		MouseX, 
		MouseY
	}

	private RotationAxes axes;
	private float sensitivityX;
	private float sensitivityY;
	private bool canUpScroll;
	private bool canDownScroll;
	private new GameObject camera;

	void Awake()
	{
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	void Start()
	{
		axes = RotationAxes.MouseXAndY;
		sensitivityX = 5.0f;
		sensitivityY = 5.0f;
		canUpScroll = true;
		canDownScroll = true;
	}

	void Update ()
	{
		float movespeed = 35.0f;

		// Keyboard Controls
		if (Input.GetKey(KeyCode.W)) {
			transform.Translate (Vector3.forward * movespeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.S)) {
			transform.Translate (-Vector3.forward * movespeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A)) {
			transform.Translate (-Vector3.right * movespeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D)) {
			transform.Translate (Vector3.right * movespeed * Time.deltaTime);
		}

		if(camera.GetComponent<Camera>().fieldOfView >= 70.0f) {
			canUpScroll = false;
			canDownScroll = true;
		}
		if(camera.GetComponent<Camera>().fieldOfView <= 50.0f) {
			canDownScroll = false;
			canUpScroll = true;
		}

		// Scroll Wheel Zoom
		if(Input.GetAxisRaw("Mouse ScrollWheel") < 0 && canUpScroll) {
			camera.GetComponent<Camera>().fieldOfView += 2.0f;
		}
		if(Input.GetAxisRaw("Mouse ScrollWheel") > 0 && canDownScroll) {
			camera.GetComponent<Camera>().fieldOfView -= 2.0f;
		}

		if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(2)){
			float rotationY = 0F;
			
			if (axes == RotationAxes.MouseXAndY) {
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
				
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, 0.0f, 0.0f);
				
				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			} else if (axes == RotationAxes.MouseX) {
				transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
			} else {
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, 0.0f, 0.0f);
				
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}
	}
}