using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AtributeData", menuName = "Game/Atributes")]
public class AtributeData : ScriptableObject
{
    [Header("Base Stats")]
    public int health;
    public float strength;
    public float defense;

    [Header("Modification Values")]
    [Range(1, 100)]
    public int minHealth;
    [Range(1, 100)]
    public int maxHealth;
    [Range(1, 100)]
    public float minStrength;
    [Range(1, 100)]
    public float maxStrength;
    [Range(1, 100)]
    public float minDefense;
    [Range(1, 100)]
    public float maxDefense;
}
