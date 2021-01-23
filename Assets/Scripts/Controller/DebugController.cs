using System;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    private void Start()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor)
            enabled = false;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            gameController.Debug_ResetPlayer();
        }
    }
}