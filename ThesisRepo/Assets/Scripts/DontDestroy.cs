using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] existingObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in existingObjects)
        {
            if (obj != this.gameObject && obj.name == this.gameObject.name)
            {
                Destroy(gameObject); // Destroy duplicate object with the same name
                return;
            }
        }

        DontDestroyOnLoad(gameObject); // Keep this object
    }
}
