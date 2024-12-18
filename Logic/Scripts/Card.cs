using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardImageRenderer;
    [SerializeField] private TextMeshPro cardTextRenderer;

    private CardSO _cardInfo;

    public void Init(CardSO cardSO)
    {
        _cardInfo = cardSO;
        cardImageRenderer.sprite = _cardInfo.cardImage;
        cardTextRenderer.text = _cardInfo.cardText;
    }

    private void OnMouseDown()
    {
        CardManager.Instance.SelectCard(_cardInfo);
    }
}
