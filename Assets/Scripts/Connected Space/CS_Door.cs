using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Door : MonoBehaviour {

    public int destinationRoomIndex; //The index for the room the door connects to.
    public int destinationDoorIndex; //The index for the door in that room that this door connects to.

    public Quaternion roomRotation {
        //get {return Quaternion.Inverse(Quaternion.LookRotation(-transform.forward, transform.up)); }
        get { return Quaternion.Inverse(transform.localRotation); }
    }
    public Vector3 roomOffset {
        get { return -transform.localPosition; }
    }

    public void Start() {
        //roomRotation = Quaternion.Inverse(Quaternion.LookRotation(-transform.forward, transform.up)); // * Quaternion.AngleAxis(180, Vector3.up); //local rotation appears to be nothing, will need a from to rotation?
        //roomOffset = -transform.localPosition;
        
        //Not sure if this initialization fro the TRS is correct. Currently it is mostly the same as the local matrix, but I may need to invert or somehow manipulate position and rotation.
        //Note: order matters when multipling Quaternions. The left-hand operand determines the spatial context.
        //Note: There might be an issue with sideways doors if local rotation is based off of the parent object.
        //doorToRoomMatrix = Matrix4x4.TRS(-transform.localPosition, transform.localRotation * Quaternion.AngleAxis(180,Vector3.up), Vector3.one); //chose not to allow for variable scale, especially since prototype objectects tend to have weird scales.
    }

    /*
    public CS_Room OpenRoom() {
        GameObject openRoom = Instantiate<GameObject>(destinationPrefab);
        CS_Room room = openRoom.GetComponent<CS_Room>();
        room.InitializeRoom(this.transform, partnerDoorIndex);
        return room;
    }
    */


}
