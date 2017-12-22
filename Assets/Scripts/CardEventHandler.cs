using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ICardEventHandler : IEventSystemHandler
{
    void OnClick(Card card);
}

public class CardEventHandler : MonoBehaviour, ICardEventHandler
{
    [SerializeField]
    private float _waitSeconds = 1.0f;

    /// <summary>
    /// 2枚のカードを格納する配列
    /// </summary>
    private Card[] _cards;

    private PlayerHand[] _hands;

    private static CardEventHandler _instance;

    private CardEventHandler()
    {
        _cards = new Card[2];
        _hands = new PlayerHand[2];
    }

    public static CardEventHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                var gameObj = new GameObject("CardEventHandler");
                _instance = gameObj.AddComponent<CardEventHandler>();
            }

            return _instance;
        }
    }

    // CPU導入時に、引数でPlayer情報を渡す必要がある。
    public void OnClick(Card card)
    {
        if (TurnManager.Instance.NowTurn == PlayerType.Cpu)
        {
            return;
        }

        if (_cards[0] == null)
        {
            _cards[0] = card;
            _cards[0].Open();
        }
        else if (_cards[1] == null && card != _cards[0])
        {
            _cards[1] = card;
            _cards[1].Open();

            StartCoroutine("WaitAndJudgeCards");
        }
    }

    private IEnumerator WaitAndJudgeCards()
    {
        yield return new WaitForSeconds(_waitSeconds);

        if (_cards[0].IsSame(_cards[1]))
        {
            _cards[0].Picked = true;
            _cards[1].Picked = true;

            _hands[0].AddCard(_cards[0]);
            _hands[0].AddCard(_cards[1]);
        }
        else
        {
            _cards[0].Close();
            _cards[1].Close();
            TurnManager.Instance.NowTurn = PlayerType.Cpu;
        }
        Array.Clear(_cards, 0, 2);
    }

    void Start()
    {
        _hands[0] = GameObject.Find("/Field/Player1Field").GetComponent<PlayerHand>();
        _hands[1] = GameObject.Find("/Field/Player2Field").GetComponent<PlayerHand>();
    }

    void Update()
    {

    }
}
