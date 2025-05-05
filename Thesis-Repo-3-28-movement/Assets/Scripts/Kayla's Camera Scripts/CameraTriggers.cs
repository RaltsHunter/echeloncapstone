using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraTriggers : MonoBehaviour
{
    // [SerializeField] private CinemachineVirtualCamera [] _cameraSettings;
    // [SerializeField] private int _whichCamera;


    [Header("Cameras")]
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

    [Header("Walls")]

    // public Renderer Store3Walls;

    public Renderer[] Store3WallObj;
    public Renderer WC1Walls;
    public Renderer WC2Walls;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        R1Camera = FindCamera("R1 Cam");
        R2Camera = FindCamera("R2 Cam");
        R3Camera = FindCamera("R3 Cam");
        H1Camera = FindCamera("H1 Cam");
        H2Camera = FindCamera("H2 Cam");
        H3Camera = FindCamera("H3 Cam");
        H4Camera = FindCamera("H4 Cam");
        Store1Camera = FindCamera("Store 1 Cam");
        Store2Camera = FindCamera("Store 2 Cam");
        Store3Camera = FindCamera("Store 3 Cam");
        WCCamera = FindCamera("WC Cam");

        Store3WallObj = new Renderer[]
        {
        FindRenderer("Store 3 Walls"),
        FindRenderer("Store3 SM1"),
        FindRenderer("Store3 SM2"),
        FindRenderer("Store3 SM3")
        };

        WC1Walls = FindRenderer("WC Walls 1");
        WC2Walls = FindRenderer("WC Walls 2");
    }

    private CinemachineVirtualCamera FindCamera(string name)
    {
        GameObject obj = GameObject.Find(name);
        if (obj == null)
        {
            Debug.LogWarning($"Camera GameObject '{name}' not found in scene.");
            return null;
        }
        return obj.GetComponent<CinemachineVirtualCamera>();
    }

    private Renderer FindRenderer(string name)
    {
        GameObject obj = GameObject.Find(name);
        if (obj == null)
        {
            Debug.LogWarning($"Renderer GameObject '{name}' not found in scene.");
            return null;
        }
        return obj.GetComponent<Renderer>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("H1Trigger"))
        {
            CameraManager.SwitchCamera(H1Camera);
            Debug.Log("H1Trigger");
        }
        if (other.CompareTag("H2Trigger"))
        {

            Debug.Log("H2Trigger");
            CameraManager.SwitchCamera(H2Camera);
        }
        if (other.CompareTag("H3Trigger"))
        {
            CameraManager.SwitchCamera(H3Camera);

            foreach (Renderer Store3Wall in Store3WallObj)
            {
                Store3Wall.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            // Store3Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            WC1Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
        if (other.CompareTag("H4Trigger"))
        {
            CameraManager.SwitchCamera(H4Camera);
        }
        if (other.CompareTag("Store1Trigger"))
        {
            CameraManager.SwitchCamera(Store1Camera);
        }
        if (other.CompareTag("Store2Trigger"))
        {
            CameraManager.SwitchCamera(Store2Camera);
        }
        if (other.CompareTag("Store3Trigger"))
        {
            CameraManager.SwitchCamera(Store3Camera);
            
            foreach (Renderer Store3Wall in Store3WallObj)
            {
                Store3Wall.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }

            // Store3Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
        if (other.CompareTag("R1Trigger"))
        {
            CameraManager.SwitchCamera(R1Camera);
        }
        if (other.CompareTag("R2Trigger"))
        {
            CameraManager.SwitchCamera(R2Camera);
        }
        if (other.CompareTag("R3Trigger"))
        {
            CameraManager.SwitchCamera(R3Camera);
        }
        if (other.CompareTag("WCTrigger"))
        {
            CameraManager.SwitchCamera(WCCamera);
            WC1Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            WC2Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        if (other.CompareTag("WCClose"))
        {
            WC2Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }
}
