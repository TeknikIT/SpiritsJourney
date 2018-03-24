using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    public List<GameObject> doors;
    private List<GameObject> doorCovers;
    public bool roomCompleted = false;
    public bool roomActive = false;
    public Vector2 roomGridPosition;
    public int roomArrayPosition;
    protected LootManager lootManager;
    public List<GameObject> monsters;
    // Use this for initialization
    public virtual void Initialize(Vector2 gridPosition, int arrayPosition) {
        doors = new List<GameObject>(4);
        doorCovers = new List<GameObject>(4);

        foreach(Transform door in transform.Find("Doors").transform)
        {
            doors.Add(door.gameObject);
        }
        foreach(Transform doorCover in transform.Find("CoverWalls").transform)
        {
            doorCovers.Add(doorCover.gameObject);
        }
       
        roomGridPosition = gridPosition;
        roomArrayPosition = arrayPosition;
        lootManager = LevelManager.instance.GetComponent<LootManager>();

    }

    public virtual void PlaceDoor(int direction)
    {
        doors[direction].SetActive(true);
    }

    public virtual void PlaceDoorCover(int direction)
    {
        doorCovers[direction].SetActive(true);
    }


    protected virtual void Update()
    {

    }

    internal void LockAllDoors()
    {
        foreach (GameObject door in doors)
        {
            if (door.GetComponent<Animator>().gameObject.activeSelf)
                door.GetComponent<Animator>().SetBool("IsOpen", false);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LevelManager.SetActiveRoom(roomArrayPosition);
            roomActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            roomActive = false;
        }
    }
    public void OpenAllDoors()
    {
        foreach (GameObject door in doors)
        {
            if (door.GetComponent<Animator>().gameObject.activeSelf)
                door.GetComponent<Animator>().SetBool("IsOpen", true);
        }
    }
    public void OpenADoor(int direction)
    {
        if (doors[direction].GetComponent<Animator>().gameObject.activeSelf)
            doors[direction].GetComponent<Animator>().SetBool("IsOpen", true);
    }
}
