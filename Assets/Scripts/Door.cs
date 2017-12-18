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

    
    void Start () {

    }
	
	
	void Update () {

    }


    //This function recursively adds all of the rooms connected to the room passed to it to a GameObject that will be used as an auxiliar to rotate/mirror all of the rooms together.
    //Every time a new connection is found, this function is recursively called with the new room, looking for new connections to call the function with.
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


    //Grabs an auxiliary empty GameObject that will be used as a container for all rooms that need to be rotated and places it right at the door so that the rotation can happen at the door.
    //Calls the AddRoomsToParent function with the original room that should be rotated (the one right behind the door).
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


    //Same as RotateRooms only it calls the StartMirroring function instead of the StartRotating inside the RotationHelper.cs
    public void MirrorRooms(Room playerCurrentRoom)
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

            rotationHelper.transform.GetComponent<RotationHelper>().StartMirroring();
        }
    }
}
