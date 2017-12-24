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

    /// <summary>
    /// レベルを表すintとEnumのマップ(インスペクタ上で、Enumを値に設定できないため、やむなく定義)。
    /// </summary>
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

    /// <summary>
    /// カードをランダムで2枚ピックする。
    /// 
    /// ロジックメモ:
    ///     1枚目のカードを引く
    ///        if   2枚以上セットで見えているカードがある && 抽選勝利 -> そのセット中からカードをランダムピックする
    ///        else 完全にランダムピックする
    ///     2枚目のカードを引く
    ///         if   1枚目と同じナンバーのカードがある && 抽選勝利 -> その中からランダムピックする
    ///         else 完全にランダムピックする
    /// </summary>
    /// <returns></returns>
    private Card[] PickTwoCards()
    {
        Card[] restCards = _deck.RestCards();
        if (restCards.Length <= 1)
        {
            return new Card[0];
        }
        Card[] restShowedCards = _deck.RestShowedCards();

        // 確定ピックを行うか。
        bool pickShowedOne = UnityEngine.Random.Range(0, 100.0f) <= _levelAndPickRateMap[Level];

        // 1枚目のカードを引く。
        Card firstCard;
        IGrouping<int, Card>[] cardsPerNumber = restShowedCards
            .GroupBy(card => card.Number)
            .Where(val => val.Count() >= 2)
            .ToArray();
        if (pickShowedOne && cardsPerNumber.Length > 0)
        {
            int randIndex = UnityEngine.Random.Range(0, cardsPerNumber.Length);
            Card[] sameSuitCards = cardsPerNumber[randIndex].ToArray();
            firstCard = _deck.PickCardRandomlyFrom(sameSuitCards);
        }
        else
        {
            firstCard = _deck.PickCardRandomlyFrom(restCards);
        }

        // 2枚目のカードを引く。
        Card secondCard;
        Card sameSuitCard = _deck.PickSameNumberCardFrom(restShowedCards, firstCard);
        if (pickShowedOne && sameSuitCard != null)
        {
            secondCard = sameSuitCard;
        }
        else
        {
            secondCard = _deck.PickCardRandomlyFrom(restCards, firstCard);
        }

        return new Card[2]{firstCard, secondCard};
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
            TurnManager.Instance.SwitchTurn(PlayerType.Player);
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
        if (! _deck.Built)
            return;

        _picking = true;
        var cards = PickTwoCards();
        if (cards.Length > 0)
        {
            StartCoroutine(OpenTwoCards(cards));
        }
    }
}
