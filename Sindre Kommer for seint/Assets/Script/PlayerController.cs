using UnityEngine;
using System.Collections;

// Copyright Vetle Bugge 2016

public class PlayerController : MonoBehaviour {

    private CharacterController controller;
    private PickUpAndInspect pickUp;
    private Camera fpCam;
    public float walkSpeed;

    private bool isInspecting;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        fpCam = Camera.main;
        pickUp = GetComponent<PickUpAndInspect>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!isInspecting)
                MoveCharacter();


        MoveCamera();

        if (Input.GetMouseButtonDown(0))
        {
            isInspecting =  pickUp.RaycasToObject(fpCam);
        }

       

	}

    void MoveCharacter()
    {
        //Gets input from directions and stores them

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");      

        //Find target direction in relation to the camera
        Vector3 moveDirection = fpCam.transform.forward * vertical + fpCam.transform.right * horizontal;

        //Move the player
        controller.SimpleMove(moveDirection * walkSpeed);
    }

    void MoveCamera()
    {
        //Get input from mouse and store them

        float mouseHorizontal = Input.GetAxisRaw("Mouse Y");
        float mouseVertical = Input.GetAxisRaw("Mouse X");

        if (!isInspecting)
        {
            //Rotate camera horizontally
            fpCam.transform.Rotate(Vector3.right, mouseHorizontal * -1);
            //Rotate the camera vertically
            fpCam.transform.Rotate(Vector3.up, mouseVertical, Space.World);
        }else
        {
            pickUp.RotateInspected(mouseHorizontal, mouseVertical);
        }
       
    }

   




}
