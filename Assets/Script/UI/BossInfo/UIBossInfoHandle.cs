using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBossInfoHandle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    
    public void ApplyInfoBoss(BossInfoScreenData bossInfoScreenData)
    {
        text.text = bossInfoScreenData.infoText;
        image.sprite = bossInfoScreenData.imageBoss;
    }

    public void BossLoadScene()
    {
        LoadSceneManager.instance.LoadScene("BattleScene");
        GameManager.instance.SetGameState(GameState.Playing);
        GameManager.instance.SetPlayerState(PlayerState.All);
    }
}
