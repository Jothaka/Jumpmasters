using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public event Action CameraMovedToEnemy;
    public event Action CameraMovedToPlayer;

    [SerializeField]
    private CameraView cameraView;

    [SerializeField]
    private float CameraDefaultMoveDuration;


    public void StartCameraPan(float duration = 0)
    {
        MoveToEnemy(duration);
        CameraMovedToEnemy += MoveToPlayer;
    }

    private void MoveToEnemy(float duration = 0)
    {
        if (duration == 0)
            duration = CameraDefaultMoveDuration;
        var enemyTween = cameraView.MoveToEnemyRestingPoint(duration);
        enemyTween.onComplete += OnEnemyMoveTweenComplete;
    }

    private void MoveToPlayer()
    {
        MoveToPlayer(CameraDefaultMoveDuration);
    }

    private void MoveToPlayer(float duration)
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