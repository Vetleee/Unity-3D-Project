using UnityEngine;
using System.Collections;

public class fpscontroll : MonoBehaviour 
{
	public float movementSpeed;
	public float sprintSpeed;
	public float maxMovementSpeed;
	public float mousesens = 5.0f; 


	float verticalRoation = 0;
	public float upDownRange = 60.0f; 


	float verticalVelocity = 0;

	public float jumpSpeed = 20.0f;
	public float powerjump = 120.0f; 

	public float powerSpeed = 40;

	public float jumpPowerGems = 0; 

	public Rigidbody rb; 
	public float thrust;

	public bool canMove;

	bool isLocked;

	bool CursorLockVar;

	// Use this for initialization
	void Start () 
	{
		//Screen.lockCursor = true; 
		rb = GetComponent<Rigidbody> ();

		//SetCursorLock (true);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = (false);
		CursorLockVar = (true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!canMove) 
		{
			return;
		}

		//if (Input.GetKeyDown ("escape")) {
			//Screen.lockCursor = false;
		//}


		//MENU INTERACTION AND CURSOR UNLOCK

		if (Input.GetKeyDown ("escape") && !CursorLockVar) 
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = (false);
			CursorLockVar = (true);

			//set mouse sens to 0

			canMove = true;

		}

		else if(Input.GetKeyDown("escape") && CursorLockVar)
		{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = (true);
				CursorLockVar = (false);

			canMove = true;
		}
	

		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = (false);


		CharacterController cc = GetComponent<CharacterController>();

		//rotation
		float rotLeftRight = Input.GetAxis("Mouse X") * mousesens;
		transform.Rotate (0, rotLeftRight, 0);

		verticalRoation -= Input.GetAxis("Mouse Y") * mousesens;
		verticalRoation = Mathf.Clamp (verticalRoation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRoation, 0, 0);

		//movement

			
		float forwardspeed = Input.GetAxis("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		if ( cc.isGrounded && Input.GetButton ("Jump")) 
		{
			verticalVelocity = jumpSpeed; 
		}

		//sprint
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			movementSpeed = movementSpeed + sprintSpeed;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			movementSpeed = movementSpeed - sprintSpeed;
		}


		if (cc.isGrounded && jumpPowerGems > 0 && Input.GetButtonDown ("secondjump") && forwardspeed > 0) {
			//rb.AddForce(transform.forward * thrust);
			//forwardspeed = forwardspeed * 2; 
			verticalVelocity = powerjump;
			jumpPowerGems--; 
			verticalVelocity = 30;
			Debug.Log("powerjumping");

		}
		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardspeed );

		speed = transform.rotation * speed; 

		cc.Move( speed * Time.deltaTime );

		//if (!cc.isGrounded && Input.GetButtonDown ("Jump")) 
		//{
		//	verticalVelocity = jumpSpeed/2; 
		//}


		
	
}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Ups")) 
		{
			other.gameObject.SetActive (false);
			jumpPowerGems ++;


		}
	}
}