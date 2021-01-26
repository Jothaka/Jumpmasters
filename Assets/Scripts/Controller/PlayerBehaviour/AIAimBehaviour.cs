using UnityEngine;

public class AIAimBehaviour : IPlayerBehaviour
{
    public delegate void AIAimingFinishedDelegate(float angle, float powerLvl);

    public AIAimingFinishedDelegate AIAimingFinished;

    private IPlayerBehaviour nextBehaviour;

    private AISettings settings;

    private float elapsedTimeSinceBehaviourUpdateStart;

    public AIAimBehaviour(AISettings settings)
    {
        this.settings = settings;
    }

    public IPlayerBehaviour GetNextBehaviour()
    {
        return nextBehaviour;
    }

    public bool ReceiveDamageOnHit()
    {
        return false;
    }

    public void SetNextBehaviour(IPlayerBehaviour nextBehaviour)
    {
        this.nextBehaviour = nextBehaviour;
    }

    public IPlayerBehaviour UpdateBehaviour(IInputComponent input)
    {
        if(elapsedTimeSinceBehaviourUpdateStart >= settings.TimeUntilJump)
        {
            elapsedTimeSinceBehaviourUpdateStart = 0;
            SetJumpParameters();
            return nextBehaviour;
        }
        elapsedTimeSinceBehaviourUpdateStart += Time.deltaTime;
        return this;
    }

    //TODO: add a more refined decision regarding angle/pwrlvl
    private void SetJumpParameters()
    {
        var powerLvl = Random.value;
        
        var newAngle = Random.Range(settings.InitialMinAngle, settings.InitialMaxAngle);
        AIAimingFinished?.Invoke(newAngle, powerLvl);
    }
}