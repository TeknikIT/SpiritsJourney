using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    public GameObject[] pickups;

    private void Start()
    {
        pickups = Resources.LoadAll<GameObject>("Prefabs/Loot/");

    }

    public GameObject RandomPotion()
    {
        return gameObject;
    }
}
