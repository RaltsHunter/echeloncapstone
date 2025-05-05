using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubTooltips : MonoBehaviour
{
    public bool newToRoom = true;
    // Start is called before the first frame update
    void Start()
    {
        if (newToRoom)
        {
            TooltipUI.ShowMessage("You can buy items on your computer that may help you.");
            newToRoom = false; // Set to false after showing the message
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
