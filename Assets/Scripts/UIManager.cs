using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject _startPanel;
    private GameObject _resultPanel;
    private GameObject _player1Panel;
    private GameObject _player2Panel;
    private GameObject _quitButton;

    public void ShowStartUIParts()
    {
        _startPanel.SetActive(true);
    }

    public void HideStartUIParts()
    {
        _startPanel.SetActive(false);
    }

    public void ShowGameUIParts()
    {
        _player1Panel.SetActive(true);
        _player2Panel.SetActive(true);
        _quitButton.SetActive(true);
    }

    public void HideGameUIParts()
    {
        _player1Panel.SetActive(false);
        _player2Panel.SetActive(false);
        _quitButton.SetActive(false);
    }

    public void ShowResultUIParts(PlayerType winPlayer)
    {
        _startPanel.SetActive(true);

        var resultWord = (winPlayer == PlayerType.Player)
            ? "You win !"
            : "You lose";
        _resultPanel.transform.FindChild("Text").GetComponent<Text>().text = resultWord;
        _resultPanel.SetActive(true);
    }

    public void HideResultUIParts()
    {
        _resultPanel.SetActive(false);
    }

    public void AddScore(PlayerType playerType, int score)
    {

    }

    public void ResetScore()
    {
        
    }

    /// <summary>
    /// 一通りのUIパーツを取得しておき、一旦全て無効化する。
    /// </summary>
    void Start()
    {
        _startPanel = GameObject.Find("/Canvas/StartPanel");
        _resultPanel = GameObject.Find("/Canvas/ResultPanel");
        _player1Panel = GameObject.Find("/Canvas/Player1Panel");
        _player2Panel = GameObject.Find("/Canvas/Player2Panel");
        _quitButton = GameObject.Find("/Canvas/QuitButton");

        _startPanel.SetActive(false);
        _resultPanel.SetActive(false);
        _player1Panel.SetActive(false);
        _player2Panel.SetActive(false);
        _quitButton.SetActive(false);
    }


    void Update()
    {

    }
}
