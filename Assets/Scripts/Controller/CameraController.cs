using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public event Action CameraMovedToEnemy;
    public event Action CameraMovedToPlayer;

    [SerializeField]
    private CameraView cameraView;

    [SerializeField]
    private float cameraDefaultMoveDuration;

    public void StartCameraPanPlayerToEnemy()
    {
        MoveToEnemy();
        CameraMovedToEnemy += MoveToPlayer;
    }

    public void StartCameraPanPlayerToEnemy(float duration)
    {
        MoveToEnemy(duration);
        CameraMovedToEnemy += MoveToPlayer;
    }

    public void StartCameraPanEnemyToPlayer(float duration)
    {
        MoveToPlayer(duration);
        CameraMovedToPlayer += MoveToEnemy;
    }

    public void MoveToEnemy()
    {
        MoveToEnemy(cameraDefaultMoveDuration);
    }

    public void MoveToEnemy(float duration)
    {
        var enemyTween = cameraView.MoveToEnemyRestingPoint(duration);
        enemyTween.onComplete += OnEnemyMoveTweenComplete;
    }

    public void MoveToPlayer()
    {
        MoveToPlayer(cameraDefaultMoveDuration);
    }

    public void MoveToPlayer(float duration)
    {
        var playerTween = cameraView.MoveToPlayerRestingPoint(duration);
        playerTween.onComplete += OnPlayerMoveTweenComplete;
    }

    private void OnEnemyMoveTweenComplete()
    {
        CameraMovedToEnemy?.Invoke();
        CameraMovedToEnemy = null;
    }

    private void OnPlayerMoveTweenComplete()
    {
        CameraMovedToPlayer?.Invoke();
        CameraMovedToPlayer = null;
    }
}