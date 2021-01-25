using System;
using UnityEngine;

public class JumpBehaviour : IPlayerBehaviour
{
    public event Action Jumped;
    public event Action HitEnemy;

    private Rigidbody2D rigidbody;
    private EntityModel model;
    private float jumpRange;
    private float jumpMinforce;

    private IPlayerBehaviour nextBehaviour;

    public JumpBehaviour(EntityModel entityModel, float maxForce, float minForce)
    {
        model = entityModel;
        rigidbody = entityModel.RigidBody;
        jumpRange = maxForce - minForce;
        jumpMinforce = minForce;
    }

    public IPlayerBehaviour UpdateBehaviour(IInputComponent input)
    {
        bool playerAscending = rigidbody.velocity.y >= 0;

        model.EntityView.SetAnimatorAscendingParameter(playerAscending);

        return this;
    }

    public void SetNextBehaviour(IPlayerBehaviour nextBehaviour)
    {
        this.nextBehaviour = nextBehaviour;
    }

    public IPlayerBehaviour GetNextBehaviour()
    {
        return nextBehaviour;
    }

    public void OnAimingFinished(float angle, float powerLvl)
    {
        JumpInDirection(angle, powerLvl);
        var view = model.EntityView;

        view.SetAnimatorAscendingParameter(true);
        view.SetAnimatorGroundedParameter(false);

        view.CollisionEnter += OnCollisionEnter;

        Jumped?.Invoke();
    }

    private void JumpInDirection(float angle, float powerLvl)
    {
        var forceStrength = powerLvl * jumpRange + jumpMinforce;
        var directionVector = Quaternion.AngleAxis(angle, Vector3.forward);
        var localDirection = directionVector * Vector3.right;
        var worldDirection = rigidbody.transform.TransformDirection(localDirection);
        rigidbody.AddForce(worldDirection * forceStrength);
    }

    private void OnCollisionEnter(Collision2D obj)
    {
        var view = model.EntityView;
        view.CollisionEnter -= OnCollisionEnter;

        rigidbody.velocity = Vector2.zero;
        view.SetAnimatorGroundedParameter(true);

        if (obj.gameObject.CompareTag(model.EntityEnemyTag))
            HitEnemy?.Invoke();
    }
}