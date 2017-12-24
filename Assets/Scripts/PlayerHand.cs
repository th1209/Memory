using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public int Score { get; set; }

    private int _cardNum = 0;

    private UIManager _uiManager;

    [SerializeField]
    private PlayerType _playerType = PlayerType.Player;

    public void AddCard(Card card)
    {
        var positioner = gameObject.GetComponent<CardPositioner>();

        card.Move(new Vector3(positioner.CurrentCardPos.x, positioner.CurrentCardPos.y, 0.0f));
        card.gameObject.transform.parent = gameObject.transform;

        card.gameObject.GetComponent<Renderer>().sortingOrder = _cardNum;

        positioner.UpdateCardPos();

        Score++;
        _uiManager.ChangeScore(_playerType, Score);

        _cardNum++;
    }
    public void Reset()
    {
        Score = 0;
        gameObject.GetComponent<CardPositioner>().Reset();
    }

    void Start()
    {
        _uiManager = GameObject.Find("/UIManager").GetComponent<UIManager>();
    }
}
