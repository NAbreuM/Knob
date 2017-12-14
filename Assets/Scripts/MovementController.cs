using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public GameObject firstRoom;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position - (transform.GetChild(0).forward * 0.025f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position - (transform.GetChild(0).right * 0.025f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + (transform.GetChild(0).right * 0.025f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + (transform.GetChild(0).forward * 0.025f);
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.GetChild(0).eulerAngles = transform.GetChild(0).eulerAngles + (new Vector3(0,-1,0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.GetChild(0).eulerAngles = transform.GetChild(0).eulerAngles + (new Vector3(1, 0, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.GetChild(0).eulerAngles = transform.GetChild(0).eulerAngles + (new Vector3(0, 1, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.GetChild(0).eulerAngles = transform.GetChild(0).eulerAngles + (new Vector3(-1, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //firstRoom.transform.Rotate(new Vector3(90, 0, 0));// = firstRoom.transform.eulerAngles + new Vector3(90, 0, 0);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.GetChild(0).forward, out hit, 100.0f))
            {
                if (hit.transform.GetComponent<Door>())
                {
                    hit.transform.GetComponent<Door>().RotateRooms();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //firstRoom.transform.Rotate(new Vector3(0, 90, 0));// = firstRoom.transform.eulerAngles + new Vector3(0, 90, 0);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.GetChild(0).forward, out hit, 100.0f))
            {
                if (hit.transform.GetComponent<Door>())
                {
                    hit.transform.GetComponent<Door>().RotateRoom();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //firstRoom.transform.Rotate(new Vector3(0, 0, 90));// = firstRoom.transform.eulerAngles + new Vector3(0, 0, 90);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.GetChild(0).forward, out hit, 100.0f))
            {
                if (hit.transform.GetComponent<Door>())
                {
                    hit.transform.GetComponent<Door>().MirrorRooms();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //firstRoom.transform.localScale = new Vector3(firstRoom.transform.localScale.x * (-1), firstRoom.transform.localScale.y, firstRoom.transform.localScale.z);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.GetChild(0).forward, out hit, 100.0f))
            {
                if (hit.transform.GetComponent<Door>())
                {
                    hit.transform.GetComponent<Door>().MirrorRoom();
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
        }
    }
}
