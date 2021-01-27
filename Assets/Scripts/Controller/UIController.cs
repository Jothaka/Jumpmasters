using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private AngleView angleView;
    [SerializeField]
    private FillableUIView powerView;
    [SerializeField]
    private GameOverView gameOverView;

    public void OnAimAngleChanged(float angle)
    {
        angleView.Show();
        angleView.SetDisplayedAngle(angle);
    }

    public void ShowPlayerLostView()
    {
        gameOverView.SetButtonText("Retry");
        gameOverView.SetHighlightText("Game Over");
        gameOverView.Show();
    }

    public void ShowPlayerWonView()
    {
        gameOverView.SetButtonText("New Game");
        gameOverView.SetHighlightText("You Won!");
        gameOverView.Show();
    }

    public void OnPowerLevelChanged(float powerLevel)
    {
        powerView.Show();
        powerView.SetFillAmount(powerLevel);
    }

    public void OnAimingFinished(float angle, float powerLvl)
    {
        angleView.Hide();
        powerView.Hide();
    }
}