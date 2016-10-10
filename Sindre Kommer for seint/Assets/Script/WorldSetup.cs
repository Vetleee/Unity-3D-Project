using UnityEngine;
using System.Collections;

public class WorldSetup : MonoBehaviour {

    private string gameState;

	// Use this for initialization
	void Start () {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        gameState = "Playing";

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (gameState)
            {
                case "Paused":
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                    gameState = "Playing";
                    break;
                case "Playing":
                    UnityEngine.Cursor.lockState = CursorLockMode.None;
                    gameState = "Paused";
                    break;
            }
        }


        
    }
    
}
