using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Deck _deck;
    private PlayerHand _p1Field;
    private PlayerHand _p2Field;
    private UIManager _uiManager;
    private TurnManager _turnManager;

    void Start()
    {
        _deck = GameObject.Find("/Field/Deck").GetComponent<Deck>();
        _p1Field = GameObject.Find("/Field/Player1Field").GetComponent<PlayerHand>();
        _p2Field = GameObject.Find("/Field/Player2Field").GetComponent<PlayerHand>();
        _uiManager = GameObject.Find("/UIManager").GetComponent<UIManager>();
        _turnManager = TurnManager.Instance;

        _uiManager.ShowStartUIParts();
    }

    void Update()
    {
        if (_deck.Built && _deck.RestCards().Count() == 0)
        {
            _uiManager.HideGameUIParts();
            _uiManager.ShowStartUIParts();
            _uiManager.ShowResultUIParts();
        }
    }

    public void InitGame()
    {
        // 2回目以降のスタート用。
        ResetObjects();

        _uiManager.HideStartUIParts();
        _uiManager.HideResultUIParts();
        _uiManager.ShowGameUIParts();

        _turnManager.SwitchTurn(PlayerType.Player);

        _deck.BuildCards(
            // Enumerable.Range(1, 10).ToArray(),
            // new CardSuit[] { CardSuit.Club, CardSuit.Diamond, CardSuit.Heart, CardSuit.Spade }
            Enumerable.Range(1, 4).ToArray(),
            new CardSuit[] { CardSuit.Club, CardSuit.Diamond,}
        );
    }

    public void QuitGame()
    {
        ResetObjects();
        _uiManager.HideGameUIParts();
        _uiManager.ShowStartUIParts();
    }

    private void ResetObjects()
    {
        _uiManager.Reset();
        _deck.Reset();
        _p1Field.Reset();
        _p2Field.Reset();
    }
}
