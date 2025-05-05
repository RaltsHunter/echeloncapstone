using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene("PlayerRoomA1S1TEST");
        }
        else
        {
            Debug.LogWarning("Scene name is not set on SceneLoader.");
        }
    }
}
