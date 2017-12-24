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
    private GameObject _player1TurnImage;

    private GameObject _player1Score;
    private GameObject _player2Panel;
    private GameObject _player2TurnImage;
    private GameObject _player2Score;
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

    public void ShowResultUIParts()
    {
        _startPanel.SetActive(true);

        var p1Score = int.Parse(_player1Score.GetComponent<Text>().text);
        var p2Score = int.Parse(_player2Score.GetComponent<Text>().text);

        var resultWord = "";
        if (p1Score > p2Score)
            resultWord = "You win !";
        else if (p1Score < p2Score)
            resultWord = "You lose";
        else
            resultWord = "Draw";

        _resultPanel.transform.FindChild("Text").GetComponent<Text>().text = resultWord;
        _resultPanel.SetActive(true);
    }

    public void HideResultUIParts()
    {
        _resultPanel.SetActive(false);
    }

    public void SwitchTurn(PlayerType toPlayerType)
    {
        var onImage = (toPlayerType == PlayerType.Player) ? _player1TurnImage : _player2TurnImage;
        var offImage = (toPlayerType == PlayerType.Player) ? _player2TurnImage : _player1TurnImage;

        onImage.GetComponent<Animator>().SetBool("TurnOn", true);
        offImage.GetComponent<Animator>().SetBool("TurnOn", false);
    }

    public void ChangeScore(PlayerType playerType, int score)
    {
        var scoreObj = (playerType == PlayerType.Player) ? _player1Score : _player2Score;
        scoreObj.GetComponent<Text>().text = score.ToString();
    }

    public void Reset()
    {
        _player1Score.GetComponent<Text>().text = "0";
        _player2Score.GetComponent<Text>().text = "0";

        _player1TurnImage.GetComponent<Animator>().SetBool("TurnOn", false);
        _player2TurnImage.GetComponent<Animator>().SetBool("TurnOn", false);
    }

    /// <summary>
    /// 一通りのUIパーツを取得しておき、一旦全て無効化する。
    /// </summary>
    void Awake()
    {
        _startPanel = GameObject.Find("/Canvas/StartPanel");
        _resultPanel = GameObject.Find("/Canvas/ResultPanel");
        _player1Panel = GameObject.Find("/Canvas/Player1Panel");
        _player2Panel = GameObject.Find("/Canvas/Player2Panel");
        _quitButton = GameObject.Find("/Canvas/QuitButton");

        _player1TurnImage = _player1Panel.transform.FindChild("TurnImage").gameObject;
        _player2TurnImage = _player2Panel.transform.FindChild("TurnImage").gameObject;

        _player1Score = _player1Panel.transform.FindChild("Score").FindChild("ScoreValue").gameObject;
        _player2Score = _player2Panel.transform.FindChild("Score").FindChild("ScoreValue").gameObject;

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
