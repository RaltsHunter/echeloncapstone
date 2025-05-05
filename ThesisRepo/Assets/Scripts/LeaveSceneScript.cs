/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1DoorScript : MonoBehaviour
{
   [SerializeField] public string sceneName;
   public SceneManager SceneManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit")) // Ensure the Player has the "Player" tag
        {
            SceneManager.FadeAndLoadScene(sceneName);
        }
    }
}
*/