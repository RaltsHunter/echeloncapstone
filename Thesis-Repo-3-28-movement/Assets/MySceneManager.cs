using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;
    public Image fadeImage; // Assign this in the Inspector
    public float fadeDuration = 1f; // Duration of the fade

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to fade to black and load a scene
    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        yield return StartCoroutine(Fade(1)); // Fade to black
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        yield return StartCoroutine(Fade(0)); // Fade back in
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, targetAlpha);
    }

    public void FadeSceneFromLua()
    {
        string sceneName = DialogueLua.GetVariable("NextScene").AsString;
        if (!string.IsNullOrEmpty(sceneName))
        {
            FadeAndLoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty or invalid!");
        }
    }
}
