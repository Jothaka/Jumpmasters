using System;
using UnityEngine;

public class AimBehaviour : IPlayerBehaviour
{
    public delegate void AimingFinishedDelegate(float angle, float powerLvl);

    public event Action<float> AimAngleChanged;
    public event Action<float> PowerLevelChanged;
    public event AimingFinishedDelegate AimingFinished;

    private Transform targetIndicatorOrigin;
    private LineRenderer targetIndicator;
    private IPlayerBehaviour nextBehaviour;

    private float currentPowerLevel;
    private float currentAimAngle;

    public AimBehaviour(Transform targetOrigin, LineRenderer targetIndicator)
    {
        targetIndicatorOrigin = targetOrigin;
        this.targetIndicator = targetIndicator;
        nextBehaviour = this;
    }

    public void SetNextBehaviour(IPlayerBehaviour playerbehaviour)
    {
        nextBehaviour = playerbehaviour;
    }

    public IPlayerBehaviour UpdateBehaviour(IInputComponent input)
    {
        if (input.IsInputPressed())
        {
            UpdateAimAngle(input);
        }
        else if (targetIndicator.enabled)
        {
            targetIndicator.enabled = false;
            AimingFinished?.Invoke(currentAimAngle, currentPowerLevel);
            return nextBehaviour;
        }

        return this;
    }

    private void UpdateAimAngle(IInputComponent input)
    {
        Vector3 inputPosition = input.GetInputPosition();
        var directionVector = (inputPosition - targetIndicatorOrigin.position).normalized;

        if (directionVector.y >= 0 && directionVector.x >= 0)
        {
            SetLineRenderer(inputPosition);

            currentAimAngle = Mathf.Rad2Deg * Mathf.Atan2(directionVector.y, directionVector.x);
            AimAngleChanged?.Invoke(currentAimAngle);
        }
    }

    private void SetLineRenderer(Vector3 inputPosition)
    {
        var linePositions = new Vector3[2] { targetIndicatorOrigin.position, inputPosition };
        targetIndicator.enabled = true;
        targetIndicator.positionCount = 2;
        targetIndicator.SetPositions(linePositions);
    }
}