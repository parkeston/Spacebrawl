using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitButton : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
