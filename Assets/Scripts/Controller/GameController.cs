using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;

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
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                player = new PlayerController(targetIndicatorOrigin, new MouseInputComponent(), targetIndicator);
                break;
            case RuntimePlatform.Android:
                player = new PlayerController(targetIndicatorOrigin, new TouchInputComponent(), targetIndicator);
                break;
            default:
                throw new NotImplementedException(Application.platform.ToString());
        }
    }
}