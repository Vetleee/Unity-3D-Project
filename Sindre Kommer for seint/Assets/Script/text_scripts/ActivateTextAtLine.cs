using UnityEngine;
using System.Collections;

public class ActivateTextAtLine : MonoBehaviour 
{
	public TextAsset theText;

	public int startAtLine;

	public int endLine;

	public textboxManager theTextBox;

	public bool DestroyWhenActivated;

	public GameObject player;

	public bool freezePlayer = false;

	// Use this for initialization
	void Start ()
	{
		theTextBox = FindObjectOfType<textboxManager> (); 
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.name == "PlayerController") 
		{
			theTextBox.ReloadScript (theText);
			theTextBox.currentline = startAtLine;
			theTextBox.endAtline = endLine;
			theTextBox.EnableTextBox ();

			if (DestroyWhenActivated) 
			{
				Destroy (gameObject);
			}
		}
	}
}
