using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCardObjectBehaviour : MonoBehaviour
{
    public Transform refPositionIcon;
    [SerializeField] private CardInfoDisplay interactiveCardInfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIInteractiveIconManager.instance.OnEnableImageInteractive(refPositionIcon, interactiveCardInfo);
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
