using System;
using UnityEngine;

public class EnemyController
{
    public event Action Killed;

    private EntityModel model;

    private int currentHealth;

    private IPlayerBehaviour enemyBehaviour;

    private IInputComponent fakeInput;

    public EnemyController(EntityModel entityModel, IPlayerBehaviour enemyBehaviour)
    {
        model = entityModel;
        currentHealth = model.MaxHealth;
        this.enemyBehaviour = enemyBehaviour;
        fakeInput = new NoInputComponent();
    }

    public void Update()
    {
        if (currentHealth > 0)
            enemyBehaviour = enemyBehaviour.UpdateBehaviour(fakeInput);
    }

    public void OnHit()
    {
        if (enemyBehaviour.ReceiveDamageOnHit())
        {
            currentHealth -= model.DamageReceived;
            float healthPercentage = (float)currentHealth / (float)model.MaxHealth;
            model.EntityHealthbar.SetTargetFillAmount(healthPercentage);

            if (currentHealth <= 0)
                Killed?.Invoke();
        }
    }
}