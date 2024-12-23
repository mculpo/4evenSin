
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewComboData", menuName = "Game/Combos/ComboData", order = 1)]
public class ComboData : ScriptableObject
{
    public string comboName;
    public List<AnimationClip> animationClips;
}