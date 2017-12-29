using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [SerializeField]
    private float _cardMoveTime = 0.25f;

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

    public bool Picked
    {
        get;
        set;
    }

    private float CardMoveTime
    {
        get { return _cardMoveTime; }
        set { _cardMoveTime = value; }
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

    public void Clicked()
    {
        ExecuteEvents.Execute<ICardEventHandler>(
            CardEventHandler.Instance.gameObject,
            null,
            (handler, eventData) => handler.OnClick(this)
        );
    }

    public void OnMouseEnter()
    {
    #if UNITY_EDITOR
        Debug.Log(this);
    #endif
    }

    public void Open()
    {
        // TODO
        var sprites = Resources.LoadAll<Sprite>("Sprites/cards");
        var spriteName = ResolveSpriteName();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites
            .Where(sprite => sprite.name.Equals(spriteName))
            .First();

        Showed = true;
    }

    public void Close()
    {
        // TODO
        var sprites = Resources.LoadAll<Sprite>("Sprites/cards");
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites
            .Where(sprite => sprite.name.Equals("card_back_red"))
            .First();
    }

    public void Move(Vector3 to)
    {
        StartCoroutine(MoveUsingLerp(to));
    }

    private IEnumerator MoveUsingLerp(Vector3 to)
    {
        float CardMoveTime = 0.25f;
        float startTime = Time.time;

        Vector3 from = gameObject.transform.position;

        do{
            gameObject.transform.position = Vector3.Lerp(from, to, (Time.time - startTime) / CardMoveTime);
            yield return null;
        // ↓ 1.0fを足しこんでいるのは、中途半端な場所でMoveが止まらないようにするため。
        }while((CardMoveTime + 1.0f) > (Time.time - startTime));
    }

    private string ResolveSpriteName()
    {
        var path = "card_{0}_{1}";
        var suitMap = new Dictionary<CardSuit, string>(){
            {CardSuit.Club,    "c"},
            {CardSuit.Diamond, "d"},
            {CardSuit.Heart,   "h"},
            {CardSuit.Spade,   "s"},
        };

        return string.Format(path, Number, suitMap[Suit]);
    }

    void Start()
    {
        Showed = false;
        Picked = false;
    }

    void Update()
    {

    }

    public override string ToString()
    {
        return string.Format("N:{0} S:{1} Showed:{2}", Number, Suit, Showed);
    }
}
