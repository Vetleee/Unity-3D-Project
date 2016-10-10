using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {


    public string Key;
    private PlayerInventory pInventory;

    void Interact()
    {
        if (pInventory.CheckForItem(Key))
        {
            print("dothing");
        }
        else
            print("no");
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "Player")
        {
            pInventory = col.gameObject.GetComponent<PlayerInventory>();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.transform.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            if (pInventory == null)
                pInventory = col.gameObject.GetComponent<PlayerInventory>();


            Interact();
        }
    }

    void OnTriggerExit(Collider col)
    {
        pInventory = null;
    }



}
