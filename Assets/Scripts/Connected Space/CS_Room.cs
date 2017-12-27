using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Room : MonoBehaviour {

    public CS_Door[] doors; //an array that holds the room's doors.

    public void InitializeRoom(Transform openDoor, int entryDoorIndex) {
        if(entryDoorIndex < doors.Length && entryDoorIndex>= 0) {
            //Debug.Log(openDoor.rotation.ToString());
            //Debug.Log(openDoor.up);
            //transform.rotation = openDoor.rotation * Quaternion.AngleAxis(180, openDoor.up) * doors[entryDoorIndex].roomRotation;

            transform.rotation = openDoor.rotation * doors[entryDoorIndex].roomRotation;
            transform.position = openDoor.position + transform.TransformDirection(doors[entryDoorIndex].roomOffset);
            transform.RotateAround(openDoor.position, openDoor.up, 180);
        }
    }
}
