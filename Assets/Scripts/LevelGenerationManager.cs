using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationManager : MonoBehaviour {
    public GameObject[] RoomPrefabs;
    private LevelGenerationHelper levelGenerationHelper;
    private Bounds[] roomBounds = new Bounds[100];
    private List<GameObject> rooms;
    // Use this for initialization
    void Start () {
        InstantiateRooms();   
	}

    void InstantiateRooms()
    {
        rooms = new List<GameObject>();
        RoomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms");
        levelGenerationHelper = new LevelGenerationHelper(10);
        //Place Rooms
        for (int i = 0; i < RoomPrefabs.Length; i++)
        {
            roomBounds[i] = GetChildrenRenderingBounds(RoomPrefabs[i]);
        }
        for (int y = 0; y < levelGenerationHelper.gridSize; y++)
            for (int x = 0; x < levelGenerationHelper.gridSize; x++)
            {
                if (levelGenerationHelper.roomPlacementGrid[x, y] == 1)
                {
                    int randomRoom = Random.Range(0, RoomPrefabs.Length);
                    rooms.Add(Instantiate(RoomPrefabs[randomRoom], new Vector3(x * roomBounds[randomRoom].size.x, 0, y * roomBounds[randomRoom].size.z), transform.rotation) as GameObject);
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
		if(Input.GetKeyUp(KeyCode.Space))
        {
            ClearAllRooms();
            InstantiateRooms();
        }
	}
}
