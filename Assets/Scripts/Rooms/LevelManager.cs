using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LevelGenerationManager))]
public class LevelManager : MonoBehaviour {

    public static int activeRoom;

    public static void SetActiveRoom (int arrayPosition)
    {
        activeRoom = arrayPosition;
    }

    public Vector2 display;
    private LevelGenerationManager levelGenerationManager;
    private List<GameObject> rooms;
    private List<Vector2> directions;
	// Use this for initialization
	void Start () {
        
        
    }
    public void Initialize()
    {
        levelGenerationManager = GetComponent<LevelGenerationManager>();
        rooms = levelGenerationManager.rooms;
        directions = levelGenerationManager.directions;
    }

    private void OpenNeighboursDoors()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < rooms.Count; j++)
            {
                if (rooms[j].GetComponent<RoomManager>().roomGridPosition == rooms[activeRoom].GetComponent<RoomManager>().roomGridPosition + directions[i])
                {
                    if (!rooms[j].GetComponent<RoomManager>().roomCompleted)
                    {
                        rooms[j].GetComponent<RoomManager>().openADoor(GetOppositeDirection(i));
                    }
                }
            }
        }
    }

    private int GetOppositeDirection(int direction)
    {
        for(int i = 0; i < 2; i++)
        {
            if(direction + 1 > 3)
            {
                direction = 0;
            }
            direction++;
        }
        return direction;
    }
    // Update is called once per frame
    void Update () {
        if(rooms[activeRoom].GetComponent<RoomManager>().roomActive && rooms[activeRoom].GetComponent<RoomManager>().roomCompleted)
        {
            OpenNeighboursDoors();
        }
        display = rooms[activeRoom].GetComponent<RoomManager>().roomGridPosition;
	}
}
