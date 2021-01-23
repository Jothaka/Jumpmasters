using UnityEngine;
using UnityEngine.UI;

public class AngleView : MonoBehaviour
{
    [SerializeField]
    private Text angleText;

   public void SetDisplayedAngle(float angle)
    {
        angleText.text = string.Format("{0,3}°", angle);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
