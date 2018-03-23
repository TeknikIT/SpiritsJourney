using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class BuffItem : ScriptableObject {

    public new string name;

    public int healthIncrease;
    public float speedIncrease;
    public float cooldownDecrease;

    public GameObject item;

}
