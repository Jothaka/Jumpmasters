using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private UIController uiController;

    [SerializeField]
    private Transform targetIndicatorOrigin;

    [SerializeField]
    private LineRenderer targetIndicator;

    private PlayerController player;


    void Start()
    {
        cameraController.StartCameraPan();
        cameraController.CameraMovedToPlayer += CreatePlayer;
    }

    void Update()
    {
        if (player != null)
            player.Update();
    }

    private void CreatePlayer()
    {
        IPlayerBehaviour startBehaviour = CreatePlayerBehaviours();

        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                player = new PlayerController(new MouseInputComponent(), startBehaviour);
                break;
            case RuntimePlatform.Android:
                player = new PlayerController(new TouchInputComponent(), startBehaviour);
                break;
            default:
                throw new NotImplementedException(Application.platform.ToString());
        }
    }

    private IPlayerBehaviour CreatePlayerBehaviours()
    {
        AimBehaviour aimBehaviour = new AimBehaviour(targetIndicatorOrigin, targetIndicator);
        aimBehaviour.AimAngleChanged += uiController.OnAimAngleChanged;
        aimBehaviour.AimingFinished += uiController.OnAimingFinished;
        return aimBehaviour;
    }
}