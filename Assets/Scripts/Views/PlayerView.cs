using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private static readonly int AscendingJumpBoolHash = Animator.StringToHash("AscendingJump");
    private static readonly int GroundedBoolHash = Animator.StringToHash("Grounded");

    public event Action<Collision2D> CollisionEnter;

    [SerializeField]
    private Animator playerAnimator;

    public Rigidbody2D PlayerRigidBody { get; private set; }


    private void Start()
    {
        PlayerRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnter?.Invoke(collision);
    }

    public void SetAnimatorAscendingParameter(bool parameterValue)
    {
        playerAnimator.SetBool(AscendingJumpBoolHash, parameterValue);
    }

    public void SetAnimatorGroundedParameter(bool parameterValue)
    {
        playerAnimator.SetBool(GroundedBoolHash, parameterValue);
    }
}