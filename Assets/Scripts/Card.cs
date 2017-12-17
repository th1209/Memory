using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardSuit
{
    Club,
    Diamond,
    Heart,
    Spade,
}

public class Card : MonoBehaviour
{
    [SerializeField]
    private int _number;
    [SerializeField]
    private CardSuit _suit;

    public int Number
    {
        get { return _number; }
        set { _number = value; }
    }
    public CardSuit Suit
    {
        get { return _suit; }
        set { _suit = value; }
    }

    public int Index
    {
        get;
        set;
    }

    public bool Showed
    {
        get;
        set;
    }

    public void Initialize(int n, CardSuit s)
    {
        if (n < 1 || n > 13)
            throw new ArgumentException("");

        Number = n;
        Suit = s;
    }

    public bool IsSame(Card other)
    {
        return Number == other.Number;
    }

    void Start()
    {
        Showed = false;
    }
}
