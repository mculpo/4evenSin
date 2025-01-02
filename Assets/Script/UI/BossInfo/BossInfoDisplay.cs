using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInfoDisplay : MonoBehaviour, IItemDisplay
{
    [SerializeField] private UIBossInfoHandle uIBossInfoBehaviour;
    [SerializeField] private GameObject bossInfoGameObject;
    private BossInfoScreenData infoBossData;

    public void OnDisplayInfo()
    {
        uIBossInfoBehaviour.ApplyInfoBoss(infoBossData);
        bossInfoGameObject.SetActive(true);
    }
    public void OffDisplayInfo()
    {
        bossInfoGameObject.SetActive(false);
    }
    public void SetBossInfoScreenData(BossInfoScreenData infoBossData)
    {
        this.infoBossData = infoBossData;
    }

    public bool IsActive()
    {
        return bossInfoGameObject.activeSelf;
    }
}
