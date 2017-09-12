using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationManager : MonoBehaviour {
    public GameObject[] RoomPrefabs;
    private LevelGenerationHelper levelGenerationHelper;
    private Bounds[] roomBounds = new Bounds[100];
    // Use this for initialization
    void Start () {
        RoomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Rooms");
        levelGenerationHelper= new LevelGenerationHelper(10, 10);
        //Place Rooms
        for(int i = 0; i < RoomPrefabs.Length; i++)
        {
            roomBounds[i] = GetChildrenRenderingBounds(RoomPrefabs[i]);
        }
        for(int y = 0; y < levelGenerationHelper.gridSize; y++)
            for(int x = 0; x < levelGenerationHelper.gridSize; x++)
            {
                if(levelGenerationHelper.roomPlacementGrid[x,y] == 1)
                {
                    Instantiate(RoomPrefabs[0], new Vector3(x*20, 0, y*12), transform.rotation);
                }
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
		
	}
}
