using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Boss Info", menuName = "Game/UI/Boss Info", order = 1)]
public class BossInfoScreenData : ScriptableObject
{
    public string infoText;
    public Sprite imageBoss;
}
