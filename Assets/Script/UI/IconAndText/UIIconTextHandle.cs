using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIIconTextHandle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Image image;
    [SerializeField] private IconAndTextData iconAndTextData;

    void Start()
    {
        textMeshPro.text = iconAndTextData.text;
        image.sprite = iconAndTextData.icon;
    }
}
