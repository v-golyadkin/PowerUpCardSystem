using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    [SerializeField] private GameObject cardSelectedUI;
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private Transform cardPositionOne;
    [SerializeField] private Transform cardPositionTwo;
    [SerializeField] private Transform cardPositionThree;

    [SerializeField] private List<CardSO> deck;

    private GameObject _cardOne, _cardTwo, _cardThree;

    List<CardSO> alreadySelectedCard = new List<CardSO>();

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
        }
    }

    public void SelectCard(CardSO selectedCard)
    {
        if (!alreadySelectedCard.Contains(selectedCard))
        {
            alreadySelectedCard.Add(selectedCard);
        }

        GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
    }

    public void ShowCardSelectionUI()
    {
        cardSelectedUI.SetActive(true);
    }

    public void HideCardSelectionUI()
    {
        cardSelectedUI.SetActive(false);
    }

    private void RandomizeCard()
    {
        if (_cardOne != null) Destroy(_cardOne);
        if (_cardTwo != null) Destroy(_cardTwo);
        if (_cardThree != null) Destroy(_cardThree);

        List<CardSO> randomizedCards = new List<CardSO>();

        List<CardSO> availableCard = new List<CardSO>(deck);
        availableCard.RemoveAll(card => 
            card.isUnique && alreadySelectedCard.Contains(card)
            || card.unlockLevel > GameManager.Instance.CurrentLevel
            );

        if (availableCard.Count < 3) 
        {
            Debug.Log("Not enough available cards");
            return;
        }

        while (randomizedCards.Count < 3)
        {
            CardSO randomizeCard = availableCard[Random.Range(0, availableCard.Count)];
            if (!randomizedCards.Contains(randomizeCard))
            {
                randomizedCards.Add(randomizeCard);
            }
        }

        _cardOne = InstantiateCard(randomizedCards[0], cardPositionOne);
        _cardTwo = InstantiateCard(randomizedCards[1], cardPositionTwo);
        _cardThree = InstantiateCard(randomizedCards[2], cardPositionThree);
    }

    private GameObject InstantiateCard(CardSO cardSO, Transform position)
    {
        GameObject cardGO = Instantiate(cardPrefab, position.position, Quaternion.identity, position);
        Card card = cardGO.GetComponent<Card>();
        card.Init(cardSO);
        return cardGO;
    }

    private void HandleGameStateChanged(GameManager.GameState state)
    {
        if(state == GameManager.GameState.CardSelection)
        {
            RandomizeCard();
        }
    }
}
