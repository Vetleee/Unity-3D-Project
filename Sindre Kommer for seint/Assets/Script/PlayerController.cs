using UnityEngine;
using System.Collections;

// Copyright @Vetle Bugge 2016

public class PlayerController : MonoBehaviour {

    private CharacterController controller;
    private PickUpAndInspect pickUp;
    private Camera fpCam;

    private float horizontal;
    private float vertical;
    public float walkSpeed;

    [SerializeField]
    private string isInspecting;

    //Headbob
    public bool headbob = true;

    private Vector3 camRestingPoint;
    private Vector3 camPostion;
    private float transitionSpeed = 20f;
    [SerializeField]
    private float bobSpeed = 4.8f;
    [SerializeField]
    private float bobAmount = 0.2f;
    float bobTimer = Mathf.PI / 2;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        fpCam = Camera.main;
        pickUp = GetComponent<PickUpAndInspect>();

        camRestingPoint = fpCam.transform.localPosition;
	}	
	// Update is called once per frame
	void FixedUpdate () {

        if (isInspecting == null)
        {
            MoveCharacter();
        }
        if(headbob)
            BobHead();

        MoveCamera();
        
        if (Input.GetButtonDown("Interact"))
        {
            isInspecting =  pickUp.RaycasToObject(fpCam);
        }

        if(Input.GetButtonUp("Interact") && isInspecting == "Door")
        {
            isInspecting = null;
            pickUp.ReleaseDoor();
        }

	}

    void MoveCharacter()
    {
        //Gets input from directions and stores them
        
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");      

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

        if (isInspecting == null)
        {
            //Rotate camera horizontally
            fpCam.transform.Rotate(Vector3.right, mouseHorizontal * -1);
            //Rotate the camera vertically
            fpCam.transform.Rotate(Vector3.up, mouseVertical, Space.World);

            fpCam.transform.localPosition = camPostion;
        }else if (isInspecting == "Object")
        {
            pickUp.RotateInspected(mouseHorizontal, mouseVertical);
        }
       
    }

    void BobHead()
    {
        camPostion = fpCam.transform.localPosition;

        if ((vertical != 0 || horizontal != 0) && isInspecting == null)
        {
            bobTimer += bobSpeed * Time.fixedDeltaTime;
            Vector3 newPosition = new Vector3(Mathf.Cos(bobTimer) * bobAmount, camRestingPoint.y + Mathf.Abs(Mathf.Cos(bobTimer) * bobAmount), camRestingPoint.z);
            camPostion = newPosition;         
        }
        else
        {
            Vector3 newPosition = new Vector3(Mathf.Lerp(camPostion.x, camRestingPoint.x, transitionSpeed * Time.deltaTime), Mathf.Lerp(camPostion.y, camRestingPoint.y, transitionSpeed * Time.deltaTime), Mathf.Lerp(camPostion.z, camRestingPoint.z, transitionSpeed * Time.deltaTime));
            camPostion = newPosition;
        }

        if(bobTimer > Mathf.PI * 2)
        {
            bobTimer = 0;   
        }




    }

    void ReleasePlayer()
    {
        isInspecting = null;
    }

}
