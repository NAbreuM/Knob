using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativePlayerController : MonoBehaviour {


    [SerializeField]
    float maxCameraTilt = 75f;
    [SerializeField]
    float mouseSensitivity = 250f;
    [SerializeField]
    float moveSpeed = 10f;
    [SerializeField]
    float jumpVelocity = 10f;
    [SerializeField]
    float gravityAcceleration = 10f;

    //fields
    Rigidbody rb;
    Transform cam;
    public float verticalLookRotation = 0f;
    public Vector3 gravityUp = Vector3.up;


    // Use this for initialization
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        ManipulateCamera();
        MoveAvatar();
    }

    public void ManipulateCamera() {
        if(Input.GetKey(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
        if(Cursor.lockState == CursorLockMode.Locked) {
            //Rotate Avatar left and right.
            transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity);
            //Rotate Camera vertically.
            verticalLookRotation = Mathf.Clamp(verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity, -maxCameraTilt, maxCameraTilt);
            Camera.main.transform.localEulerAngles = Vector3.left * verticalLookRotation;
        }
    }

    public void MoveAvatar() {
        Vector3 movementVelocity = Vector3.zero;
        movementVelocity += transform.forward * Input.GetAxis("Vertical");
        movementVelocity += transform.right * Input.GetAxis("Horizontal");
        movementVelocity = movementVelocity.normalized * moveSpeed;

        //get falling velocity
        movementVelocity += Vector3.Project(rb.velocity, gravityUp);

        //apply velocity
        rb.velocity = movementVelocity;

        if(Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = Vector3.ProjectOnPlane(rb.velocity, gravityUp); //nullify downward velocity;
            rb.AddForce(gravityUp * jumpVelocity, ForceMode.VelocityChange);
        }
        if(!Input.GetKey(KeyCode.Space)) {//No gravity while jump is held down.
            rb.AddForce(-gravityUp * gravityAcceleration, ForceMode.Acceleration);
        }
    }

    public void PortalCameraCorrect(Transform target) {
        transform.position = target.position;
        transform.rotation = target.rotation;
        gravityUp = transform.up; //update the avatar's down direction;
        rb.velocity = Vector3.zero; //reset velocity. A polished version of this script would reorient velocity.
    }
}
