using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public List<Room> connections;
    public List<Door> doors;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
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
