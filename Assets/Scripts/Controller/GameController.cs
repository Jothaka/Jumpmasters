using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;

    void Start()
    {
        cameraController.StartCameraPan();
    }

    void Update()
    {
        
    }
}
