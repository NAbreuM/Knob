using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class TestAvatar : MonoBehaviour {

    [SerializeField]
    float maxCameraTilt = 75f;
    [SerializeField]
    float mouseSensitivity = 250f;
    [SerializeField]
    float moveSpeed = 10f;
    [SerializeField]
    float jumpVelocity = 10f;

    //fields
    Rigidbody avatarBody;
    Transform cameraRig;
    float verticalLookRotation = 0f;


    // Use this for initialization
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        avatarBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        ManipulateCamera();
        MoveAvatar();
    }

    public void ManipulateCamera() {
        //Rotate Avatar left and right.
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity);

        //Rotate Camera vertically.
        verticalLookRotation = Mathf.Clamp(verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity, -maxCameraTilt, maxCameraTilt);
        Camera.main.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    public void MoveAvatar() {
        Vector3 movementDelta = Vector3.zero;
        movementDelta += Vector3.forward * Input.GetAxis("Vertical");
        movementDelta += Vector3.right * Input.GetAxis("Horizontal");
        //Didn't make a new axis for true vertical. Wanted to minimize potential merge conflicts.

        if(Input.GetKeyDown(KeyCode.Space)) {
            Vector3 jumpVector = avatarBody.velocity;
            jumpVector.y = jumpVelocity;
            avatarBody.velocity = jumpVector; //nullify downward velocity.
        }
        avatarBody.useGravity = !Input.GetKey(KeyCode.Space); //No gravity while jump is held down.

        movementDelta.Normalize();
        transform.Translate(movementDelta * moveSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        CS_Room enteredRoom = other.gameObject.GetComponent<CS_Room>();
        if(enteredRoom != null) {
            Debug.Log("Room layer recognized.");
            CS_Manager.Instance.NewRooms(other.GetComponent<CS_Room>());
        }
    }
}
