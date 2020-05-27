using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseListener : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(!pauseScreen.activeSelf);
        }
    }
}
