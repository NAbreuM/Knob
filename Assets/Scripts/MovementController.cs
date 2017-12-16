﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    //Currently Held Door Knobs
    List<Door> DoorKnobs = new List<Door>();

    public Room currentRoom;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Basic Movement (WASD for Moving, Arrow Keys for Camera)
        {
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
                transform.GetChild(0).eulerAngles = transform.GetChild(0).eulerAngles + (new Vector3(0, -1, 0));
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


            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
            }
        }


        //Place Door on Specific Spot on Wall
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (DoorKnobs.Count > 0)
                {
                    RaycastHit[] hit;

                    hit = Physics.RaycastAll(transform.position, transform.GetChild(0).forward, 10.0f);

                    Debug.DrawRay(transform.position, transform.GetChild(0).forward * 100, Color.green, 5);

                    if (hit.Length > 0 && hit[0].transform.tag == "WallDoor")
                    {
                        DoorKnobs[DoorKnobs.Count - 1].currentWalls[0] = hit[0].transform.gameObject;
                        hit[0].transform.gameObject.SetActive(false);
                        hit[0].transform.parent.GetComponent<Room>().AddDoor(DoorKnobs[DoorKnobs.Count - 1]);

                        if (hit.Length > 1)
                        {
                            DoorKnobs[DoorKnobs.Count - 1].currentWalls[1] = hit[1].transform.gameObject;
                            hit[1].transform.gameObject.SetActive(false);
                            hit[1].transform.parent.GetComponent<Room>().AddDoor(DoorKnobs[DoorKnobs.Count - 1]);

                            hit[0].transform.parent.GetComponent<Room>().ConnectToRoom(hit[1].transform.parent.GetComponent<Room>());
                            hit[1].transform.parent.GetComponent<Room>().ConnectToRoom(hit[0].transform.parent.GetComponent<Room>());
                        }

                        DoorKnobs[DoorKnobs.Count - 1].transform.position = hit[0].point;
                        DoorKnobs[DoorKnobs.Count - 1].transform.rotation = hit[0].transform.rotation;

                        if (DoorKnobs[DoorKnobs.Count - 1].transform.eulerAngles.y % 180 == 0)
                        {
                            if (DoorKnobs[DoorKnobs.Count - 1].transform.eulerAngles.z % 180 == 0)
                            {
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localScale = new Vector3(hit[0].transform.localScale.x, hit[0].transform.localScale.y, (hit[0].point.z - (hit[0].transform.localPosition.z - (hit[0].transform.localScale.z / 2))) - (DoorKnobs[DoorKnobs.Count - 1].doorGapX / 2));
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localPosition = new Vector3(hit[0].transform.position.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y, DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localPosition.y, hit[0].transform.localPosition.z - (hit[0].transform.localScale.z / 2) + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localScale.z / 2) - (DoorKnobs[DoorKnobs.Count - 1].transform.position.z));
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localScale = new Vector3(hit[0].transform.localScale.x, hit[0].transform.localScale.y, (hit[0].transform.localScale.z - (hit[0].point.z - (hit[0].transform.localPosition.z - (hit[0].transform.localScale.z / 2)))) - (DoorKnobs[DoorKnobs.Count - 1].doorGapX / 2));
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localPosition = new Vector3(hit[0].transform.position.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y, DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localPosition.y, hit[0].transform.localPosition.z - (hit[0].transform.localScale.z / 2) + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localScale.z + DoorKnobs[DoorKnobs.Count - 1].doorGapX + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localScale.z / 2) - (DoorKnobs[DoorKnobs.Count - 1].transform.position.z)));



                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localScale = new Vector3(hit[0].transform.localScale.x - (hit[0].point.y - hit[0].transform.localPosition.y + (DoorKnobs[DoorKnobs.Count - 1].doorGapY / 2)), hit[0].transform.localScale.y, hit[0].transform.localScale.z);
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localPosition = new Vector3((hit[0].point.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y) + (DoorKnobs[DoorKnobs.Count - 1].doorGapY / 2) + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localScale.x / 2), DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localPosition.y, hit[0].transform.position.z - DoorKnobs[DoorKnobs.Count - 1].transform.position.z);
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localScale = new Vector3(hit[0].transform.localScale.x - DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localScale.x - DoorKnobs[DoorKnobs.Count - 1].doorGapY, hit[0].transform.localScale.y, hit[0].transform.localScale.z);
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localPosition = new Vector3((hit[0].point.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y) - (DoorKnobs[DoorKnobs.Count - 1].doorGapY / 2) - (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localScale.x / 2), DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localPosition.y, hit[0].transform.position.z - DoorKnobs[DoorKnobs.Count - 1].transform.position.z);
                            }
                            else
                            {
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localScale = new Vector3(hit[0].transform.localScale.x, hit[0].transform.localScale.y, (hit[0].point.z - (hit[0].transform.localPosition.z - (hit[0].transform.localScale.z / 2))) - (DoorKnobs[DoorKnobs.Count - 1].doorGapX / 2));
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localPosition = new Vector3(hit[0].transform.position.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y, DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localPosition.y, hit[0].transform.localPosition.z - (hit[0].transform.localScale.z / 2) + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localScale.z / 2) - (DoorKnobs[DoorKnobs.Count - 1].transform.position.z));
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localScale = new Vector3(hit[0].transform.localScale.x, hit[0].transform.localScale.y, (hit[0].transform.localScale.z - (hit[0].point.z - (hit[0].transform.localPosition.z - (hit[0].transform.localScale.z / 2)))) - (DoorKnobs[DoorKnobs.Count - 1].doorGapX / 2));
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localPosition = new Vector3(hit[0].transform.position.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y, DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localPosition.y, hit[0].transform.localPosition.z - (hit[0].transform.localScale.z / 2) + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localScale.z + DoorKnobs[DoorKnobs.Count - 1].doorGapX + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localScale.z / 2) - (DoorKnobs[DoorKnobs.Count - 1].transform.position.z)));



                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localScale = new Vector3(hit[0].transform.localScale.x - (hit[0].point.y - hit[0].transform.localPosition.y + (DoorKnobs[DoorKnobs.Count - 1].doorGapY / 2)), hit[0].transform.localScale.y, hit[0].transform.localScale.z);
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localPosition = new Vector3((hit[0].point.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y) + (DoorKnobs[DoorKnobs.Count - 1].doorGapY/2) + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localScale.x/2), DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localPosition.y, hit[0].transform.position.z - DoorKnobs[DoorKnobs.Count - 1].transform.position.z);
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localScale = new Vector3(hit[0].transform.localScale.x - DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localScale.x - DoorKnobs[DoorKnobs.Count - 1].doorGapY, hit[0].transform.localScale.y, hit[0].transform.localScale.z);
                                DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localPosition = new Vector3((hit[0].point.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y) - (DoorKnobs[DoorKnobs.Count - 1].doorGapY / 2) - (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localScale.x / 2), DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localPosition.y, hit[0].transform.position.z - DoorKnobs[DoorKnobs.Count - 1].transform.position.z);
                            }
                        }
                        else
                        {
                            DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localScale = new Vector3(hit[0].transform.localScale.x, hit[0].transform.localScale.y, (hit[0].point.x - (hit[0].transform.localPosition.x - (hit[0].transform.localScale.z / 2))) - (DoorKnobs[DoorKnobs.Count - 1].doorGapX / 2));
                            DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localPosition = new Vector3(hit[0].transform.position.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y, DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localPosition.y, hit[0].transform.localPosition.x - (hit[0].transform.localScale.z / 2) + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localScale.z / 2) - (DoorKnobs[DoorKnobs.Count - 1].transform.position.x));
                            DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localScale = new Vector3(hit[0].transform.localScale.x, hit[0].transform.localScale.y, (hit[0].transform.localScale.z - (hit[0].point.x - (hit[0].transform.localPosition.x - (hit[0].transform.localScale.z / 2)))) - (DoorKnobs[DoorKnobs.Count - 1].doorGapX / 2));
                            DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localPosition = new Vector3(hit[0].transform.position.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y, DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localPosition.y, hit[0].transform.localPosition.x - (hit[0].transform.localScale.z / 2) + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(0).localScale.z + DoorKnobs[DoorKnobs.Count - 1].doorGapX + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(1).localScale.z / 2) - (DoorKnobs[DoorKnobs.Count - 1].transform.position.x)));


                            DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localScale = new Vector3(hit[0].transform.localScale.x - (hit[0].point.y - hit[0].transform.localPosition.y + (DoorKnobs[DoorKnobs.Count - 1].doorGapY / 2)), hit[0].transform.localScale.y, hit[0].transform.localScale.z);
                            DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localPosition = new Vector3((hit[0].point.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y) + (DoorKnobs[DoorKnobs.Count - 1].doorGapY / 2) + (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localScale.x / 2), DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localPosition.y, hit[0].transform.position.x - DoorKnobs[DoorKnobs.Count - 1].transform.position.x);
                            DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localScale = new Vector3(hit[0].transform.localScale.x - DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(2).localScale.x - DoorKnobs[DoorKnobs.Count - 1].doorGapY, hit[0].transform.localScale.y, hit[0].transform.localScale.z);
                            DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localPosition = new Vector3((hit[0].point.y - DoorKnobs[DoorKnobs.Count - 1].transform.position.y) - (DoorKnobs[DoorKnobs.Count - 1].doorGapY / 2) - (DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localScale.x / 2), DoorKnobs[DoorKnobs.Count - 1].transform.GetChild(3).localPosition.y, hit[0].transform.position.x - DoorKnobs[DoorKnobs.Count - 1].transform.position.x);
                        }
                        DoorKnobs.Remove(DoorKnobs[DoorKnobs.Count - 1]);
                    }
                }
            }
        }


        //Take Door from Wall
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                RaycastHit hit;

                Debug.DrawRay(transform.position, transform.GetChild(0).forward * 100, Color.red, 5);

                if (Physics.Raycast(transform.position, transform.GetChild(0).forward, out hit, 100.0f))
                {
                    if (hit.transform.GetComponent<Door>())
                    {
                        hit.transform.position = new Vector3(1000,1000,1000);
                        if (hit.transform.GetComponent<Door>().currentWalls[0])
                        {
                            hit.transform.GetComponent<Door>().currentWalls[0].gameObject.SetActive(true);
                            hit.transform.GetComponent<Door>().currentWalls[0].transform.parent.GetComponent<Room>().RemoveDoor(hit.transform.GetComponent<Door>());

                            if (hit.transform.GetComponent<Door>().currentWalls[1])
                            {
                                hit.transform.GetComponent<Door>().currentWalls[1].gameObject.SetActive(true);
                                hit.transform.GetComponent<Door>().currentWalls[1].transform.parent.GetComponent<Room>().RemoveDoor(hit.transform.GetComponent<Door>());

                                hit.transform.GetComponent<Door>().currentWalls[0].transform.parent.GetComponent<Room>().DisonnectFromRoom(hit.transform.GetComponent<Door>().currentWalls[1].transform.parent.GetComponent<Room>());
                                hit.transform.GetComponent<Door>().currentWalls[1].transform.parent.GetComponent<Room>().DisonnectFromRoom(hit.transform.GetComponent<Door>().currentWalls[0].transform.parent.GetComponent<Room>());
                            }
                        }
                        DoorKnobs.Add(hit.transform.GetComponent<Door>());
                    }
                }
            }
        }


        //Rotate all Walls behind the Door in front of the Player
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RaycastHit hit;

                Debug.DrawRay(transform.position, transform.GetChild(0).forward * 100, Color.blue, 5);

                if (Physics.Raycast(transform.position, transform.GetChild(0).forward, out hit, 100.0f))
                {
                    if (hit.transform.GetComponent<Door>())
                    {
                        hit.transform.GetComponent<Door>().RotateRooms(currentRoom);
                    }
                }
            }
        }


        //Mirror all Walls behind the Door in front of the Player
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.GetChild(0).forward, out hit, 100.0f))
                {
                    if (hit.transform.GetComponent<Door>())
                    {
                        //hit.transform.GetComponent<Door>().MirrorRooms();
                    }
                }
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.activeSelf == true && collision.transform.tag == "WallDoor")
        {
            currentRoom = collision.transform.parent.GetComponent<Room>();
        }
    }
}
