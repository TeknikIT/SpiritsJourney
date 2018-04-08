using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pedistal with item
/// </summary>
public class PickUpPedistal : MonoBehaviour {

    public BuffItem buffItem;
    private Transform item;
    private GameObject itemModel;
    private float count = 0;
    private float startY;


    private void Start()
    {
        item = transform.Find("Item"); // The gameobject under which the consumable will be instantiated
        buffItem = LevelManager.instance.GetComponent<LootManager>().RandomItem(); // Get a random item
        itemModel = Instantiate(buffItem.item); // Instantiate in game world
        itemModel.transform.parent = item; // Set parent
        itemModel.transform.position = item.position; // Set position
        startY = item.position.y; // Set y coordinate
    }
    private void Update()
    {
        itemModel.transform.position = transform.Find("Item").position; //Continually set position
        item.position = new Vector3(item.position.x, startY + Mathf.Cos(count)/10, item.position.z); // Move the item according to a cosine function
        count += 0.1f;


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.instance.GetComponent<Character>().AddPickupItem(buffItem);
            Destroy(gameObject);
        }
    }
}
