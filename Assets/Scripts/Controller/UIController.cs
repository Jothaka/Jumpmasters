using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private AngleView angleView;
    [SerializeField]
    private PowerView powerView;

    public void OnAimAngleChanged(float angle)
    {
        angleView.Show();
        angleView.SetDisplayedAngle(angle);
    }

    public void OnPowerLevelChanged(float powerLevel)
    {
        powerView.Show();
        powerView.SetPowerLevel(powerLevel);
    }

    public void OnAimingFinished(float angle, float powerLvl)
    {
        angleView.Hide();
        powerView.Hide();
    }
}