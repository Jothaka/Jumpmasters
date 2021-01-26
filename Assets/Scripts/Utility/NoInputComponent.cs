using UnityEngine;

public class NoInputComponent : IInputComponent
{
    public Vector2 GetInputPosition()
    {
        return Vector2.zero;
    }

    public bool IsInputPressed()
    {
        return false;
    }
}