using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    //List of other rooms that are connected via a door to this room.
    public List<Room> connections;

    //List of doors present inside this room.
    public List<Door> doors;

	
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void ConnectToRoom(Room newConnection)
    {
        connections.Add(newConnection);
    }

    public void DisonnectFromRoom(Room lostConnection)
    {
        connections.Remove(lostConnection);
    }

    public void AddDoor(Door connectionDoor)
    {
        doors.Add(connectionDoor);
    }

    public void RemoveDoor(Door connectionDoor)
    {
        doors.Remove(connectionDoor);
    }
}
