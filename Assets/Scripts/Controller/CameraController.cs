using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public event Action CameraMovedToEnemy;
    public event Action CameraMovedToPlayer;

    [SerializeField]
    private CameraView cameraView;

    [SerializeField]
    private float CameraMoveDuration;

    public void StartCameraPan()
    {
        MoveToEnemy();
    }

    private void MoveToEnemy()
    {
        var enemyTween = cameraView.MoveToEnemyRestingPoint(CameraMoveDuration);
        enemyTween.onComplete += OnEnemyMoveTweenComplete;
    }

    private void MoveToPlayer()
    {
        var playerTween = cameraView.MoveToPlayerRestingPoint(CameraMoveDuration);
        playerTween.onComplete += OnPlayerMoveTweenComplete;
    }

    private void OnEnemyMoveTweenComplete()
    {
        CameraMovedToEnemy?.Invoke();
        MoveToPlayer();
    }

    private void OnPlayerMoveTweenComplete()
    {
        CameraMovedToPlayer?.Invoke();
    }
}