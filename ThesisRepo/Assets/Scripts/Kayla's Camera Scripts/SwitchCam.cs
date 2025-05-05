using UnityEngine;
using Cinemachine;

public class SwitchCam : MonoBehaviour

{
    public CinemachineVirtualCamera StraightCamera;
    public CinemachineVirtualCamera TurnCamera;
    public CinemachineVirtualCamera TopCamera;

    public CinemachineVirtualCamera R1Camera;
    public CinemachineVirtualCamera R2Camera;
    public CinemachineVirtualCamera R3Camera;

    public CinemachineVirtualCamera H1Camera;
    public CinemachineVirtualCamera H2Camera;
    public CinemachineVirtualCamera H3Camera;
    public CinemachineVirtualCamera H4Camera;
    public CinemachineVirtualCamera Store1Camera;
    public CinemachineVirtualCamera Store2Camera;
    public CinemachineVirtualCamera Store3Camera;
    public CinemachineVirtualCamera WCCamera;



    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            CameraManager.SwitchCamera(H1Camera);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CameraManager.SwitchCamera(WCCamera);
        }

    }



    void OnEnable()
    {
        CombatManager.OnCombatTriggered += SwitchToCombatCam;
    }

    void OnDisable()
    {
        CombatManager.OnCombatTriggered -= SwitchToCombatCam;
    }

    void SwitchToCombatCam()
    {
        CameraManager.SwitchCamera(TopCamera);
    }
}
