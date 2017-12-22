using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Cpu : MonoBehaviour
{
    [SerializeField]
    private float _waitSeconds = 1.0f;

    [SerializeField]
    private CpuLevel _level;

    private Deck _deck;

    private PlayerHand _hand;

    private bool _picking;

    private Dictionary<CpuLevel, float> _levelAndPickRateMap;
    private Dictionary<int, CpuLevel> _numberAndLevelMap;

    public void SetLevelByInt(int level)
    {
        if (level < 0 || level > 2)
            throw new ArgumentException("");
        Level = _numberAndLevelMap[level];
    }

    public CpuLevel Level
    {
        get;
        set;
    }

    private Card[] PickTwoCards()
    {
        var cards = new Card[0];
        if (UnityEngine.Random.Range(0, 100.0f) <= _levelAndPickRateMap[Level])
        {
            Debug.Log("PickShowedCards");
            cards = PickTwoShowedCards();
        }
        if (cards.Length == 0)
        {
            cards = PickTwoRandomCards();
        }
        return cards;
    }

    private Card[] PickTwoRandomCards()
    {
        return PickTwoCardsFrom(_deck.RestCards());
    }

    private Card[] PickTwoCardsFrom(Card[] cards)
    {
        var first = UnityEngine.Random.Range(0, cards.Length);
        var second = 0;
        do
        {
            second = UnityEngine.Random.Range(0, cards.Length);
        } while (second == first);

        return new Card[2]{
            cards[first],
            cards[second],
        };
    }

    private Card[] PickTwoShowedCards()
    {
        var cards = _deck.RestShowedCards();
        if (cards.Length <= 2)
            return new Card[0];

        var cardsPerNumber = cards
            .GroupBy(card => card.Number)
            .Where(val => val.Count() >= 2)
            .ToArray();

        if (cardsPerNumber.Length <= 0)
            return new Card[0];

        var randIndex = UnityEngine.Random.Range(0, cardsPerNumber.Length);
        var sameSuitCards = cardsPerNumber[randIndex].ToArray();
        return PickTwoCardsFrom(sameSuitCards);
    }

    private IEnumerator OpenTwoCards(Card[] cards)
    {
        yield return new WaitForSeconds(_waitSeconds);

        cards[0].Open();

        yield return new WaitForSeconds(_waitSeconds);

        cards[1].Open();

        yield return new WaitForSeconds(_waitSeconds);

        if (cards[0].IsSame(cards[1]))
        {
            cards[0].Picked = true;
            cards[1].Picked = true;

            _hand.AddCard(cards[0]);
            _hand.AddCard(cards[1]);
        }
        else
        {
            cards[0].Close();
            cards[1].Close();
            TurnManager.Instance.NowTurn = PlayerType.Player;
        }

        _picking = false;
    }

    void Start()
    {
        Level = CpuLevel.Normal;
        _picking = false;
        _levelAndPickRateMap = new Dictionary<CpuLevel, float>(){
            {CpuLevel.Easy,   25.0f},
            {CpuLevel.Normal, 50.0f},
            {CpuLevel.Hard,   75.0f},
        };
        _numberAndLevelMap = new Dictionary<int, CpuLevel>(){
            {0, CpuLevel.Easy},
            {1, CpuLevel.Normal},
            {2, CpuLevel.Hard},
        };
        _deck = GameObject.Find("/Field/Deck").GetComponent<Deck>();
        _hand = GameObject.Find("/Field/Player2Field").GetComponent<PlayerHand>();

    }
    void Update()
    {
        if (TurnManager.Instance.NowTurn != PlayerType.Cpu || _picking)
            return;
        if (! _deck.Built || _deck.RestCards().Length < 2)
            return;

        _picking = true;
        var cards = PickTwoCards();
        StartCoroutine(OpenTwoCards(cards));
    }
}
