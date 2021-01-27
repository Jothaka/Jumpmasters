using UnityEngine;
using System;
using System.Collections;

public class GameController : MonoBehaviour
{
    private event Action PlayerTurnStart;
    private event Action EnemyTurnStart;

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
    [SerializeField]
    private AISettings aiSettings;

    [Header("Settings")]
    [SerializeField]
    private float jumpMaxforce;
    [SerializeField]
    private float jumpMinforce;

    private PlayerController player;
    private EnemyController enemy;

    void Start()
    {
        cameraController.StartCameraPanPlayerToEnemy();
        cameraController.CameraMovedToPlayer += CreatePlayer;
        var enemyBehavior = CreateEnemyBehaviours();
        enemy = new EnemyController(enemyModel, enemyBehavior);
        enemy.Killed += OnEnemyKilled;
    }

    void Update()
    {
        if (player != null)
            player.Update();
        if (enemy != null)
            enemy.Update();
    }

    //invoked by GameOverView Button press
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void CreatePlayer()
    {
        cameraController.CameraMovedToPlayer -= CreatePlayer;

        IPlayerBehaviour startBehaviour = CreatePlayerBehaviours();

        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                player = new PlayerController(new MouseInputComponent(), startBehaviour, playerModel);
                break;
            case RuntimePlatform.Android:
                player = new PlayerController(new TouchInputComponent(), startBehaviour, playerModel);
                break;
            default:
                throw new NotImplementedException(Application.platform.ToString());
        }

        player.Killed += OnPlayerKilled;
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
        jumpBehaviour.Jumped += OnPlayerJumped;
        jumpBehaviour.HitEnemy += OnEnemyHit;

        WaitBehaviour waitBehaviour = new WaitBehaviour();
        waitBehaviour.SetNextBehaviour(aimBehaviour);
        jumpBehaviour.SetNextBehaviour(waitBehaviour);
        PlayerTurnStart += waitBehaviour.StopWaiting;

        return aimBehaviour;
    }

    private IPlayerBehaviour CreateEnemyBehaviours()
    {
        WaitBehaviour waitBehaviour = new WaitBehaviour();
        EnemyTurnStart += waitBehaviour.StopWaiting;

        AIAimBehaviour aimBehaviour = new AIAimBehaviour(aiSettings);
        waitBehaviour.SetNextBehaviour(aimBehaviour);

        JumpBehaviour jumpBehaviour = new JumpBehaviour(enemyModel, jumpMaxforce, jumpMinforce);
        aimBehaviour.SetNextBehaviour(jumpBehaviour);
        aimBehaviour.AIAimingFinished += jumpBehaviour.OnAimingFinished;
        jumpBehaviour.SetNextBehaviour(waitBehaviour);
        jumpBehaviour.Jumped += OnEnemyJumped;
        jumpBehaviour.HitEnemy += OnPlayerHit;

        return waitBehaviour;
    }

    private void ResetEntityPosition(Transform entityTransform, Transform targetPosition)
    {
        entityTransform.position = targetPosition.position;
        entityTransform.rotation = targetPosition.rotation;
    }

    #region Method-Callbacks
    private void StartAITurn()
    {
        ResetEntityPosition(playerModel.transform, playerStartingPosition);
        EnemyTurnStart?.Invoke();
    }

    private void StartPlayerTurn()
    {
        ResetEntityPosition(enemyModel.transform, enemyStartingPosition);
        PlayerTurnStart?.Invoke();
    }

    private void OnPlayerJumped()
    {
        StartCoroutine(FollowPlayerJump());
    }

    private void OnEnemyHit()
    {
        enemy.OnHit();
    }

    private void OnEnemyJumped()
    {
        StartCoroutine(FollowEnemyJump());
    }

    private void OnPlayerHit()
    {
        player.OnHit();
    }

    private void OnPlayerKilled()
    {
        uiController.ShowPlayerLostView();
    }
    private void OnEnemyKilled()
    {
        uiController.ShowPlayerWonView();
    }
    #endregion

    private IEnumerator FollowPlayerJump()
    {
        //need to wait for the next physics frame to get the correct velocity
        yield return new WaitForFixedUpdate();
        float traveltime = GetCameraTravelTime(playerModel, enemyStartingPosition, playerStartingPosition);
        cameraController.MoveToEnemy(traveltime);
        cameraController.CameraMovedToEnemy += StartAITurn;
    }

    private IEnumerator FollowEnemyJump()
    {
        //need to wait for the next physics frame to get the correct velocity
        yield return new WaitForFixedUpdate();
        float travelTime = GetCameraTravelTime(enemyModel, playerStartingPosition, enemyStartingPosition);
        cameraController.MoveToPlayer(travelTime);
        cameraController.CameraMovedToPlayer += StartPlayerTurn;
    }

    private static float GetCameraTravelTime(EntityModel model, Transform targetPosition, Transform originPosition)
    {
        var velocity = model.RigidBody.velocity;
        var distance = targetPosition.position.x - originPosition.position.x;
        var traveltime = distance / (velocity.x / 2);
        return traveltime;
    }    
}