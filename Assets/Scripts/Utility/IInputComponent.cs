using UnityEngine;
public interface IInputComponent
{
    bool IsInputPressed();
    Vector2 GetInputPosition();
}