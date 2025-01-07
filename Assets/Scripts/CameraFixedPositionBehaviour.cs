using UnityEngine;

public class CameraFixedPositionBehaviour : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 15f;
        transform.LookAt(mainCamera.transform);
    }
}
