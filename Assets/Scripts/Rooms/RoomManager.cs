using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The parent room managaer
/// </summary>
public class RoomManager : MonoBehaviour {

    // The doors or door covers
    public List<GameObject> doors;
    private List<GameObject> doorCovers;

    // Room status
    public bool roomCompleted = false;
    public bool roomActive = false;

    // The position in the grid
    public Vector2 roomGridPosition;

    // The index in the room grid
    public int roomArrayPosition;

    // The loot manager
    protected LootManager lootManager;

    // The monsters in the room
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

    protected virtual void Update()
    {

    }

    /// <summary>
    /// Place a door
    /// </summary>
    /// <param name="direction">direction of the door</param>
    public virtual void PlaceDoor(int direction)
    {
        doors[direction].SetActive(true);
    }

    /// <summary>
    /// Place a cover instead of door
    /// </summary>
    /// <param name="direction">direction of the door cover</param>
    public virtual void PlaceDoorCover(int direction)
    {
        doorCovers[direction].SetActive(true);
    }

    /// <summary>
    /// Lock all the doors
    /// </summary>
    internal void LockAllDoors()
    {
        foreach (GameObject door in doors)
        {
            if (door.GetComponent<Animator>().gameObject.activeSelf)
                door.GetComponent<Animator>().SetBool("IsOpen", false);
        }
    }

    /// <summary>
    /// Opens all the doors
    /// </summary>
    public void OpenAllDoors()
    {
        foreach (GameObject door in doors)
        {
            if (door.GetComponent<Animator>().gameObject.activeSelf)
                door.GetComponent<Animator>().SetBool("IsOpen", true);
        }
    }

    /// <summary>
    /// Opens a specific door
    /// </summary>
    /// <param name="direction">direction of the door</param>
    public void OpenADoor(int direction)
    {
        if (doors[direction].GetComponent<Animator>().gameObject.activeSelf)
            doors[direction].GetComponent<Animator>().SetBool("IsOpen", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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
}