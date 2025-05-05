using UnityEngine;
using Cinemachine;

public class CameraRegister : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        CameraManager.Register(GetComponent<CinemachineVirtualCamera>());
    }

    private void OnDisable()
    {
        CameraManager.Unregister(GetComponent<CinemachineVirtualCamera>());
    }
}
