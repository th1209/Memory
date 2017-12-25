using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager _instance;

    private UIManager _uiManager;

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

    public void SwitchTurn(PlayerType toPlayerType)
    {
        NowTurn = toPlayerType;
        _uiManager.SwitchTurn(NowTurn);
    }

    void Awake()
    {
        NowTurn = PlayerType.Player;
        _uiManager = GameObject.Find("/UIManager").GetComponent<UIManager>();
    }

    void Update()
    {

    }
}
