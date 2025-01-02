using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CardGridNavigator : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private int cardsPerRow = 3;
    private int currentIndex = 0;
    private void Awake()
    {
        if (cards.Count > 0)
        {
            HighlightCard(currentIndex);
        }

        foreach (var card in cards)
        {
            Button button = card.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnCardClicked(card));
            }
        }
    }
    private void OnEnable()
    {
        InputManager.instance.OnActionTriggeredDown += HandleInputMove;
    }
    private void OnDisable()
    {
        InputManager.instance.OnActionTriggeredDown -= HandleInputMove;
    }
    private void HandleInputMove(InputManager.InputAction action)
    {
        if (InputManager.InputAction.Move == action)
        {
            Vector2 input = InputManager.instance.directionalLeftInput;

            if (input.x > 0)
            {
                Navigate(1);
            }
            else if (input.x < 0)
            {
                Navigate(-1);
            }
            else if (input.y > 0)
            {
                Navigate(-cardsPerRow);
            }
            else if (input.y < 0)
            {
                Navigate(cardsPerRow);
            }
        }
    }
    private void Navigate(int direction)
    {
        int newIndex = currentIndex + direction;

        if (newIndex >= 0 && newIndex < cards.Count)
        {
            UnhighlightCard(currentIndex);
            currentIndex = newIndex;
            HighlightCard(currentIndex);
            CenterOnCard(cards[currentIndex].GetComponent<RectTransform>());
        }
    }
    private void HighlightCard(int index)
    {
        var cardRenderer = cards[index].GetComponent<CardGridHandle>();
        if (cardRenderer != null)
        {
            cardRenderer.moved.enabled = true;
        }
    }
    private void UnhighlightCard(int index)
    {
        var cardRenderer = cards[index].GetComponent<CardGridHandle>();
        if (cardRenderer != null)
        {
            cardRenderer.moved.enabled = false;
        }
    }

    private void SelectedCard(int index)
    {
        var cardRenderer = cards[index].GetComponent<CardGridHandle>();
        if (cardRenderer != null)
        {
            cardRenderer.selected.enabled = !cardRenderer.selected.enabled;
        }
    }
    
    private void CenterOnCard(RectTransform card)
    {
        Debug.Log($" CenterOnCard name : {card.name} ");
        Canvas.ForceUpdateCanvases();

        float contentHeight = scrollRect.content.rect.height;
        float viewportHeight = scrollRect.viewport.rect.height;

        float cardCenterY = -card.localPosition.y;

        float targetY = cardCenterY - (viewportHeight / 2);

        targetY = Mathf.Clamp(targetY, 0, contentHeight - viewportHeight);

        scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, targetY);
    }
    private void OnCardClicked(GameObject card)
    {
        int clickedIndex = cards.IndexOf(card);

        if (clickedIndex >= 0)
        {
            if(clickedIndex == currentIndex)
                SelectedCard(clickedIndex);
            UnhighlightCard(currentIndex);
            currentIndex = clickedIndex;
            HighlightCard(currentIndex);
            CenterOnCard(cards[currentIndex].GetComponent<RectTransform>());
        }
    }
}
