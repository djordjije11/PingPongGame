using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private int player1Score;
    private int player2Score;
    public Text player1ScoreText;
    public Text player2ScoreText;
    public BallMovement ballMovement;
    public Text playerWinsText;
    public Text hitCounterText;
    public Text goalText;

    public void Player1Scores()
    {
        player1Score++;
        ballMovement.SetIsPlayer1First(false);
        PlayerScores();
    }
    public void Player2Scores()
    {
        player2Score++;
        ballMovement.SetIsPlayer1First(true);
        PlayerScores();
    }
    private void PlayerScores()
    {
        RefreshScoreText();
        StartCoroutine(ShowGoalText());
        if (player1Score == 5 || player2Score == 5)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            ballMovement.gameObject.SetActive(false);
            StartCoroutine(ballMovement.RestartBall());
        }
    }
    private IEnumerator ShowGoalText()
    {
        var counter = ballMovement.HitCounter;
        goalText.gameObject.SetActive(true);
        bool showHitCounter = counter > 10;
        if(showHitCounter == true)
        {
            hitCounterText.text = counter.ToString() + " HITS";
            hitCounterText.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        if (showHitCounter == true)
        {
            hitCounterText.gameObject.SetActive(false);
            hitCounterText.text = "";
        }
        goalText.gameObject.SetActive(false);
    }

    private void RefreshScoreText()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    private IEnumerator GameOver()
    {
        ballMovement.FreezeBall();
        if (player1Score > player2Score)
        {
            playerWinsText.text = "PLAYER 1 WINS!";
        }
        else
        {
            playerWinsText.text = "PLAYER 2 WINS!";
        }
        playerWinsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }

}
