using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject[] roomsToRotate;
    public Vector3 rotationAngle;
    public GameObject rotationHelper;
    public GameObject correspondingDoor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RotateRooms()
    {
        if (transform.position == correspondingDoor.transform.position)
        {
            rotationHelper.transform.position = transform.position;
            for (int i = 0; i < roomsToRotate.Length; i++)
            {
                roomsToRotate[i].transform.SetParent(rotationHelper.transform);
            }
            rotationHelper.transform.Rotate(rotationAngle);
            for (int i = 0; i < roomsToRotate.Length; i++)
            {
                roomsToRotate[i].transform.SetParent(rotationHelper.transform.parent);
            }
        }
    }

    public void RotateRoom()
    {
        if (transform.position == correspondingDoor.transform.position)
        {
            rotationHelper.transform.position = transform.position;
            for (int i = 0; i < 1; i++)
            {
                roomsToRotate[i].transform.SetParent(rotationHelper.transform);
            }
            rotationHelper.transform.Rotate(rotationAngle);
            for (int i = 0; i < 1; i++)
            {
                roomsToRotate[i].transform.SetParent(rotationHelper.transform.parent);
            }
        }
    }

    public void MirrorRooms()
    {
        if (transform.position == correspondingDoor.transform.position)
        {
            rotationHelper.transform.position = transform.position;
            for (int i = 0; i < roomsToRotate.Length; i++)
            {
                roomsToRotate[i].transform.SetParent(rotationHelper.transform);
            }
            rotationHelper.transform.localScale = new Vector3(rotationHelper.transform.localScale.x * -1, rotationHelper.transform.localScale.y, rotationHelper.transform.localScale.z);
            for (int i = 0; i < roomsToRotate.Length; i++)
            {
                roomsToRotate[i].transform.SetParent(rotationHelper.transform.parent);
            }
        }
    }

    public void MirrorRoom()
    {
        if (transform.position == correspondingDoor.transform.position)
        {
            rotationHelper.transform.position = transform.position;
            for (int i = 0; i < 1; i++)
            {
                roomsToRotate[i].transform.SetParent(rotationHelper.transform);
            }
            rotationHelper.transform.localScale = new Vector3(rotationHelper.transform.localScale.x * -1, rotationHelper.transform.localScale.y, rotationHelper.transform.localScale.z);
            for (int i = 0; i < 1; i++)
            {
                roomsToRotate[i].transform.SetParent(rotationHelper.transform.parent);
            }
        }
    }
}
