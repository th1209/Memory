using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Deck _deck;
    private UIManager _uiManager;
    private TurnManager _turnManager;

    public void ToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    void Start()
    {
        _deck = GameObject.Find("/Field/Deck").GetComponent<Deck>();
        _uiManager = GameObject.Find("/UIManager").GetComponent<UIManager>();
        _turnManager = TurnManager.Instance;

        InitGame();
    }

    void Update()
    {
        if (_deck.Built && _deck.RestCards().Count() == 0)
        {
            _uiManager.ShowResultUIParts();
        }
    }

    private void InitGame()
    {
        _uiManager.HideResultUIParts();
        _uiManager.ShowGameUIParts();

        _turnManager.SwitchTurn(PlayerType.Player);

        _deck.BuildCards(
            Enumerable.Range(1, 10).ToArray(),
            new CardSuit[] { CardSuit.Club, CardSuit.Diamond, CardSuit.Heart, CardSuit.Spade }
            // Enumerable.Range(1, 4).ToArray(),
            // new CardSuit[] { CardSuit.Club, CardSuit.Diamond,}
        );
    }
}
