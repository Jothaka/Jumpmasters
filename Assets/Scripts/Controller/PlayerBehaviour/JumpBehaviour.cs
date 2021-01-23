using System;

public class JumpBehaviour : IPlayerBehaviour
{
    public IPlayerBehaviour UpdateBehaviour(IInputComponent input)
    {
        return this;
    }
}