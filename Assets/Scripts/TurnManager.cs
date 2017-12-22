using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    private static TurnManager _instance;

    public PlayerType NowTurn
    {
        get;
        set;
    }

    private TurnManager()
    {
    }

    public static TurnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var gameObj = new GameObject("TurnManager");
                _instance = gameObj.AddComponent<TurnManager>();
            }

            return _instance;
        }
    }

    public void SwitchTurn()
    {
        NowTurn = (NowTurn == PlayerType.Player)
            ? PlayerType.Cpu
            : PlayerType.Player;
    }

    void Start()
    {
        NowTurn = PlayerType.Player;
    }

    void Update()
    {

    }
}
