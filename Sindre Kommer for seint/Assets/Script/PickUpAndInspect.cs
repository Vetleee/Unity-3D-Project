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

    [SerializeField]
    private GameObject reticle;
    [SerializeField]
    private Sprite[] reticleSprites;

    [SerializeField]
    private PlayerInventory inventory;


    void start()
    {
        cam = Camera.main;
        reticle.SetActive(false);
        inventory = this.GetComponent<PlayerInventory>();
    }

    public string RaycasToObject(Camera cam)
    {
        if(currentObject != null && currentObject.GetComponent<Inspectable>().canPickUp)
        {
            inventory.AddItem(currentObject.GetComponent<Inspectable>().itemName);
            GameObject.Destroy(currentObject);
            DisableReticle();
            currentObject = null;
            return null;
        }
        else if (currentObject != null)
        {
                currentObject.SendMessage("PutDownObject", null, SendMessageOptions.DontRequireReceiver);
                currentObject = null;
                return null;           
        }

        RaycastHit hit;
        
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000))
        {

            if(hit.distance < pickupDist)
            {
                switch( hit.transform.tag ){
                    case "Interactable":
                        currentObject = hit.transform.gameObject;
                        targetPosition = cam.transform.position + (cam.transform.forward * objectPos);
                        currentObject.SendMessage("PickUpObject", targetPosition);

                        return "Object";
                    case "Door":
                        hit.transform.gameObject.SendMessage("GetGrabbed", SendMessageOptions.DontRequireReceiver);
                        hit.transform.gameObject.SendMessage("Unlock", SendMessageOptions.DontRequireReceiver);
                        currentObject = hit.transform.gameObject;
                        return "Door";
                    case "Lock":
                        return "Lock";
                    default:
                        return null;
                }
            }
        }

        return null;
    }

    public void RotateInspected(float horizontal, float vertical)
    {
        currentObject.transform.Rotate(Vector3.left, horizontal * rotationSpeed , Space.World);
        currentObject.transform.Rotate(Vector3.up, vertical * rotationSpeed , Space.World);


    }

    public void EnableReticle()
    {
        reticle.SetActive(true);
    }

    public void DisableReticle()
    {
        reticle.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Interactable")
            EnableReticle();
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "Interactable")
            DisableReticle();
    }

    public void ReleaseDoor()
    {
        currentObject.SendMessage("ReleaseDoor",null,  SendMessageOptions.DontRequireReceiver);
        currentObject = null;
    }


}
