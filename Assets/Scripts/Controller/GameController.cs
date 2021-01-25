using UnityEngine;
using System;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Header("Controller references")]
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private UIController uiController;

    [Header("Player references")]
    [SerializeField]
    private Transform targetIndicatorOrigin;
    [SerializeField]
    private EntityModel playerModel;
    [SerializeField]
    private LineRenderer targetIndicator;
    [SerializeField]
    private Transform playerStartingPosition;
    [SerializeField]
    private Transform enemyStartingPosition;

    [Header("Enemy references")]
    [SerializeField]
    private EntityModel enemyModel;

    [Header("Settings")]
    [SerializeField]
    private float jumpMaxforce;
    [SerializeField]
    private float jumpMinforce;

    private PlayerController player;
    private EnemyController enemy;

    void Start()
    {
        cameraController.StartCameraPan();
        cameraController.CameraMovedToPlayer += CreatePlayer;
        enemy = new EnemyController(enemyModel);
    }

    void Update()
    {
        if (player != null)
            player.Update();
    }

    public void Debug_ResetPlayer()
    {
        var playerTransform = playerModel.transform;
        playerTransform.position = playerStartingPosition.position;
        playerTransform.rotation = playerStartingPosition.rotation;
        player.ForceNextBehaviour();
    }

    private void CreatePlayer()
    {
        cameraController.CameraMovedToPlayer -= CreatePlayer;

        IPlayerBehaviour startBehaviour = CreatePlayerBehaviours();

        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                player = new PlayerController(new MouseInputComponent(), startBehaviour , playerModel);
                break;
            case RuntimePlatform.Android:
                player = new PlayerController(new TouchInputComponent(), startBehaviour, playerModel);
                break;
            default:
                throw new NotImplementedException(Application.platform.ToString());
        }
    }

    private IPlayerBehaviour CreatePlayerBehaviours()
    {
        AimBehaviour aimBehaviour = new AimBehaviour(targetIndicatorOrigin, targetIndicator);
        aimBehaviour.AimAngleChanged += uiController.OnAimAngleChanged;
        aimBehaviour.PowerLevelChanged += uiController.OnPowerLevelChanged;
        aimBehaviour.AimingFinished += uiController.OnAimingFinished;

        JumpBehaviour jumpBehaviour = new JumpBehaviour(playerModel, jumpMaxforce, jumpMinforce);
        aimBehaviour.AimingFinished += jumpBehaviour.OnAimingFinished;
        aimBehaviour.SetNextBehaviour(jumpBehaviour);
        jumpBehaviour.SetNextBehaviour(aimBehaviour);
        jumpBehaviour.Jumped += OnJumped;
        jumpBehaviour.HitEnemy += enemy.OnHit;

        return aimBehaviour;
    }

    private void OnJumped()
    {
        StartCoroutine(FollowPlayerJump());
    }

    private IEnumerator FollowPlayerJump()
    {
        //need to wait for the next physics frame to get the correct velocity
        yield return new WaitForFixedUpdate();

        var velocity = playerModel.RigidBody.velocity;
        var distance = enemyStartingPosition.position.x - playerStartingPosition.position.x;
        var traveltime = distance / (velocity.x / 2);
        cameraController.StartCameraPan(traveltime);
    }
}