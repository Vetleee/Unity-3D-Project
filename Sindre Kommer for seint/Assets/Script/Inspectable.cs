using UnityEngine;
using System.Collections;

public class Inspectable : MonoBehaviour {

    private bool lerping;
    private bool held;

    private Vector3 targetVector;
    private Vector3 startVector;

    private Quaternion defaultRotation;
    private Vector3 defaultVector;
    private Quaternion newRotation;

    private float animationTime = .2f;
    private float moveTimer;


    void Start()
    {
        defaultRotation = transform.rotation;
        defaultVector = transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (lerping)
        {
            transform.position = Vector3.Lerp(startVector, targetVector, moveTimer / animationTime);
            moveTimer += Time.fixedDeltaTime;

            if (!held)
            {
                transform.rotation = Quaternion.Slerp(newRotation, defaultRotation, moveTimer / animationTime);
            }

            if (Vector3.Distance(transform.position, targetVector) < 0.05f)
            {
                transform.position = targetVector;
                lerping = false;
            }
        }
	
	}


    void PickUpObject(Vector3 targetIn)
    {
        targetVector = targetIn;
        startVector = defaultVector;
        moveTimer = 0;
        held = true;
        lerping = true;
    }
    void PutDownObject()
    {        
        targetVector = defaultVector;
        startVector = transform.position;
        newRotation = transform.rotation;
        moveTimer = 0;
        lerping = true;
        held = false;
    }

}
