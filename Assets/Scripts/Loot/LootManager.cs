using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    public BuffItem[] itemPickups; //Items
    public GameObject[] itemContainers; //Containers with items
    public Consumable[] consumablePickups; //Pickups
    public GameObject[] consumalbeContainers; //Containers with consumables
    private bool hasBeenInitialized = false; // Checks if this class has been initialized

    /// <summary>
    /// Initializing the loot system
    /// </summary>
    private void Initialize()
    {
        //Loading all prefabs
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

    /// <summary>
    /// Returns a random item
    /// </summary>
    /// <returns>Random item</returns>
    public BuffItem RandomItem()
    {
        int i = Random.Range(0, itemPickups.Length);
        return itemPickups[i];
    }

    /// <summary>
    /// Returns a random consumable
    /// </summary>
    /// <returns></returns>
    public Consumable RandomConsumable()
    {
        int i = Random.Range(0, consumablePickups.Length);
        return consumablePickups[i];
    }

    /// <summary>
    /// Returns a random container with an item
    /// </summary>
    /// <returns>Random container with an item</returns>
    public GameObject RandomContainerWithItem()
    {
        //Check if the loot manager has been initialized
        if (!hasBeenInitialized)
        {
            Initialize();
        }
        int i = Random.Range(0, itemContainers.Length);
        return itemContainers[i];
    }

    /// <summary>
    /// Returns a random container with consumable
    /// </summary>
    /// <returns>Random container with consumable</returns>
    public GameObject RandomContainerWithConsumable()
    {
        //Check if the loot manager has been initialized
        if (!hasBeenInitialized)
        {
            Initialize();
        }
        int i = Random.Range(0, consumalbeContainers.Length);
        return consumalbeContainers[i];
    }
}
