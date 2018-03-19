using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    public BuffItem[] pickups;
    public GameObject[] lootContainer;

    private void Start()
    {
        pickups = Resources.LoadAll<BuffItem>("Prefabs/Loot/ItemInfo");
        lootContainer = Resources.LoadAll<GameObject>("Prefabs/Loot/LootContainer");
    }

    public BuffItem RandomPotion()
    {
        int i = Random.Range(0, pickups.Length);
        return pickups[i];
    }

    public GameObject RandomContainer()
    {
        int i = Random.Range(0, lootContainer.Length);
        return lootContainer[i];
    }
}
