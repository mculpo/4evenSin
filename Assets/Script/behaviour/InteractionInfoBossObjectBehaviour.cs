using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionInfoBossObjectBehaviour : MonoBehaviour
{
    public Transform refPositionIcon;
    public BossInfoScreenData infoBossData;
    [SerializeField] private BossInfoDisplay bossInfoDisplay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossInfoDisplay.SetBossInfoScreenData(infoBossData);
            UIInteractiveIconManager.instance.OnEnableImageInteractive(refPositionIcon, bossInfoDisplay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIInteractiveIconManager.instance.OnDisableImageInteractive(refPositionIcon);
        }
    }
}
