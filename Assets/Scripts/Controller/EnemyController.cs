using System;
using UnityEngine;

public class EnemyController
{
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
        enemyBehaviour = enemyBehaviour.UpdateBehaviour(fakeInput);
    }

    public void OnHit()
    {
        if (enemyBehaviour.ReceiveDamageOnHit())
        {
            currentHealth -= model.DamageReceived;
            float healthPercentage = (float)currentHealth / (float)model.MaxHealth;
            model.EntityHealthbar.SetTargetFillAmount(healthPercentage);
        }
    }
}