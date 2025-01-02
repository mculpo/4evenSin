using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfoDisplay : MonoBehaviour, IItemDisplay
{
    [SerializeField] private GameObject UICardInfoObject;

    public bool IsActive()
    {
        return UICardInfoObject.activeSelf;
    }

    public void OffDisplayInfo()
    {
        UICardInfoObject.SetActive(false);
    }

    public void OnDisplayInfo()
    {
        UICardInfoObject.SetActive(true);
    }
}
