using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private AngleView angleView;


    public void OnAimAngleChanged(float angle)
    {
        angleView.Show();
        angleView.SetDisplayedAngle(angle);
    }

    internal void OnAimingFinished(float angle, float powerLvl)
    {
        angleView.Hide();
    }
}