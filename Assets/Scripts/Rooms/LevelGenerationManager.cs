using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LevelGenerationHelper))]
public class LevelGenerationManager : MonoBehaviour {
    #region Singleton
    public static LevelGenerationManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    //Room prefabs
    private GameObject[] GeneralRoomPrefabs, BossRoomPrefabs, StartingRoomPrefabs, LootRoomPrefabs; 
    private GameObject emptyRoom; 

    private int levelSize; //The size of the level. More than one 100 is very laggy

    public LevelGenerationHelper levelGenerationHelper; //Helperclass for creating the level

    private Bounds[] roomBounds = new Bounds[100]; //Size of rooms, Used for making enough room between rooms

    private Vector3 emptyRoomSize;

    public List<GameObject> rooms; // A list of all the rooms in the game
    public List< Vector2> directions;//A list of vectors setting integers to Vectors

    public float maxX, maxY, minX, minY;

    // Use this for initialization
    void Start () {
        //Initalizing lists
        directions = new List<Vector2>
        {
            Vector2.up,
            Vector2.right,
            Vector2.down,
            Vector2.left
        };
        //Creating the rooms
        //GetComponent<LevelManager>().Initialize(); // Initializing Levelmanager
    }

    /// <summary>
    /// Instantiating the rooms
    /// </summary>
    public void InstantiateRooms()
    {
        if(directions.Count == 0)
        {
            Start();
        }
        levelSize = LevelManager.instance.currentRoomAmount;
        rooms = new List<GameObject>();
        //Loading in all prefabs
        GeneralRoomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms/GeneralRooms");
        BossRoomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms/BossRooms");
        StartingRoomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms/StartingRooms");
        LootRoomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms/LootRooms");
        emptyRoom = Resources.Load<GameObject>("Prefabs/Rooms/EmptyRoom");
        //Instantating the levelhelper which returns a levelgrid
        levelGenerationHelper = new LevelGenerationHelper(levelSize);
        //Place Rooms
        for (int i = 0; i < GeneralRoomPrefabs.Length; i++)
        {
            roomBounds[i] = GetChildrenRenderingBounds(GeneralRoomPrefabs[i]);
        }

        emptyRoomSize = emptyRoom.GetComponent<Renderer>().bounds.size;

        int halfGridSize = levelGenerationHelper.GridSize / 2;
            
        for(int i = 0; i < levelGenerationHelper.CreatedRooms.Count; i++)
        {
            switch(levelGenerationHelper.RoomPlacementGrid[(int)levelGenerationHelper.CreatedRooms[i].x, (int)levelGenerationHelper.CreatedRooms[i].y]){
                case 1: // Normal Rooms
                    int randomGeneralRoom = Random.Range(0, GeneralRoomPrefabs.Length);
                    rooms.Add(Instantiate(GeneralRoomPrefabs[randomGeneralRoom], new Vector3((levelGenerationHelper.CreatedRooms[i].x - halfGridSize) * roomBounds[randomGeneralRoom].size.x,
                        0, (levelGenerationHelper.CreatedRooms[i].y - halfGridSize)* roomBounds[randomGeneralRoom].size.z),
                        transform.rotation) as GameObject);
                    rooms[i].transform.parent = GameObject.Find("Rooms").transform;
                    rooms[i].GetComponent<RoomManager>().Initialize(levelGenerationHelper.CreatedRooms[i], i);
                    break;
                case 2: // Starting room
                    int randomStartingRoom = Random.Range(0, StartingRoomPrefabs.Length);
                    rooms.Add(Instantiate(StartingRoomPrefabs[randomStartingRoom], new Vector3((levelGenerationHelper.CreatedRooms[i].x - halfGridSize) * roomBounds[randomStartingRoom].size.x,
                        0, (levelGenerationHelper.CreatedRooms[i].y - halfGridSize) * roomBounds[randomStartingRoom].size.z),
                        transform.rotation) as GameObject);
                    rooms[i].transform.parent = GameObject.Find("Rooms").transform;
                    rooms[i].GetComponent<RoomManager>().Initialize(levelGenerationHelper.CreatedRooms[i], i);
                    break;
                case 3: // Boss Room
                    int randomBossRoom = Random.Range(0, BossRoomPrefabs.Length);
                    rooms.Add(Instantiate(BossRoomPrefabs[randomBossRoom], new Vector3((levelGenerationHelper.CreatedRooms[i].x - halfGridSize) * roomBounds[randomBossRoom].size.x,
                        0, (levelGenerationHelper.CreatedRooms[i].y - halfGridSize) * roomBounds[randomBossRoom].size.z),
                        transform.rotation) as GameObject);
                    rooms[i].transform.parent = GameObject.Find("Rooms").transform;
                    rooms[i].GetComponent<RoomManager>().Initialize(levelGenerationHelper.CreatedRooms[i], i);
                    break;
                case 4: // Loot room
                    int randomLootRoom = Random.Range(0, LootRoomPrefabs.Length);
                    rooms.Add(Instantiate(LootRoomPrefabs[randomLootRoom], new Vector3((levelGenerationHelper.CreatedRooms[i].x - halfGridSize) * roomBounds[randomLootRoom].size.x,
                        0, (levelGenerationHelper.CreatedRooms[i].y - halfGridSize) * roomBounds[randomLootRoom].size.z),
                        transform.rotation) as GameObject);
                    rooms[i].transform.parent = GameObject.Find("Rooms").transform;
                    rooms[i].GetComponent<RoomManager>().Initialize(levelGenerationHelper.CreatedRooms[i], i);
                    break;
            }

            
        }

        // Constraints for optimisation
        maxX = levelGenerationHelper.CreatedRooms[0].x;
        maxY = levelGenerationHelper.CreatedRooms[0].y;
        minX = levelGenerationHelper.CreatedRooms[0].x;
        minY = levelGenerationHelper.CreatedRooms[0].y;

        //Decide the constraints
        for (int i = 0; i < levelGenerationHelper.CreatedRooms.Count; i++)
        {
            if (maxX < levelGenerationHelper.CreatedRooms[i].x)
                maxX = levelGenerationHelper.CreatedRooms[i].x;
            if (maxY < levelGenerationHelper.CreatedRooms[i].y)
                maxY = levelGenerationHelper.CreatedRooms[i].y;
            if (minX > levelGenerationHelper.CreatedRooms[i].x)
                minX = levelGenerationHelper.CreatedRooms[i].x;
            if (minY > levelGenerationHelper.CreatedRooms[i].y)
                minY = levelGenerationHelper.CreatedRooms[i].y;
        }
        maxX++;
        maxY++;
        minX--;
        minY--;

        //Instantiate empty rooms
        for (int y = (int)minY; y <= maxY; y++)
        {
            for(int x = (int)minX; x <= (int)maxX; x++)
            {
                if(levelGenerationHelper.RoomPlacementGrid[x,y] == 0)
                {
                    var emptyRoomInstance = Instantiate(emptyRoom, new Vector3((x - halfGridSize) * emptyRoomSize.x,
                        0.99f, (y - halfGridSize) * emptyRoomSize.z), transform.rotation);
                    emptyRoomInstance.transform.parent = GameObject.Find("Rooms").transform;
                }
            }
        }
        
        //Open the correct doors
        for (int i = 0; i < levelGenerationHelper.CreatedRooms.Count; i++)
        {
            CheckDoors(i);
        }

    }
    //Checking were to place doors
    void CheckDoors(int index)
    {
        for(int direction = 0; direction < 4; direction++)
        {
            Vector2 resultingVector = levelGenerationHelper.CreatedRooms[index] + directions[direction];

            if (levelGenerationHelper.RoomPlacementGrid[(int)resultingVector.x, (int)resultingVector.y] == 0)
            {
                rooms[index].GetComponent<RoomManager>().PlaceDoorCover(direction);
            }
            else
            {
                rooms[index].GetComponent<RoomManager>().PlaceDoor(direction);
            }
        }
    }

    //Clearing all rooms
    //Used for reset
    void ClearAllRooms()
    {
        foreach (GameObject go in rooms)
        {
            Object.Destroy(go);
        }
    }

    //Gets the bounds of all the rooms
    Bounds GetChildrenRenderingBounds(GameObject go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
        if(renderers.Length > 1)
        {
            Bounds bounds = renderers[0].bounds;
            for (int i = 0, ni = renderers.Length; i < ni; i++)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }
            return bounds;
        }
        else
        {
            return new Bounds();
        }

    }

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.P))
        {
            ClearAllRooms();
            InstantiateRooms();
        }
	}
}
