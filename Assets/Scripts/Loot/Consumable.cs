using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Contains the stats for a consumable
/// </summary>
[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable")]
public class Consumable : ScriptableObject {

    public new string name;

    public int healthIncrease;
    public float speedIncrease;
    public float cooldownDecrease;

    public GameObject item;
}
