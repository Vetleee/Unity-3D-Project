using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textboxManager : MonoBehaviour {

	public GameObject TextBox; 
	public Text theText;


	public TextAsset textfile;
	public string[] textLines;

	public int currentline;
	public int endAtline;

	public fpscontroll player;

	public bool isActive;

	public bool StopPlayerMovement;

	private bool isTyping = false;
	private bool CancelTyping = false; 

	public float typeSpeed;


	// Use this for initialization
	void Start () 
	{
		player = FindObjectOfType<fpscontroll>();
		if (textfile != null) 
		{
			textLines = (textfile.text.Split ('\n'));
		}
		if (endAtline == 0) 
		{
			endAtline = textLines.Length - 1;

		}
		if (isActive) {
			EnableTextBox ();
		} else 
		{
			DisableTextBox ();
		}

	}

	// Update is called once per frame
	void Update () 
	{
		if (!isActive) 
		{
			return;
		}

		//theText.text = textLines [currentline];

		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			if (!isTyping) {
				currentline += 1;

				if (currentline > endAtline) {
					DisableTextBox ();
				} else 
				{

					StartCoroutine (TextScroll(textLines[currentline]));
				}
			} else if(isTyping && !CancelTyping)
			{
				CancelTyping = true; 
			}
		}

	

	}

	private IEnumerator TextScroll(string lineOfText)
	{
		int letter = 0;
		theText.text = "";
		isTyping = true; 
		CancelTyping = false;
		while (isTyping = true && !CancelTyping && (letter < lineOfText.Length - 1)) 
		{
			theText.text += lineOfText [letter];
			letter += 1;
			yield return new WaitForSeconds (typeSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		CancelTyping = false; 
	}


	public void EnableTextBox()
	{
		TextBox.SetActive (true);
		isActive = true;

		if (StopPlayerMovement) 
		{
			player.canMove = false;
		}

		StartCoroutine (TextScroll(textLines[currentline]));

	}

	public void DisableTextBox()
	{
		TextBox.SetActive (false);
		isActive = false; 

		if (StopPlayerMovement) 
		{
			player.canMove = true;
		}
	}
	public void ReloadScript(TextAsset theText)
	{
		if (theText != null) 
		{
			textLines = new string[1];
			textLines = (theText.text.Split ('\n'));
		}


	}

}
