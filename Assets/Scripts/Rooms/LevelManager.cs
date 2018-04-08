using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Keeping track of all the rooms on the level, i.e. when the doors are supposed to open
/// </summary>


[RequireComponent(typeof(LevelGenerationManager))]
public class LevelManager : MonoBehaviour {

    #region Singleton
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    // The current active room
    public static int activeRoom;

    //Set the active room
    public static void SetActiveRoom (int arrayPosition)
    {
        activeRoom = arrayPosition;
    }

    //public Vector2 display;

    private LevelGenerationManager levelGenerationManager;

    private List<GameObject> rooms;
    public List<GameObject> enemies;
    private List<Vector2> directions;

    public int startingRoomAmount;
    public int currentRoomAmount;
    public int maxRoomAmount;

	// Use this for initialization
	void Start () {
        directions = new List<Vector2>
        {
            Vector2.up,
            Vector2.right,
            Vector2.down,
            Vector2.left
        };

    }
    /// <summary>
    /// Initialize the level
    /// </summary>
    public void Initialize()
    {
        levelGenerationManager = GetComponent<LevelGenerationManager>();
        rooms = levelGenerationManager.rooms;
    }

    /// <summary>
    /// Reload the level
    /// </summary>
    public void Reload()
    {
        Transform roomTransforms = GameObject.Find("Rooms").transform;
        foreach (Transform rt in roomTransforms)
        {
            Destroy(rt.gameObject);
        }
        levelGenerationManager.InstantiateRooms();
        rooms = levelGenerationManager.rooms;
        PlayerManager.instance.transform.position = PlayerManager.instance.GetComponent<PlayerController>().originPosition;
        PlayerManager.instance.health = 100;
        foreach (GameObject r in rooms)
        {
            if (r.GetComponent<RoomManager>().monsters.Count != 0)
            {
                foreach (GameObject monster in r.GetComponent<RoomManager>().monsters)
                {
                    enemies.Add(monster);
                }
            }
        }
    }

    /// <summary>
    /// Open the doors of a neighbouring room
    /// </summary>
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
                        rooms[j].GetComponent<RoomManager>().OpenADoor(GetOppositeDirection(i));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets the opposite direction in the direction list
    /// </summary>
    /// <param name="direction">Input direction</param>
    /// <returns>Oposite direction index</returns>
    private int GetOppositeDirection(int direction)
    {
        for (int i = 0; i < 2; i++)
        {
            if(direction + 1 > 3)
            {
                direction = 0;
            }
            else
            {
                direction++;
            }
        }
        return direction;
    }
    // Update is called once per frame
    void Update () {
        if(rooms[activeRoom].GetComponent<RoomManager>().roomActive && rooms[activeRoom].GetComponent<RoomManager>().roomCompleted)
        {
            OpenNeighboursDoors();
        }
        else if(rooms[activeRoom].GetComponent<RoomManager>().roomActive && !rooms[activeRoom].GetComponent<RoomManager>().roomCompleted)
        {
            rooms[activeRoom].GetComponent<RoomManager>().LockAllDoors();
        }
        //display = rooms[activeRoom].GetComponent<RoomManager>().roomGridPosition;

	}
}
