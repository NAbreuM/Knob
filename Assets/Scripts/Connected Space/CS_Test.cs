using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Test : MonoBehaviour {

    [SerializeField]
    GameObject roomPrefab;

    //internal fields
    CS_Room currentRoom;

    private void Start() {
        GameObject initialRoom = Instantiate<GameObject>(roomPrefab);
        currentRoom = initialRoom.GetComponent<CS_Room>();
    }

    void Update () {
        //press space to spawn the next room.
        if(Input.GetKeyDown(KeyCode.Space)) {
            //currentRoom = currentRoom.doors[0].OpenRoom();
        }
	}
}
