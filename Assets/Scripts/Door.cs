using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    //Auxiliary Gameobject to help rotation by parenting everything below it
    public GameObject rotationHelper;

    //Walls this Door is currently attached to
    public GameObject[] currentWalls = new GameObject[2];

    //Gap between doors
    public float doorGapX;
    public float doorGapY;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }

    public void AddRoomsToParent(Room roomToAdd, Room playerCurrentRoom)
    {
        if (!roomToAdd.transform.IsChildOf(rotationHelper.transform))
        {
            roomToAdd.transform.SetParent(rotationHelper.transform);
            for (int i=0; i<roomToAdd.connections.Count; i++)
            {
                if (roomToAdd.connections[i] != playerCurrentRoom)
                {
                    AddRoomsToParent(roomToAdd.connections[i], playerCurrentRoom);
                }
            }

            for (int i = 0; i < roomToAdd.doors.Count; i++)
            {
                if (!playerCurrentRoom.doors.Contains(roomToAdd.doors[i]))
                {
                    roomToAdd.doors[i].transform.SetParent(rotationHelper.transform);
                }
            }
        }
    }

    public void RotateRooms(Room playerCurrentRoom)
    {
        if (!rotationHelper.transform.GetComponent<RotationHelper>().rotating)
        {
            rotationHelper.transform.position = transform.position;

            if (currentWalls[0].transform.parent.GetComponent<Room>() != playerCurrentRoom)
            {
                AddRoomsToParent(currentWalls[0].transform.parent.GetComponent<Room>(), playerCurrentRoom);
            }
            else
            {
                AddRoomsToParent(currentWalls[1].transform.parent.GetComponent<Room>(), playerCurrentRoom);
            }

            rotationHelper.transform.GetComponent<RotationHelper>().StartRotating(this.transform.up);
        }
    }

    public void MirrorRooms()
    {
        /*rotationHelper.transform.position = transform.position;

        for (int i = 0; i < rotateableObjects.Length; i++)
        {
            rotateableObjects[i].transform.SetParent(rotationHelper.transform);
        }
        new Vector3(rotationHelper.transform.localScale.x* -1, rotationHelper.transform.localScale.y, rotationHelper.transform.localScale.z);
        for (int i = 0; i < rotateableObjects.Length; i++)
        {
            rotateableObjects[i].transform.SetParent(rotationHelper.transform.parent);
        }*/
    }
}
