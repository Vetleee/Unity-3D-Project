using UnityEngine;
using System.Collections;

public class PickUpAndInspect : MonoBehaviour {

    public GameObject currentObject;
    public float pickupDist = 50;
    [Range(0.00f, 1.00f)]
    public float objectPos;
    public float rotationSpeed;

    private Camera cam;

    private Vector3 defaultPosition;
    private Vector3 targetPosition;


    void start()
    {
        cam = Camera.main;
    }

    public bool RaycasToObject(Camera cam)
    {

        if (currentObject != null)
        {
                currentObject.SendMessage("PutDownObject", null, SendMessageOptions.DontRequireReceiver);
                currentObject = null;
                return false;           
        }

        RaycastHit hit;
        
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000))
        {

            if (hit.transform.tag == "Interactable" && hit.distance < pickupDist)
            {
                currentObject = hit.transform.gameObject;
                targetPosition = cam.transform.position + (cam.transform.forward * objectPos);
                currentObject.SendMessage("PickUpObject", targetPosition);

                return true;
                
            }
        }
        return false;
    }


    public void RotateInspected(float horizontal, float vertical)
    {
        currentObject.transform.Rotate(Vector3.left, horizontal * rotationSpeed , Space.World);
        currentObject.transform.Rotate(Vector3.up, vertical * rotationSpeed , Space.World);


    }

   
}
