using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    // public double CurrentScore { get; set; }

    // public Card[] Cards { get; }

    private int _cardNum = 0;

    public void AddCard(GameObject card)
    {
        var curPos = gameObject.GetComponent<CardPositioner>().CurrentCardPos;
        var positioner = gameObject.GetComponent<CardPositioner>();
        
        card.transform.position = new Vector3(positioner.CurrentCardPos.x, positioner.CurrentCardPos.y, 0);
        card.transform.parent = gameObject.transform;

        card.GetComponent<Renderer>().sortingOrder = _cardNum;

        positioner.UpdateCardPos();

        // TODO AddScoreする。

        _cardNum++;
    }

    public void AddScore()
    {

    }
}
