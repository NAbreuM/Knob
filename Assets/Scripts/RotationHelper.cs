using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHelper : MonoBehaviour {


    Quaternion previousRotation;
    Quaternion nextRotation;
    public bool rotating;
    float currentTimeRotating;
    public float totalTimeToRotate = 2;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (rotating)
        {
            if (currentTimeRotating < totalTimeToRotate)
            {
                currentTimeRotating += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(previousRotation, nextRotation, currentTimeRotating / totalTimeToRotate);
            }
            else
            {
                currentTimeRotating = 0;
                rotating = false;

                for (int i = transform.childCount; i > 0; i--)
                {
                    transform.GetChild(i - 1).transform.SetParent(transform.parent);
                }

                transform.rotation = Quaternion.identity;
            }
        }
    }


    public void StartRotating(Vector3 doorUpVector)
    {
        rotating = true;
        currentTimeRotating = 0;
        previousRotation = transform.rotation;
        transform.Rotate(doorUpVector * 90);
        nextRotation = transform.rotation;
    }
}
