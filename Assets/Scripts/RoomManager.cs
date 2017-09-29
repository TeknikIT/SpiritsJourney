using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    private List<GameObject> doors;
    private List<GameObject> doorCovers;
    // Use this for initialization
    public void Initialize() {
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
        
	}
	
    public void placeDoor(int direction)
    {
        doors[direction].SetActive(true);
    }

    public void placeDoorCover(int direction)
    {
        doorCovers[direction].SetActive(true);
    }

}
