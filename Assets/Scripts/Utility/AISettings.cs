using UnityEngine;

[CreateAssetMenu()]
public class AISettings : ScriptableObject
{
    public float TimeUntilJump;
    public float InitialMinAngle = 1;
    public float InitialMaxAngle = 89;
}