using DG.Tweening;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerRestingPoint;
    [SerializeField]
    private Transform EnemyResitingPoint;
    
    public Tween MoveToPlayerRestingPoint(float duration)
    {
        return transform.DOMove(PlayerRestingPoint.position, duration);
    }

    public Tween MoveToEnemyRestingPoint(float duration)
    {
        return transform.DOMove(EnemyResitingPoint.position, duration);
    }
}
