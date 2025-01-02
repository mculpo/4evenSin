
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewComboGroupData", menuName = "Game/Combos/ComboGroupData", order = 1)]
public class ComboGroupData : ScriptableObject
{
    public List<ComboData> lightCombos;
    public List<ComboData> heavyCombos;
}