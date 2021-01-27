using System;
using UnityEngine;

public class EntityView : MonoBehaviour
{
    private static readonly int AscendingJumpBoolHash = Animator.StringToHash("AscendingJump");
    private static readonly int GroundedBoolHash = Animator.StringToHash("Grounded");
    private static readonly int HitTriggerHash = Animator.StringToHash("Hit");

    public event Action<Collision2D> CollisionEnter;

    [SerializeField]
    private Animator entityAnimator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnter?.Invoke(collision);
    }

    public void SetAnimatorAscendingParameter(bool parameterValue)
    {
        entityAnimator.SetBool(AscendingJumpBoolHash, parameterValue);
    }

    public void SetAnimatorGroundedParameter(bool parameterValue)
    {
        entityAnimator.SetBool(GroundedBoolHash, parameterValue);
    }

    public void SetAnimatorHitTrigger()
    {
        entityAnimator.SetTrigger(HitTriggerHash);
    }
}