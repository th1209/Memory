using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject _deck;
    private GameObject _p1Field;
    private GameObject _p2Field;

    void Start()
    {
        _deck = GameObject.Find("/Field/Deck");
        _p1Field = GameObject.Find("/Field/Player1Field");
        _p2Field = GameObject.Find("/Field/Player2Field");

        _deck.GetComponent<Deck>().BuildCards(
            Enumerable.Range(1, 10).ToArray(),
            new CardSuit[]{CardSuit.Club, CardSuit.Diamond, CardSuit.Heart, CardSuit.Spade}
        );

        // 以下、カード移動用の検証コード。
        var cards = _deck.GetComponent<Deck>().GetCards();
        for (int i = 0; i < cards.Count() - 10; i++)
        {
            var card = cards[i];
            _p1Field.GetComponent<PlayerHand>().AddCard(card);
        }
    }

    void Update()
    {

    }
}
