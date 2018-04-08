using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Pickup handler for consumable
/// </summary>
public class PickUpConsumable : MonoBehaviour {

    public Consumable consumable;
    private Transform item;
    private GameObject consumableModel;
    private float count = 0;
    private float startY;


    private void Start()
    {
        item = transform.Find("Item"); //The gameobject under which the consumable will be instatiated
        consumable = LevelManager.instance.GetComponent<LootManager>().RandomConsumable(); //Get a random consumable
        consumableModel = Instantiate(consumable.item); //Instatiate in game world
        consumableModel.transform.parent = item; //Set parent
        consumableModel.transform.position = item.position; //Transform position
        startY = item.position.y; //Set y coordinate
    }


    private void Update()
    {
        consumableModel.transform.position = transform.Find("Item").position; //Continually set position
        item.position = new Vector3(item.position.x, startY + Mathf.Cos(count) / 10, item.position.z); //Make the item move according to a cosine function
        count += 0.1f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.instance.GetComponent<Character>().AddConsumableItem(consumable);
            Destroy(gameObject);
        }
    }
}
