using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationManager : MonoBehaviour {
    #region Singleton
    public static LevelGenerationManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    private GameObject[] GeneralRoomPrefabs, BossRoomPrefabs, StartingRoomPrefabs;
    public int levelSize;
    private LevelGenerationHelper levelGenerationHelper;
    private Bounds[] roomBounds = new Bounds[100];
    private List<GameObject> rooms;
    private List< Vector2> directions;
    // Use this for initialization
    void Start () {
        directions = new List<Vector2>();
        directions.Add(Vector2.up);
        directions.Add(Vector2.right);
        directions.Add(Vector2.down);
        directions.Add(Vector2.left);
        InstantiateRooms();
    }

    public void Reload()
    {
        Transform room = GameObject.Find("Rooms").transform;
        foreach(Transform r in room)
        {
            Destroy(r.gameObject);
        }
        InstantiateRooms();
        PlayerManager.instance.transform.position = PlayerManager.instance.GetComponent<PlayerController>().originPosition;
        
    }

    public void InstantiateRooms()
    {
        rooms = new List<GameObject>();
        GeneralRoomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms/GeneralRooms");
        BossRoomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms/BossRooms");
        StartingRoomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms/StartingRooms");
        levelGenerationHelper = new LevelGenerationHelper(levelSize);
        //Place Rooms
        for (int i = 0; i < GeneralRoomPrefabs.Length; i++)
        {
            roomBounds[i] = GetChildrenRenderingBounds(GeneralRoomPrefabs[i]);
        }
       
        int halfGridSize = levelGenerationHelper.gridSize / 2;
            
        for(int i = 0; i < levelGenerationHelper.createdRooms.Count; i++)
        {
            switch(levelGenerationHelper.roomPlacementGrid[(int)levelGenerationHelper.createdRooms[i].x, (int)levelGenerationHelper.createdRooms[i].y]){
                case 1:
                    int randomGeneralRoom = Random.Range(0, GeneralRoomPrefabs.Length);
                    rooms.Add(Instantiate(GeneralRoomPrefabs[randomGeneralRoom], new Vector3((levelGenerationHelper.createdRooms[i].x - halfGridSize) * roomBounds[randomGeneralRoom].size.x,
                        0, (levelGenerationHelper.createdRooms[i].y - halfGridSize)* roomBounds[randomGeneralRoom].size.z),
                        transform.rotation) as GameObject);
                    rooms[i].transform.parent = GameObject.Find("Rooms").transform;
                    rooms[i].GetComponent<RoomManager>().Initialize();
                    break;
                case 2:
                    int randomStartingRoom = Random.Range(0, StartingRoomPrefabs.Length);
                    rooms.Add(Instantiate(StartingRoomPrefabs[randomStartingRoom], new Vector3((levelGenerationHelper.createdRooms[i].x - halfGridSize) * roomBounds[randomStartingRoom].size.x,
                        0, (levelGenerationHelper.createdRooms[i].y - halfGridSize) * roomBounds[randomStartingRoom].size.z),
                        transform.rotation) as GameObject);
                    rooms[i].transform.parent = GameObject.Find("Rooms").transform;
                    rooms[i].GetComponent<RoomManager>().Initialize();
                    break;
                case 3:
                    Debug.Log("placed boss");
                    int randomBossRoom = Random.Range(0, BossRoomPrefabs.Length);
                    rooms.Add(Instantiate(BossRoomPrefabs[randomBossRoom], new Vector3((levelGenerationHelper.createdRooms[i].x - halfGridSize) * roomBounds[randomBossRoom].size.x,
                        0, (levelGenerationHelper.createdRooms[i].y - halfGridSize) * roomBounds[randomBossRoom].size.z),
                        transform.rotation) as GameObject);
                    rooms[i].transform.parent = GameObject.Find("Rooms").transform;
                    rooms[i].GetComponent<RoomManager>().Initialize();
                    break;
            }
            
        }
        
        //Open the correct doors
        for (int i = 0; i < levelGenerationHelper.createdRooms.Count; i++)
        {
            CheckDoors(i);
        }

    }

    void CheckDoors(int index)
    {
        for(int direction = 0; direction < 4; direction++)
        {
            Vector2 resultingVector = levelGenerationHelper.createdRooms[index] + directions[direction];

            if (levelGenerationHelper.roomPlacementGrid[(int)resultingVector.x, (int)resultingVector.y] == 0)
            {
                rooms[index].GetComponent<RoomManager>().placeDoorCover(direction);
            }
            else
            {
                rooms[index].GetComponent<RoomManager>().placeDoor(direction);
            }
        }
    }

    void ClearAllRooms()
    {
        foreach (GameObject go in rooms)
        {
            Object.Destroy(go);
        }
    }

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
