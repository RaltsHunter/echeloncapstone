using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnTimerScript : MonoBehaviour
{
    [SerializeField] public TMP_Text turnTimerText;
    [SerializeField] public TurnManager turnManager; 
    // Start is called before the first frame update
    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        turnTimerText = GameObject.Find("TurnTimerText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        turnTimerText.text = turnManager.roundCounter.ToString();
    }
}
