using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject ui;
    private GraphicRaycaster main;

    public void PauseGame()
    {
        main = ui.GetComponent<GraphicRaycaster>();
        if (main.enabled == true)
            main.enabled = false;
        else
            main.enabled = true;
    }
}
