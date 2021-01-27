using System;

public class PlayerController
{
    private readonly IInputComponent input;

    public event Action Killed;

    private IPlayerBehaviour currentbehaviour;
    private EntityModel playerModel;
    private int currentHealth;

    public PlayerController(IInputComponent inputComponent, IPlayerBehaviour startingBehaviour, EntityModel entityModel)
    {
        input = inputComponent;
        currentbehaviour = startingBehaviour;
        playerModel = entityModel;
        currentHealth = playerModel.MaxHealth;
    }

    public void Update()
    {
        if (currentHealth > 0)
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

    public void OnHit()
    {
        if (currentbehaviour.ReceiveDamageOnHit())
        {
            currentHealth -= playerModel.DamageReceived;
            float healthPercentage = (float)currentHealth / (float)playerModel.MaxHealth;
            playerModel.EntityHealthbar.SetTargetFillAmount(healthPercentage);
            playerModel.EntityView.SetAnimatorHitTrigger();

            if (currentHealth <= 0)
                Killed?.Invoke();
        }
    }
}