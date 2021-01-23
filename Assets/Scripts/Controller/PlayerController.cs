using System;
using UnityEngine;

public class PlayerController
{
    private IInputComponent input;
    private Transform targetIndicatorOrigin;
    private LineRenderer targetIndicator;

    public PlayerController(Transform targetOrigin, IInputComponent inputComponent, LineRenderer targetIndicator)
    {
        targetIndicatorOrigin = targetOrigin;
        input = inputComponent;
        this.targetIndicator = targetIndicator;
        targetIndicator.enabled = false;
    }

    public void Update()
    {
        if(input.IsInputPressed())
        {
            Vector3 inputPosition = input.GetInputPosition();
            var directionVector = (inputPosition - targetIndicatorOrigin.position).normalized;
            if(directionVector.y >= 0 && directionVector.x >= 0)
            {
                var linePositions = new Vector3[2] { targetIndicatorOrigin.position, inputPosition };
                targetIndicator.enabled = true;
                targetIndicator.positionCount = 2;
                targetIndicator.SetPositions(linePositions);
            }
        }
    }
}