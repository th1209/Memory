using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPositioner : MonoBehaviour
{
    [SerializeField]
    private Vector2 _initialCardPos;

    [SerializeField]
    private Vector2 _currentCardPos;

    [SerializeField]
    private Vector2 _cardOffset;

    [SerializeField]
    private int _cardNumPerLine = 10;

    private int _cardCount = 0;

    public Vector2 InitialCardPos
    {
        get { return _initialCardPos; }
    }

    public Vector2 CurrentCardPos
    {
        get { return _currentCardPos; }
        set { _currentCardPos = value; }
    }

    public Vector2 CardOffset
    {
        get { return _cardOffset; }
    }

    public int CardNumPerLine
    {
        get { return _cardNumPerLine; }
    }

    public void UpdateCardPos()
    {
        _cardCount++;
        if (_cardCount % _cardNumPerLine == 0)
            UpdateCardPos(InitialCardPos.x, CurrentCardPos.y + CardOffset.y);
        else
            UpdateCardPos(CurrentCardPos.x + CardOffset.x, CurrentCardPos.y);
    }

    public void UpdateCardPos(Vector2 v)
    {
        UpdateCardPos(v.x, v.y);
    }

    public void UpdateCardPos(float x, float y)
    {
        var newPos = new Vector2(x, y);
        CurrentCardPos = newPos;
    }

    public void Reset()
    {
        _currentCardPos = _initialCardPos;
    }
}
