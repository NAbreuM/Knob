using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHelper : MonoBehaviour {

    //Variables to create Lerping Rotation and Mirroring
    float originalScale;
    Quaternion previousRotation;
    Quaternion nextRotation;
    public bool rotating;
    bool mirroring;
    float currentTimeRotating;
    public float totalTimeToRotate = 1;



    void Start () {
		
	}
	

	void Update ()
    {
        //Check if it's rotating, if it is then check whether it's mirroring or not. Afterwards LERP between old and new rotations or old and new scales. 
        //If it has finished LERPing then reset all variables
        if (rotating)
        {
            if (currentTimeRotating < totalTimeToRotate)
            {
                currentTimeRotating += Time.deltaTime;
                if (mirroring)
                {
                    transform.localScale = new Vector3(Mathf.Lerp(originalScale, originalScale * -1, currentTimeRotating/totalTimeToRotate), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(previousRotation, nextRotation, currentTimeRotating / totalTimeToRotate);
                }
            }
            else
            {
                currentTimeRotating = 0;
                rotating = false;
                mirroring = false;

                for (int i = transform.childCount; i > 0; i--)
                {
                    transform.GetChild(i - 1).transform.SetParent(transform.parent);
                }

                transform.rotation = Quaternion.identity;
            }
        }
    }


    //Obtain the rotation the objects are currently at, then obtain the quaternion for the finished rotation and switch the rotation bool so that the LERPing can begin inside the Update function.
    public void StartRotating(Vector3 doorUpVector)
    {
        rotating = true;
        currentTimeRotating = 0;
        previousRotation = transform.rotation;
        transform.Rotate(doorUpVector * 90);
        nextRotation = transform.rotation;
    }


    //Obtain the original scale then set the mirroring variables so that it starts mirroring inside the Update function.
    public void StartMirroring()
    {
        rotating = true;
        mirroring = true;
        currentTimeRotating = 0;
        originalScale = transform.localScale.x;
    }
}
