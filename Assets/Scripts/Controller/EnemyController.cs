using System;
using UnityEngine;

public class EnemyController
{
    private EntityModel model;

    private int currentHealth;

    public EnemyController(EntityModel entityModel)
    {
        model = entityModel;
        currentHealth = model.MaxHealth;
    }

    public void OnHit()
    {
        currentHealth -= model.DamageReceived;
        float healthPercentage = (float)currentHealth / (float)model.MaxHealth;
        model.EntityHealthbar.SetTargetFillAmount(healthPercentage);
    }
}