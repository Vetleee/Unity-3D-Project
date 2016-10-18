using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour {

    public bool isLocked = true;
    public bool isGrabbed = false;
    public string KeyName;

    public Vector2 angleRange;
    public Vector2 unlockedRange;
    public Vector2 mousePos;

    private Rigidbody rigid;
    
    [SerializeField]
    private PlayerInventory inv;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        rigid.centerOfMass = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

        if (isGrabbed)
        {
            Quaternion newRot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + Input.GetAxisRaw("Mouse X"), transform.eulerAngles.z);
            if (transform.eulerAngles.y - 180 + Input.GetAxisRaw("Mouse X") > angleRange.x && transform.eulerAngles.y - 180 + Input.GetAxisRaw("Mouse X") < angleRange.y)
                transform.rotation = newRot;
        }
	}


    void Unlock()
    {

        if (!isLocked)
            return;


        if (inv.CheckForItem(KeyName))
        {
            angleRange = unlockedRange;
            //PLaySound
            print("Door unlocked");
        }
        else
        {
            //PLaysound
            print("No Key in inventory");
        }
    }

    public void GetGrabbed()
    {
        isGrabbed = true;
    }

    public void ReleaseDoor()
    {
        isGrabbed = false;
    }


}
