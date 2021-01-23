using System;
using UnityEngine;

public class MouseInputComponent : IInputComponent
{
    public Vector2 GetInputPosition()
    {
        if (IsInputPressed())
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return Vector2.zero;
    }

    public bool IsInputPressed()
    {
        return Input.GetMouseButton(0);
    }
}