using UnityEngine;
using UnityEngine.EventSystems;

public class MouseLock : MonoBehaviour
{
    void Start()
    {
        OnEnable();
    }

    void Update()
    {
        OnEnable();
    }
    void OnEnable()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Disable UI mouse interaction
        if (EventSystem.current != null)
        {
            EventSystem.current.enabled = false;
        }
    }

    void OnDisable()
    {
        // Optional: restore mouse when leaving the scene or disabling this script
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (EventSystem.current != null)
        {
            EventSystem.current.enabled = true;
        }
    }
}
