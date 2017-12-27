using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Manager : Singleton<CS_Manager> {

    [SerializeField]
    GameObject[] roomTypes;

    //internal fields
    CS_Room lastRoom;
    CS_Room currentRoom;

    List<CS_Room> rooms;

    public void Start() {
        rooms = new List<CS_Room>();
        
        //Create starting room.
        if(roomTypes.Length > 0) {
            GameObject startingRoom = Instantiate<GameObject>(roomTypes[0]);
            currentRoom = startingRoom.GetComponent<CS_Room>();
            rooms.Add(currentRoom);
        } else {
            Debug.Log("Can't instantiate a starting room. There are no registered Room Types.");
        }
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            //Destroy the room last left and create a new room to enter.
            if(lastRoom != null) { Destroy(lastRoom.gameObject); }
            lastRoom = currentRoom;

            CS_Door randomDoor = lastRoom.doors[Random.Range(0, lastRoom.doors.Length - 1)];
            currentRoom = RandomOpen(randomDoor);
        }
    }

    public void NewRooms(CS_Room enteredRoom) {
        //CS_Room[] nextRooms = new CS_Room[enteredRoom.doors.Length + 1];
        //nextRooms[nextRooms.Length] = enteredRoom; //put the entered room at the back of the list.
        for(int i = rooms.Count - 1; i > 0;i--) { //Remove all rooms except the entered room.
            if(!Object.ReferenceEquals(rooms[i], enteredRoom)) {
                Destroy(rooms[i].gameObject);
                rooms.RemoveAt(i);
            }
        }
        for(int i = 0; i < enteredRoom.doors.Length; i++) {
            rooms.Add(RandomOpen(enteredRoom.doors[i]));
        }

    }


    //Random open is for demo purposes. It just makes a random connection from a random door.
    private CS_Room RandomOpen(CS_Door door) {
        GameObject openRoom = Instantiate<GameObject>(roomTypes[Random.Range(0, roomTypes.Length)]); //Instantiate a copy of the object
        CS_Room destinationRoom = openRoom.GetComponent<CS_Room>();
        destinationRoom.InitializeRoom(door.transform, Random.Range(0, destinationRoom.doors.Length - 1));
        return destinationRoom;
    }

    private CS_Room OpenDoor(CS_Door door) {
        GameObject openRoom = Instantiate<GameObject>(roomTypes[door.destinationRoomIndex]); //Instantiate a copy of the object
        CS_Room destinationRoom = openRoom.GetComponent<CS_Room>();
        destinationRoom.InitializeRoom(door.transform, door.destinationDoorIndex);
        return destinationRoom;
    }
}
