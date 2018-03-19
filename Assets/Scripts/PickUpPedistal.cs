using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPedistal : MonoBehaviour {

    public BuffItem buffItem;
    private Transform item;
    private GameObject itemModel;
    private float count = 0;
    private float startY;


    private void Start()
    {
        item = transform.Find("Item");
        buffItem = LevelManager.instance.GetComponent<LootManager>().RandomPotion();
        itemModel = Instantiate(buffItem.item);
        itemModel.transform.parent = item;
        itemModel.transform.position = item.position;
        startY = item.position.y;
    }
    private void Update()
    {
        itemModel.transform.position = transform.Find("Item").position;
        item.position = new Vector3(item.position.x, startY + Mathf.Cos(count)/10, item.position.z);
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
