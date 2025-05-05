using UnityEngine;
using PixelCrushers.DialogueSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public int totalDocumentCount = 0;
    [SerializeField] private int dayOneDocs = 0;
    [SerializeField] private int dayTwoDocs = 0;
    [SerializeField] private int dayThreeDocs = 0;
    [SerializeField] public int currentDay = 1; // 1 = Day 1, 2 = Day 2, 3 = Day 3

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int CurrentDay => currentDay; // Public getter

    public void SetCurrentDay(int day)
    {
        currentDay = Mathf.Clamp(day, 1, 3);
        Debug.Log($"Current game day set to: {currentDay}");
    }

    public void AddDocument()
    {
        totalDocumentCount++;
        DialogueLua.SetVariable("Act1Variables.totalDocs", totalDocumentCount);
        Debug.Log($"Total Documents: {totalDocumentCount}");

        switch (currentDay)
        {
            case 1:
                dayOneDocs++;
                DialogueLua.SetVariable("Act1Variables.dayOneDocs", dayOneDocs);
                Debug.Log($"Day 1 Docs: {dayOneDocs}");
                break;
            case 2:
                dayTwoDocs++;
                DialogueLua.SetVariable("Act1Variables.dayTwoDocs", dayTwoDocs);
                Debug.Log($"Day 2 Docs: {dayTwoDocs}");
                break;
            case 3:
                dayThreeDocs++;
                DialogueLua.SetVariable("Act1Variables.dayThreeDocs", dayThreeDocs);
                Debug.Log($"Day 3 Docs: {dayThreeDocs}");
                break;
        }
    }
}
