public class PlayerController
{
    private IInputComponent input;
    private IPlayerBehaviour currentbehaviour;

    public PlayerController(IInputComponent inputComponent, IPlayerBehaviour startingBehaviour)
    {
        input = inputComponent;
        currentbehaviour = startingBehaviour;
    }

    public void Update()
    {
        currentbehaviour = currentbehaviour.UpdateBehaviour(input);
    }

    public void SetBehaviour(IPlayerBehaviour newBehaviour)
    {
        currentbehaviour = newBehaviour;
    }

    //for debug purposes
    public void ForceNextBehaviour()
    {
        currentbehaviour = currentbehaviour.GetNextBehaviour();
    }
}