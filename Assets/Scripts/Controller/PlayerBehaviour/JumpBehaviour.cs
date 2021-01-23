using System;
using UnityEngine;

public class JumpBehaviour : IPlayerBehaviour
{
    public event Action Jumped;

    private Rigidbody2D rigidbody;
    private PlayerView playerView;
    private float jumpRange;
    private float jumpMinforce;

    private IPlayerBehaviour nextBehaviour;

    public JumpBehaviour(PlayerView playerView, float maxForce, float minForce)
    {
        this.playerView = playerView;
        rigidbody = playerView.PlayerRigidBody;
        playerView.CollisionEnter += OnCollisionEnter;
        jumpRange = maxForce - minForce;
        jumpMinforce = minForce;
    }

    public IPlayerBehaviour UpdateBehaviour(IInputComponent input)
    {
        bool playerAscending = rigidbody.velocity.y >= 0;

        playerView.SetAnimatorAscendingParameter(playerAscending);

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
        var forceStrength = powerLvl * jumpRange + jumpMinforce;
        var directionVector = Quaternion.AngleAxis(angle, Vector3.forward);
        var localDirection = directionVector * Vector3.right;
        var worldDirection = rigidbody.transform.TransformDirection(localDirection);
        rigidbody.AddForce(worldDirection * forceStrength);
        playerView.SetAnimatorAscendingParameter(true);
        playerView.SetAnimatorGroundedParameter(false);

        Jumped?.Invoke();
    }

    private void OnCollisionEnter(Collision2D obj)
    {
        rigidbody.velocity = Vector2.zero;
        playerView.SetAnimatorGroundedParameter(true);
    }
}