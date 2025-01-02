using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharaterStatsData", menuName = "Game/Charater Stats")]
public class CharacterStatsData : ScriptableObject
{
    [Header("Base Stats")]
    [Range(1, 100)]
    public int baseHealth;
    [Range(1, 100)]
    public float basePower;
    [Range(1, 100)]
    public float baseDefense;
    [Range(1, 100)]
    public float baseSpeed;

    [Header("Max Stats After Mods")]
    [Range(1, 100)]
    public int maxHealth;
    [Range(1, 100)]
    public float maxPower;
    [Range(1, 100)]
    public float maxDefense;
    [Range(1, 100)]
    public float maxSpeed;
}
