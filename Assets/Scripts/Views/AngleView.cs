using UnityEngine;
using UnityEngine.UI;

public class AngleView : UIView
{
    [SerializeField]
    private Text angleText;

   public void SetDisplayedAngle(float angle)
    {
        angleText.text = string.Format("{0,2}°", angle);
    }
}