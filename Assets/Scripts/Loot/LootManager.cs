using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    public BuffItem[] itemPickups;
    public GameObject[] itemContainers;
    public Consumable[] consumablePickups;
    public GameObject[] consumalbeContainers;
    private bool hasBeenInitialized = false;

    private void Initialize()
    {
        itemPickups = Resources.LoadAll<BuffItem>("Prefabs/Loot/Items/ItemInfo");
        itemContainers = Resources.LoadAll<GameObject>("Prefabs/Loot/Items/ItemContainer");
        consumablePickups = Resources.LoadAll<Consumable>("Prefabs/Loot/Consumables/ConsumableInfo");
        consumalbeContainers = Resources.LoadAll<GameObject>("Prefabs/Loot/Consumables/ConsumableContainer");

        hasBeenInitialized = true;
    }

    private void Start()
    {
        Initialize();
    }

    public BuffItem RandomItem()
    {
        int i = Random.Range(0, itemPickups.Length);
        return itemPickups[i];
    }

    public Consumable RandomConsumable()
    {
        int i = Random.Range(0, consumablePickups.Length);
        return consumablePickups[i];
    }

    public GameObject RandomContainerWithItem()
    {
        if (!hasBeenInitialized)
        {
            Initialize();
        }
        int i = Random.Range(0, itemContainers.Length);
        return itemContainers[i];
    }
    
    public GameObject RandomContainerWithConsumable()
    {
        if (!hasBeenInitialized)
        {
            Initialize();
        }
        int i = Random.Range(0, consumalbeContainers.Length);
        return consumalbeContainers[i];
    }
}
