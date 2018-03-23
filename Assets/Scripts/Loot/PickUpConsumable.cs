using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpConsumable : MonoBehaviour {

    public Consumable consumable;
    private Transform item;
    private GameObject consumableModel;
    private float count = 0;
    private float startY;


    private void Start()
    {
        item = transform.Find("Item");
        consumable = LevelManager.instance.GetComponent<LootManager>().RandomConsumable();
        consumableModel = Instantiate(consumable.item);
        consumableModel.transform.parent = item;
        consumableModel.transform.position = item.position;
        startY = item.position.y;
    }
    private void Update()
    {
        consumableModel.transform.position = transform.Find("Item").position;
        item.position = new Vector3(item.position.x, startY + Mathf.Cos(count) / 10, item.position.z);
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
