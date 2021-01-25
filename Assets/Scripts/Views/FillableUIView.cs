using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillableUIView : UIView
{
    [SerializeField]
    private Image imageToFill;

    [SerializeField]
    private float fillSpeed = 0.2f;

    public void SetTargetFillAmount(float fillAmount)
    {
        StartCoroutine(ReduceFillToTargetAmount(fillAmount));
    }

    public void SetFillAmount(float fillAmount)
    {
        imageToFill.fillAmount = fillAmount;
    }

    private IEnumerator ReduceFillToTargetAmount(float targetFillamount)
    {
        while (imageToFill.fillAmount > targetFillamount)
        {
            imageToFill.fillAmount -= Time.deltaTime * fillSpeed;
            yield return new WaitForEndOfFrame();
        }
    }
}