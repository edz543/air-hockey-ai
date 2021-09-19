using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string playerWinMessage, opponentWinMessage;
    public int playerScore = 0, opponentScore = 0, maxScore = 5;

    public GameObject gameOverPanel;
    public Text gameOverText;
    public Text playerScoreText, opponentScoreText;

    private void Awake()
    {
        playerScoreText.text = playerScore + "/" + maxScore;
        opponentScoreText.text = opponentScore + "/" + maxScore;
    }

    public void IncreaseScore(bool player)
    {
        if(player)
        {
            playerScore++;
            playerScoreText.text = playerScore + "/" + maxScore;
            if(playerScore >= maxScore)
            {
                EndGame(playerWinMessage);
            }
        }
        else
        {
            opponentScore++;
            opponentScoreText.text = opponentScore + "/" + maxScore;
            if (opponentScore >= maxScore)
            {
                EndGame(opponentWinMessage);
            }
        }
    }

    void EndGame(string message)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = message;
        Time.timeScale = 0;
    }
}
