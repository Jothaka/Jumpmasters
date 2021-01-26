using System;

public class WaitBehaviour : IPlayerBehaviour
{
    private IPlayerBehaviour nextBehaviour;
    private bool isWaiting = true;

    public IPlayerBehaviour GetNextBehaviour()
    {
        return nextBehaviour;
    }

    public void SetNextBehaviour(IPlayerBehaviour nextBehaviour)
    {
        this.nextBehaviour = nextBehaviour;
    }

    public IPlayerBehaviour UpdateBehaviour(IInputComponent input)
    {
        if (isWaiting)
            return this;
        else
        {
            isWaiting = true;
            return nextBehaviour;
        }
    }

    public void StopWaiting()
    {
        isWaiting = false;
    }

    public bool ReceiveDamageOnHit()
    {
        return true;
    }
}