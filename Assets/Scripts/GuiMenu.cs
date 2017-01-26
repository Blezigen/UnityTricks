using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiMenu : MonoBehaviour
{
    public GUISkin skin;
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 25), "StartGame"))
        {

        }

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 25, 100, 25), "Next Item"))
        {

        }

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 55, 100, 25), "Exit"))
        {

        }

    }
}
