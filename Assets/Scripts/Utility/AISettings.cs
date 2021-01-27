using UnityEngine;

[CreateAssetMenu()]
public class AISettings : ScriptableObject
{
    public float TimeUntilJump;
    public float InitialMinAngle = 1;
    public float InitialMaxAngle = 89;
    [Range(0f,1.0f)]
    public float MinimalPower = 0.45f;
}