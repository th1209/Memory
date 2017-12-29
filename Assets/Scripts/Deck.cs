using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private GameObject _cardPrefab;

    private Card[] _cards;

    // 初期化済みフラグ。
    public bool Built
    {
        get;
        set;
    }

    public GameObject CardPrefab
    {
        get { return _cardPrefab; }
    }

    /// <summary>
    /// デッキの初期化。
    /// </summary>
    /// <param name="numbers"></param>
    /// <param name="suitArray"></param>
    public void BuildCards(int[] numbers, CardSuit[] suitArray)
    {
        if (suitArray.Count() != 2 && suitArray.Count() != 4)
            throw new ArgumentException("");

        var cardNum = numbers.Count() * suitArray.Count();
        _cards = new Card[cardNum];

        AlignCards(cardNum);
        ShuffleAndAssign(numbers, suitArray);

        Built = true;
    }

    /// <summary>
    /// カードを初期化し、画面に配置する。
    /// </summary>
    /// <param name="cardNum"></param>
    private void AlignCards(int cardNum)
    {
        var positioner = gameObject.GetComponent<CardPositioner>();

        for (int i = 0; i < cardNum; i++)
        {
            var curPos = positioner.CurrentCardPos;

            var cardObj = Instantiate(CardPrefab, new Vector3(curPos.x, curPos.y, 0), Quaternion.identity);
            cardObj.transform.parent = gameObject.transform;
            _cards[i] = cardObj.GetComponent<Card>();

            positioner.UpdateCardPos();
        }
    }

    /// <summary>
    /// デッキをシャッフルし、各カードに番号とスートを割り振る。
    /// </summary>
    private void ShuffleAndAssign(int[] numbers, CardSuit[] suitArray)
    {
        _cards = _cards.OrderBy(card => Guid.NewGuid()).ToArray();
        int current = 0;
        foreach (var number in numbers)
        {
            foreach (var suit in suitArray)
            {
                _cards[current].Initialize(number, suit);
                current++;
            }
        }
    }

    public Card[] RestCards()
    {
        return _cards
            .Where((card) => { return !card.Picked; })
            .ToArray();
    }

    public Card[] ShowedCards()
    {
        return _cards
            .Where((card) => { return card.Showed; })
            .ToArray();
    }

    public Card[] RestShowedCards()
    {
        return _cards
            .Where((card) => { return card.Showed && !card.Picked; })
            .ToArray();
    }

    public Card PickSameNumberCardFrom(Card[] cards, Card one)
    {
        Card[] sameSuitCards = cards
            .Where((card) => { return card.Number == one.Number && card.Suit != one.Suit; })
            .ToArray();
        return PickCardRandomlyFrom(sameSuitCards);
    }

    public Card PickCardRandomlyFrom(Card[] cards, Card excludeOne = null)
    {
        if (cards.Length == 0)
            return null;

        Card card;
        do
        {
            int randIndex = UnityEngine.Random.Range(0, cards.Length);
            card = cards[randIndex];
        } while (excludeOne != null && card.Number == excludeOne.Number && card.Suit == excludeOne.Suit);
        return card;
    }

    // public void Reset()
    // {
    //     if (!Built)
    //         return;

    //     foreach (var card in _cards)
    //     {
    //         GameObject.DestroyImmediate(card.gameObject);
    //     }

    //     gameObject.GetComponent<CardPositioner>().Reset();

    //     Built = false;
    // }

    void Awake()
    {
        Built = false;
    }
}
