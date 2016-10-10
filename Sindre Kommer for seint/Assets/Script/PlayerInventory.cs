using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

    public List<string> inventory = new List<string>();

	public void AddItem(string item)
    {
        inventory.Add(item);
    }
    public void RemoveItem(string item)
    {
        inventory.Remove(item);
    }
    public bool CheckForItem(string item)
    {
        if (inventory.Contains(item))
            return true;
        else
            return false;
    }
}
