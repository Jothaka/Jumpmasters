using UnityEngine.UI;
using UnityEngine;

public class GameOverView : UIView
{
    [SerializeField]
    private Text HighlightText;
    [SerializeField]
    private Text ButtonText;

    public void SetHighlightText(string text)
    {
        HighlightText.text = text;
    }

    public void SetButtonText(string text)
    {
        ButtonText.text = text;
    }
}