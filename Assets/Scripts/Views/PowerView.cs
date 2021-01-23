using UnityEngine;
using UnityEngine.UI;

public class PowerView : UIView
{
    [SerializeField]
    private Image powerLevelFill;

    public void SetPowerLevel(float powerLevel)
    {
        powerLevelFill.fillAmount = powerLevel;
    }
}