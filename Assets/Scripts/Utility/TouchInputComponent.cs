using System;
using UnityEngine;
public class TouchInputComponent : IInputComponent
{
    public Vector2 GetInputPosition()
    {
        if (IsInputPressed())
        {
            Touch touch = Input.GetTouch(0);
            return Camera.main.ScreenToWorldPoint(touch.position);
        }
        return Vector2.zero;
    }

    public bool IsInputPressed()
    {
        return Input.touchCount > 0;
    }
}